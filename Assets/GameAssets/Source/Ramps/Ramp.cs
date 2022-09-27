using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public abstract class Ramp : ObjectModel
{
    public int id;
    public bool canTrig;
    public GameObject parentObject;
    public SplineComputer splineComputer;
    public float speed;
    public ItemDataModel GetData()
    {
        ItemDataModel dataModel = new ItemDataModel();
        dataModel.id = id;
        dataModel.position = parentObject.transform.position;
        dataModel.rotation = parentObject.transform.rotation;
        return dataModel;
    }
    public abstract void OnTriggerRoad();
}
