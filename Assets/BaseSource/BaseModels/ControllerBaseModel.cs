using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBaseModel : ObjectModel
{




    public virtual void ResetController()
    {

    }

    protected virtual void Reset()
    {
        transform.name = GetType().Name;
        transform.ResetLocal();
    }
}