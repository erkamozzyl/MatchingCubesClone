using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PoolModel : ObjectModel
{
    public List<GameObject> Items;
    public int ItemCount;
    public bool GetChildsOnOnInit;

    public override void Initialize()
    {
        if (Items == null)
            Items = new List<GameObject>();

        if (GetChildsOnOnInit)
            GetItemsFromChilds();

        ItemCount = Items.Count;

      //  InitializeItems();

     
    }

    protected virtual void InitializeItems()
    {
        foreach (var item in Items)
        {
            //   item.Initialize();
        }
    }

    public virtual GameObject GetDeactiveItem()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].gameObject.activeInHierarchy == false)
            {
                return Items[i];
            }
        }

        return null;
    }

    public virtual void SetDeactiveItems()
    {
        foreach (var item in Items)
        {
            item.SetActive(false);
        }
    }

    protected virtual void GetItemsFromChilds()
    {
        if (Items == null)
            Items = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject item = transform.GetChild(i).GetComponent<GameObject>();
            if (item != null)
            {
                Items.Add(item);
            }
        }
    }

    public void GetItemsEditor()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "GetItems");
        if (Items != null)
            Items.Clear();

        GetItemsFromChilds();
#endif
    }

    public void SetActiveWithPos(Vector3 pos)
    {
        GameObject item = GetDeactiveItem();

        if (item != null)
        {
            item.transform.position = pos;
            item.SetActive(true);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PoolModel))]
public class PoolModelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Get Items"))
        {
            ((PoolModel)target).GetItemsEditor();
        }
    }
}

#endif