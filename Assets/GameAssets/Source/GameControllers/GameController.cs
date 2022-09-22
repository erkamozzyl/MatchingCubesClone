using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : ControllerBaseModel
{
   public override void Initialize()
   {
      base.Initialize();
      Application.targetFrameRate = 60;
   }
}
