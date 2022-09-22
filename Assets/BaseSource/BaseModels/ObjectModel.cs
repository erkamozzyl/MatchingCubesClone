using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectModel : MonoBehaviour
{
    [HideInInspector] public bool IsActiveInHierarchy;

    public virtual void Initialize()
    {
    }

    public virtual void SetActive()
    {
        IsActiveInHierarchy = true;
        gameObject.SetActive(true);
    }

    public virtual void SetDeactive()
    {
        IsActiveInHierarchy = false;
        gameObject.SetActive(false);
    }
}