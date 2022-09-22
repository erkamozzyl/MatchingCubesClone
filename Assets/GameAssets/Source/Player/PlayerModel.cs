using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : ObjectModel
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public void AddVector3ToPosition(Vector3 position)
    {
        transform.position += position;
    }
    
    
}
