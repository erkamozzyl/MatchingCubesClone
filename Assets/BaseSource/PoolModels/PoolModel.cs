using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PoolModel : ObjectModel
{
    [SerializeField] List<ObjectModel> items;

    public virtual T GetDeactiveItem<T>()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].gameObject.activeInHierarchy == false)
            {
                return (T) ((object) items[i]);
            }
        }

        return default(T);
    }

    private void getItemsFromChilds()
    {
        if (items == null)
            items = new List<ObjectModel>();

        for (int i = 0; i < transform.childCount; i++)
        {
            ObjectModel item = transform.GetChild(i).GetComponent<ObjectModel>();
            if (item != null)
            {
                if (item.HasComponent<ItemModel>())
                    item.GetComponent<ItemModel>().id = item.transform.parent.GetSiblingIndex();
                item.SetDeactive();
                items.Add(item);
            }
        }
    }

  
    public void InitializeOnEditor()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "GetItems");
        if (items != null)
            items.Clear();

        getItemsFromChilds();
#endif
    }

    private void Reset()
    {
        transform.ResetLocal();
    }
}