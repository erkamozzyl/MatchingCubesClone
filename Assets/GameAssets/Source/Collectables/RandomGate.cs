using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGate : Gate
{
    public override void OnCollect()
    {
        canPass = false;
    }
}
