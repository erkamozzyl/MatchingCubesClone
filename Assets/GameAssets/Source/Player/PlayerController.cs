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
         boostDuration = boost.duration;
         
      }

     
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
         CheckMatch();
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
         CheckMatch();
      }
   }
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.A))
      {
         CheckMatch();
      }
      if (Input.GetKeyDown(KeyCode.B))
      {
         for (int i = 0; i < cubes.Count; i++)
         {
            cubes[i].colorId = 0;
            cubes[i].Initialize();
         }
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
            cubes[i].CollectScaleAnimation(i/15f);
         }
         CheckMatch();
      }
   }

   private void CheckMatch()
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
                  cubes[j].OnMatch((() =>
                  {
                     playerModel.AddVector3ToPosition(new Vector3(0, -1.5f, 0));
                  }));
               }
               cubes.RemoveRange(i, 3);
               for (int x = 0; x < cubes.Count; x++)
               {
                  if (i <= x)
                  {
                     cubes[x].transform.localPosition += new Vector3(0,  3 * cubes[x].heightOffset, 0);
                  }
               }
               CheckMatch();
            }
         }
      }
   }
}
