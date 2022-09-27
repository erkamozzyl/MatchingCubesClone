using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBaseModel : ObjectModel
{
    public int id;
    public bool canHit;
    public GameObject parentObject;
    public abstract void OnHit();
    public ItemDataModel GetData()
    {
        ItemDataModel dataModel = new ItemDataModel();
        dataModel.id = id;
        dataModel.position = parentObject.transform.position;
        dataModel.rotation = parentObject.transform.rotation;
        return dataModel;
    }
}
