using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gate : CollectableBaseModel
{
    public bool canPass;
    public abstract override void OnCollect();
}
