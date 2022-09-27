using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGate : Gate
{
    public override void OnPass()
    {
        canPass = false;
    }
}
