using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBaseModel : ObjectModel
{
    public bool canHit;
    public abstract void OnHit();
}
