using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gate : ObjectModel
{
    public int id;
    public bool canPass;
    public GameObject parentObject;
    public abstract  void OnPass();
    public ItemDataModel GetData()
    {
        ItemDataModel dataModel = new ItemDataModel();
        dataModel.id = id;
        dataModel.position = parentObject.transform.position;
        dataModel.rotation = parentObject.transform.rotation;
        return dataModel;
    }
}
