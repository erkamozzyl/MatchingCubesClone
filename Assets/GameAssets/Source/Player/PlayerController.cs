using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : ControllerBaseModel
{
   [SerializeField] private PlayerModel playerModel;
   [SerializeField] private PlayerMovement playerMovement;
   [SerializeField] private List<Cube> cubes;
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

   private void StopMove()
   {
      playerMovement.Stop();
   }
   
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.A))
      {
         RemoveCubes(1);
      }
      if (Input.GetKeyDown(KeyCode.B))
      {
         RemoveCubes(2);
      }

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

     
   }

   private void OnCollectSpeedBoost(SpeedBoost boost)
   {
      boost.OnCollect();
      boostDuration = boost.duration;
      feverMode = true;
      playerMovement.SetSpeed(12f);
   }

   private void OnPassRandomGate(RandomGate gate)
   {
      if (gate.canPass)
      {
         gate.OnCollect();
         List<Cube> currentCubes = new List<Cube>();
         currentCubes.AddRange(cubes);
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

         for (int i = 0; i < cubes.Count; i++)
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
         MatchCheck();
      }
   }
   private void OnPassOrderGate(OrderGate gate)
   {
      if (gate.canPass)
      {
         gate.OnCollect();
         int totalBlue = 0, totalOrange = 0, totalPurple = 0;
         for (int i = 0; i < cubes.Count; i++)
         {
            switch (cubes[i].colorId)
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
         for (int i = 0; i < cubes.Count; i++)
         {
            var targetCube = cubes[i];
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
         MatchCheck();
      }
   }
  

   private void OnTriggerCube(Cube cube)
   {
      if (cube.canCollect)
      {
         cube.transform.parent = playerModel.transform;
         cube.OnCollect();
         cubes.Add(cube);
         playerModel.AddVector3ToPosition(new Vector3(0, cube.heightOffset, 0));
         for (int i = 0; i < cubes.Count; i++)
         {
            cubes[i].ScaleAnimation(i/15f,1.5f);
         }
         MatchCheck();
      }
   }

   private void RemoveCubes(int count)
   {
      int removedCubeCounter = 0;
      for (int j = cubes.Count - 1; j >= 0 ; j--)
      {
         if (removedCubeCounter < count)
         {
            cubes[j].ScaleAnimation(0,() =>
            {
               playerModel.AddVector3ToPosition(new Vector3(0, -1.5f, 0));
            });
            removedCubeCounter += 1;
         }
      }
      cubes.RemoveRange(cubes.Count - count, count);
   }
   private void MatchCheck()
   {
      for (int i = 0; i < cubes.Count - 2; i++)
      {
         int targetCubeColor = cubes[i].colorId;
         if (targetCubeColor == cubes[i + 1].colorId)
         {
            if (targetCubeColor == cubes[i + 2].colorId)
            {
               for (int j = i; j < 3 + i; j++)
               {
                  cubes[j].ScaleAnimation(0,() =>
                  {
                     playerModel.AddVector3ToPosition(new Vector3(0, -1.5f, 0));
                  });
               }
               cubes.RemoveRange(i, 3);
               for (int x = 0; x < cubes.Count; x++)
               {
                  if (i <= x)
                  {
                     cubes[x].AddVector3ToPosition( new Vector3(0,  3 * cubes[x].heightOffset, 0));
                  }
               }
               MatchCheck();
            }
         }
      }
   }
}
