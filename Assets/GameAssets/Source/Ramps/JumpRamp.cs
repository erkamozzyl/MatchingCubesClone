using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRamp : Ramp
{
    private void Start()
    {
        splineComputer.RebuildImmediate();
    }
    public override void OnTriggerRoad()
    {
        canTrig = false;
    }
}

