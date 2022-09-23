using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : CollectableBaseModel
{
    public bool canCollect;
    public float duration;
    public override void OnCollect()
    {
        canCollect = false;
    }
}
