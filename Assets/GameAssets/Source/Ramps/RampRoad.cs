using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampRoad : Ramp
{
    public override void OnTriggerRoad()
    {
        canTrig = false;
    }
}
