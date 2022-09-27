using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : ObjectModel
{
   public List<Cube> cubes;
   [SerializeField] private ParticleSystem colorTrail;
   [SerializeField] private StateMachineMono stateMachine;



   public void OnTriggerCube(Cube cube, PlayerModel playerModel)
   {
      if (cube.canCollect)
      {
         cube.transform.parent = playerModel.transform;
         cube.OnCollect();
         cubes.Add(cube);
         playerModel.AddVector3ToPosition(new Vector3(0, cube.heightOffset, 0));
         for (int i = 0; i < cubes.Count; i++)
         {
            cubes[i].ScaleAnimation(i / 15f, 1.5f);
         }

         SetTrailParticle(true);
         MatchCheck(playerModel);
      }
   }

   public void RemoveCubes(int count, bool onFeverMode, PlayerModel playerModel)
   {
      SetTrailParticle(false);
      int removedCubeCounter = 0;
      if (!onFeverMode)
      {
         if (cubes.Count >= count)
         {
            for (int j = cubes.Count - 1; j >= 0; j--)
            {
               if (removedCubeCounter < count)
               {
                  cubes[j].ScaleAnimation(0, () =>
                  {
                     playerModel.AddVector3ToPosition(new Vector3(0, -1.5f, 0));
                     SetTrailParticle(true);
                  });
                  removedCubeCounter += 1;
               }
            }

            cubes.RemoveRange(cubes.Count - count, count);
         }
         else
            stateMachine.ChangeState(3);
      }
   }

   public void MatchCheck(PlayerModel playerModel)
   {
      for (int i = 0; i < cubes.Count - 2; i++)
      {
         int targetCubeColor = cubes[i].colorId;
         if (targetCubeColor == cubes[i + 1].colorId)
         {
            if (targetCubeColor == cubes[i + 2].colorId)
            {
               SetTrailParticle(false);
               for (int j = i; j < 3 + i; j++)
               {
                  cubes[j].ScaleAnimation(0, () =>
                  {
                     playerModel.AddVector3ToPosition(new Vector3(0, -1.5f, 0));
                     SetTrailParticle(true);
                  });
               }

               cubes.RemoveRange(i, 3);
               for (int x = 0; x < cubes.Count; x++)
               {
                  if (i <= x)
                  {
                     cubes[x].AddVector3ToPosition(new Vector3(0, 3 * cubes[x].heightOffset, 0));
                  }
               }

               MatchCheck(playerModel);
            }
         }
      }
   }

   private void SetTrailParticle(bool isActive)
   {
      if (cubes.Count < 1 || !isActive)
      {
         colorTrail.Stop();
      }
      else
      {
         switch (cubes[^1].colorId)
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

         colorTrail.transform.localPosition = cubes[^1].transform.localPosition + new Vector3(0, -.75f, 0);
         if (isActive)
         {
            colorTrail.Play();
         }

      }
   }
}
