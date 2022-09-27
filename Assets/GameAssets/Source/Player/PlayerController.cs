using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : ControllerBaseModel
{
   [SerializeField] private StateMachineMono stateMachine;
   [SerializeField] private PlayerModel playerModel;
   [SerializeField] private PlayerMovement playerMovement;
   [SerializeField] private SplineFollower splineFollower;
 
   [SerializeField] private ParticleSystem colorTrail;
   [SerializeField] private CubeHandler cubeHandler;
   
   
   private bool feverMode;
   private float boostDuration;
   public override void Initialize()
   {
      base.Initialize();
      StartMove();
   }

   private void StartMove()
   {
      playerMovement.Move();
   }

   public void StopMove()
   {
      playerMovement.Stop();
   }
   
   private void Update()
   {
      if (feverMode)
      {
         OnFeverMode();
      }
   }

   private void OnFeverMode()
   {
      if (boostDuration > 0)
      {
         boostDuration -= Time.deltaTime;
      }
      else
      {
         feverMode = false;
         playerMovement.SetSpeed(7);
      }
   }
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Cube"))
      {
         var cube = other.gameObject.GetComponent<Cube>();
         OnTriggerCube(cube);
      }

      if (other.gameObject.CompareTag("RandomGate"))
      {
         var gate = other.gameObject.GetComponent<RandomGate>();
         OnPassRandomGate(gate);
      }
      if (other.gameObject.CompareTag("OrderGate"))
      {
         var gate = other.gameObject.GetComponent<OrderGate>();
         OnPassOrderGate(gate);
      }

      if (other.gameObject.CompareTag("SpeedBoost"))
      {
         var boost = other.gameObject.GetComponent<SpeedBoost>();
         OnCollectSpeedBoost(boost);
      }
      if (other.gameObject.CompareTag("FireGround"))
      {
         var fireGround = other.gameObject.GetComponent<FireGround>();
         OnHitFireGround(fireGround);
      }
      if (other.gameObject.CompareTag("CubeBlock"))
      {
         var block = other.gameObject.GetComponent<CubeBlock>();
         OnHitCubeBlock(block);
      }

      if (other.gameObject.CompareTag("RampRoad") || other.gameObject.CompareTag("JumpRamp"))
      {
         var road = other.gameObject.GetComponent<Ramp>();
         OnTriggerRamp(road);
      }

      if (other.gameObject.CompareTag("Finish"))
      {
         stateMachine.ChangeState(2);
      }
      
   }

   private void OnTriggerCube(Cube cube)
   {
      cubeHandler.OnTriggerCube(cube,playerModel);
   }
   private void SetTrailParticle(bool isActive)
   {
      if (cubeHandler.cubes.Count < 1 || !isActive)
      {
         colorTrail.Stop();
      }
      else
      {
         switch (cubeHandler.cubes[^1].colorId)
         {
            case 0:
               colorTrail.SetStartColor(new Color32(47, 150, 243, 255));
               break;
            case 1:
               colorTrail.SetStartColor(new Color32(166, 107, 34, 255));
               break;
            case 2: 
               colorTrail.SetStartColor(new Color32(173, 35, 255, 255));
               break;
         }
         colorTrail.transform.localPosition = cubeHandler.cubes[^1].transform.localPosition + new Vector3(0, -.75f, 0);
         if (isActive)
         {
            colorTrail.Play();
         }
         
      }
   }
   private void OnTriggerRamp(Ramp ramp)
   {
      if (ramp.canTrig)
      {
         StopMove();
         ramp.OnTriggerRoad();
         splineFollower.spline = ramp.splineComputer;
         splineFollower.followSpeed = ramp.speed;
         splineFollower.motion.offset = new Vector2(0, cubeHandler.cubes.Count * 1.5f + 1.2f);
         splineFollower.startPosition = 0;
         splineFollower.Restart();
         splineFollower.RebuildImmediate();
         if (ramp.id == 1)
         {
            colorTrail.Stop();
         }
         splineFollower.follow = true;
         
      }
   }

   public void OnExitRamp()
   {
      splineFollower.follow = false;
      var playerModelTransform = playerModel.transform;
      playerModelTransform.localPosition = new Vector3(playerModelTransform.position.x, cubeHandler.cubes.Count * 1.5f,
         playerModelTransform.position.z);
      StartMove();
      SetTrailParticle(true);
   }
   private void OnHitCubeBlock(CubeBlock block)
   {
      if (block.canHit)
      {
         block.OnHit();
         cubeHandler.RemoveCubes(block.height, feverMode, playerModel);
         if (feverMode)
         {
            block.parentObject.SetActive(false);
         }
      }
   }

   private void OnHitFireGround(FireGround fireGround)
   {
      if (fireGround.canHit)
      {
         fireGround.OnHit();
         cubeHandler.RemoveCubes(1, feverMode, playerModel);
      }
   }

   private void OnCollectSpeedBoost(SpeedBoost boost)
   {
      if (boost.canCollect)
      {
         boost.OnCollect();
         boostDuration = boost.duration;
         feverMode = true;
         playerMovement.SetSpeed(12f);
         
      }
   }
   
   private void OnPassRandomGate(RandomGate gate)
   {
      if (gate.canPass)
      {
         gate.OnPass();
         List<Cube> currentCubes = new List<Cube>();
         currentCubes.AddRange(cubeHandler.cubes);
         int totalBlue = 0, totalOrange = 0, totalPurple = 0;
         for (int i = 0; i < currentCubes.Count; i++)
         {
            switch (currentCubes[i].colorId)
            {
               case 0:
                  totalBlue += 1;
                  break;
               case 1:
                  totalOrange += 1;
                  break;
               case 2:
                  totalPurple += 1;
                  break;
            }
         }

         for (int i = 0; i < cubeHandler.cubes.Count; i++)
         {
            var targetCube = currentCubes[Random.Range(0, currentCubes.Count)];
            if (totalBlue > 0)
            {
               --totalBlue;
               targetCube.colorId = 0;
            }
            else if (totalOrange > 0)
            {
               targetCube.colorId = 1;
               --totalOrange;
            }
            else if (totalPurple > 0)
            {
               targetCube.colorId = 2;
               --totalPurple;
            }
            targetCube.Initialize();
            currentCubes.Remove(targetCube);
         }
         cubeHandler.MatchCheck(playerModel);
      }
   }
   
   private void OnPassOrderGate(OrderGate gate)
   {
      if (gate.canPass)
      {
         gate.OnPass();
         int totalBlue = 0, totalOrange = 0, totalPurple = 0;
         for (int i = 0; i < cubeHandler.cubes.Count; i++)
         {
            switch (cubeHandler.cubes[i].colorId)
            {
               case 0:
                  totalBlue += 1;
                  break;
               case 1:
                  totalOrange += 1;
                  break;
               case 2:
                  totalPurple += 1;
                  break;
            }
         }
         for (int i = 0; i < cubeHandler.cubes.Count; i++)
         {
            var targetCube = cubeHandler.cubes[i];
            if (totalBlue > 0)
            {
               --totalBlue;
               targetCube.colorId = 0;
            }
            else if (totalOrange > 0)
            {
               targetCube.colorId = 1;
               --totalOrange;
            }
            else if (totalPurple > 0)
            {
               targetCube.colorId = 2;
               --totalPurple;
            }
            targetCube.Initialize();
         }
         cubeHandler.MatchCheck(playerModel);
      }
   }
   
 
}
