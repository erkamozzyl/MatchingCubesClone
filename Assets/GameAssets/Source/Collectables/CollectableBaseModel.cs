using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableBaseModel : ObjectModel
{
    public bool canCollect;
    public abstract void OnCollect();
}
