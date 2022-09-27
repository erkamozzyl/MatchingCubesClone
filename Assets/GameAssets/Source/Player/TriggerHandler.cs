using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using UnityEngine;

public class TriggerHandler : ObjectModel
{
   [SerializeField] private PlayerController playerController;
   [SerializeField] private PlayerMovement playerMovement;
   [SerializeField] private CubeHandler cubeHandler;
   [SerializeField] private PlayerModel playerModel;
   [SerializeField] private SplineFollower splineFollower;
   [SerializeField] private ParticleSystem colorTrail;
   
   
   public void OnTriggerCube(Cube cube)
   {
      cubeHandler.OnTriggerCube(cube,playerModel);
   }
   
   public void OnTriggerRamp(Ramp ramp)
   {
      if (ramp.canTrig)
      {
         playerMovement.Stop();
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
      playerMovement.Move();
      SetTrailParticle(true);
   }
   
   public void OnHitCubeBlock(CubeBlock block)
   {
      if (block.canHit)
      {
         block.OnHit();
         cubeHandler.RemoveCubes(block.height, playerController.feverMode, playerModel);
         if (playerController.feverMode)
         {
            block.parentObject.SetActive(false);
         }
      }
   }

   public void OnHitFireGround(FireGround fireGround)
   {
      if (fireGround.canHit)
      {
         fireGround.OnHit();
         cubeHandler.RemoveCubes(1, playerController.feverMode, playerModel);
      }
   }

   public void OnCollectSpeedBoost(SpeedBoost boost)
   {
      if (boost.canCollect)
      {
         boost.OnCollect();
         playerController.boostDuration = boost.duration;
         playerController.feverMode = true;
         playerMovement.SetSpeed(12f);
         
      }
   }
   
   public void OnPassRandomGate(RandomGate gate)
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
   
   public void OnPassOrderGate(OrderGate gate)
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

   private void SetTrailParticle(bool isActive)
   {
      if (cubeHandler.cubes.Count < 1 || !isActive)
      {
         colorTrail.Stop();
      }
      else
      {
         switch (cubeHandler.cubes.Last().colorId)
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
         colorTrail.transform.localPosition = cubeHandler.cubes.Last().transform.localPosition + new Vector3(0, -.75f, 0);
         if (isActive)
         {
            colorTrail.Play();
         }
         
      }
   }
}
