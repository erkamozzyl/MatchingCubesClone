using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class DataHandler : HandlerBaseModel
{
    public PlayerDataModel Player;
    public override void Initialize()
    {
        base.Initialize();
        Player = new PlayerDataModel().Load();
    }

#if UNITY_EDITOR
  
    public void ClearAllData()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.dat");
        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }

        PlayerPrefs.DeleteAll();

        if (Directory.GetFiles(Application.persistentDataPath, "*.dat").Length == 0)
        {
            Debug.Log("Data Clear Successed");
        }
    }
#endif
}

public class Data
{
    public List<BaseData> Datas;
}

public class BaseData
{

}