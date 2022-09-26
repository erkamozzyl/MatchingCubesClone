using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGround : ObstacleBaseModel
{
    public override void OnHit()
    {
        canHit = false;
    }
}
