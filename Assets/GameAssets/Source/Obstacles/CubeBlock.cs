using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBlock : ObstacleBaseModel
{
    public int height;
    public override void OnHit()
    {
        canHit = false;
    }
}
