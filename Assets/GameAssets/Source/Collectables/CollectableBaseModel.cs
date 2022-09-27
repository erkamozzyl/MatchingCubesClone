using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableBaseModel : ObjectModel
{
    public int id;
    public bool canCollect;
    public GameObject parentObject;
    public abstract void OnCollect();
    public ItemDataModel GetData()
    {
        ItemDataModel dataModel = new ItemDataModel();
        dataModel.id = id;
        dataModel.position = parentObject.transform.position;
        dataModel.rotation = parentObject.transform.rotation;
        return dataModel;
    }
}
