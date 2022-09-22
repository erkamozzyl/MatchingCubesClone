using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : ControllerBaseModel
{
   [SerializeField] private PlayerModel playerModel;
   [SerializeField] private PlayerMovement playerMovement;
   [SerializeField] private List<Cube> cubes;
   
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
            cubes[i].ScaleAnimation(i/15f);
         }
      }
   }
}
