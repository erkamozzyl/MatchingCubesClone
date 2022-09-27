using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Dreamteck.Splines;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : ControllerBaseModel
{
   [SerializeField] private StateMachineMono stateMachine;
   [SerializeField] private PlayerMovement playerMovement;
   [SerializeField] private TriggerHandler triggerHandler;
   public bool feverMode;
   public float boostDuration;
   
   public override void Initialize()
   {
      base.Initialize();
      playerMovement.Move();
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
         triggerHandler.OnTriggerCube(cube);
      }

      if (other.gameObject.CompareTag("RandomGate"))
      {
         var gate = other.gameObject.GetComponent<RandomGate>();
         triggerHandler.OnPassRandomGate(gate);
      }
      if (other.gameObject.CompareTag("OrderGate"))
      {
         var gate = other.gameObject.GetComponent<OrderGate>();
         triggerHandler.OnPassOrderGate(gate);
      }

      if (other.gameObject.CompareTag("SpeedBoost"))
      {
         var boost = other.gameObject.GetComponent<SpeedBoost>();
         triggerHandler.OnCollectSpeedBoost(boost);
      }
      if (other.gameObject.CompareTag("FireGround"))
      {
         var fireGround = other.gameObject.GetComponent<FireGround>();
         triggerHandler.OnHitFireGround(fireGround);
      }
      if (other.gameObject.CompareTag("CubeBlock"))
      {
         var block = other.gameObject.GetComponent<CubeBlock>();
        triggerHandler.OnHitCubeBlock(block);
      }

      if (other.gameObject.CompareTag("RampRoad") || other.gameObject.CompareTag("JumpRamp"))
      {
         var road = other.gameObject.GetComponent<Ramp>();
         triggerHandler.OnTriggerRamp(road);
      }

      if (other.gameObject.CompareTag("Finish"))
      {
         stateMachine.ChangeState(2);
      }
      
   }
}
