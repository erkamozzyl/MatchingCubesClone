using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePoolModel : ObjectModel
{
    public List<PoolModel> Pools;
    [HideInInspector] public int Index;

    public  void Initialize()
    {
        foreach (var item in Pools)
        {
            item.Initialize();
        }
    }
    public GameObject GetRandomDeactiveItem()
    {
        int index = Random.Range(0, Pools.Count);
        return Pools[index].GetDeactiveItem();
    }
    public T GetDeactiveItem<T>(int index)
    {
        return (T)(object)Pools[index].GetDeactiveItem();
    }

    public GameObject GetLinearDeactiveItem()
    {
        GameObject model = Pools[Index].GetDeactiveItem();
        Index = (Index + 1 < Pools.Count) ? Index + 1 : 0;
        return model;
    }

    public void ResetPool()
    {
        foreach (var item in Pools)
        {
            item.SetDeactiveItems();
        }
    }

}