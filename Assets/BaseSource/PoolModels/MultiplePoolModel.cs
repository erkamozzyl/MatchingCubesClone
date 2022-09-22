using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePoolModel : ObjectModel
{
    public List<PoolModel> SubPools => pools;
    [SerializeField] List<PoolModel> pools;

    public virtual T GetDeactiveItem<T>(int poolIndex)
    {
        return pools[poolIndex].GetDeactiveItem<T>();
    }

    public virtual T GetRandomPoolItem<T>()
    {
        return pools.GetRandom().GetDeactiveItem<T>();
    }

    
    public void GetPools()
    {
        pools.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            PoolModel pool = transform.GetChild(i).GetComponent<PoolModel>();
            if (pool != null)
            {
                pools.Add(pool);
                
            }
        }
    }

  
}
