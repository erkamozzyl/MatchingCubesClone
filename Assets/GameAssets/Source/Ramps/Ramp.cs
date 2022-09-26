using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public abstract class Ramp : ObjectModel
{
    public bool canTrig;
    public SplineComputer splineComputer;
    public float speed;
    public abstract void OnTriggerRoad();
}
