using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRamp : Ramp
{
    public override void OnTriggerRoad()
    {
        canTrig = false;
    }
}

