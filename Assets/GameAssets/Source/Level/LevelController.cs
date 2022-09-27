using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class LevelController : MonoBehaviour
{
    [FormerlySerializedAs("levelItems")] [SerializeField] private LevelItemsHandler levelItemsHandler;
    [SerializeField] private List<GameLevel> levels;
    [SerializeField] private int currentLevelIdx;
    public GameLevel currentLevel;
    [Space(20)]
    public int EditorLoadLevelIdx;
    

    public void InstantiateLevel()
    {
       
        var data = PlayerDataModel.Data;
        if (levels.Count <= data.LevelIndex)
        {
            data.LevelIndex = 0;
            currentLevel = levels[(int)data.LevelIndex];
            currentLevelIdx = (int)data.LevelIndex;
            GetLevelItems((int)data.LevelIndex);
        }
        else
        {
            currentLevel = levels[(int)data.LevelIndex];
            currentLevelIdx = (int)data.LevelIndex;
            GetLevelItems((int)data.LevelIndex);
        }
        
    }

    private void GetLevelItems(int idx)
    {
       
        for (int i = 0; i < levels[idx].Collectables.Length; i++)
        {
          var instance =  levelItemsHandler.GetItem(levels[idx].Collectables[i].id, LevelItemsHandler.ItemType.Collectable);
          instance.transform.position = levels[idx].Collectables[i].position;
          instance.transform.rotation = levels[idx].Collectables[i].rotation;
          instance.SetActive(true);
        }
        
        for (int i = 0; i < levels[idx].Obstacles.Length; i++)
        {
            var instance =  levelItemsHandler.GetItem(levels[idx].Obstacles[i].id, LevelItemsHandler.ItemType.Obstacle);
            instance.transform.position = levels[idx].Obstacles[i].position;
            instance.transform.rotation = levels[idx].Obstacles[i].rotation;
            instance.SetActive(true);
        }
        
        for (int i = 0; i < levels[idx].Cubes.Length; i++)
        {
            var instance =  levelItemsHandler.GetItem(levels[idx].Cubes[i].id, LevelItemsHandler.ItemType.Cube);
            instance.transform.position = levels[idx].Cubes[i].position;
            instance.transform.rotation = levels[idx].Cubes[i].rotation;
            var cube = instance.GetComponent<Cube>();
            cube.colorId = levels[idx].Cubes[i].colorID;
            instance.SetActive(true);
        }
        
        for (int i = 0; i < levels[idx].Gates.Length; i++)
        {
            var instance =  levelItemsHandler.GetItem(levels[idx].Gates[i].id, LevelItemsHandler.ItemType.Gate);
            instance.transform.position = levels[idx].Gates[i].position;
            instance.transform.rotation = levels[idx].Gates[i].rotation;
            instance.SetActive(true);
        }
        for (int i = 0; i < levels[idx].Ramps.Length; i++)
        {
            var instance =  levelItemsHandler.GetItem(levels[idx].Ramps[i].id, LevelItemsHandler.ItemType.Ramp);
            instance.transform.position = levels[idx].Ramps[i].position;
            instance.transform.rotation = levels[idx].Ramps[i].rotation;
            instance.SetActive(true);
        }
       
    }

    public void E_SaveLevel()
    {
        GameLevel level = new GameLevel();
        CollectableBaseModel[] collectables = FindObjectsOfType<CollectableBaseModel>();
        ObstacleBaseModel[] obstacles = FindObjectsOfType<ObstacleBaseModel>();
        Cube[] cubes = FindObjectsOfType<Cube>();
        Gate[] gates = FindObjectsOfType<Gate>();
        Ramp[] ramps = FindObjectsOfType<Ramp>();
        
        level.Collectables = new ItemDataModel[collectables.Length];
        level.Obstacles = new ItemDataModel[obstacles.Length];
        level.Cubes = new CubeItemDataModel[cubes.Length];
        level.Gates = new ItemDataModel[gates.Length];
        level.Ramps = new ItemDataModel[ramps.Length];
        
        for (int i = 0; i < collectables.Length; i++)
        {
            level.Collectables[i] = collectables[i].GetData();
        }
        for (int i = 0; i < obstacles.Length; i++)
        {
            level.Obstacles[i] = obstacles[i].GetData();
        }
        for (int i = 0; i < cubes.Length; i++)
        {
            level.Cubes[i] = cubes[i].GetData();
        }
        for (int i = 0; i < gates.Length; i++)
        {
            level.Gates[i] = gates[i].GetData();
        }
        for (int i = 0; i < ramps.Length; i++)
        {
            level.Ramps[i] = ramps[i].GetData();
        }
        levels.Add(level);
    }
    
    public void LoadLevelByIdx(int idx)
    {
        currentLevel = levels[idx];
        GetLevelItems(idx);
    }

}
#if UNITY_EDITOR
[CustomEditor(typeof(LevelController))]
public class LevelEditor : Editor
{
    LevelController LevelController
    {
        get
        {
            return (LevelController)target;
        }
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Color color = GUI.color;

        GUI.color = Color.green;
        if (GUILayout.Button("Save Level"))
        {
             LevelController.E_SaveLevel();
        }

      
        GUI.color = Color.cyan;
        if (GUILayout.Button("Load Level"))
        {
            LevelController.LoadLevelByIdx(LevelController.EditorLoadLevelIdx);
        }
        GUI.color = Color.red;
        if (GUILayout.Button("Reset Scene"))
        {
           
            foreach (var collectable in FindObjectsOfType<CollectableBaseModel>())
            {
                collectable.parentObject.SetActive(false);
            }
            foreach (var obstacle in FindObjectsOfType<ObstacleBaseModel>())
            {
                obstacle.parentObject.SetActive(false);
            }
            foreach (var cube in FindObjectsOfType<Cube>())
            {
                cube.parentObject.SetActive(false);
            }
            foreach (var gate in FindObjectsOfType<Gate>())
            {
                gate.parentObject.SetActive(false);
            }
            foreach (var ramp in FindObjectsOfType<Ramp>())
            {
                ramp.parentObject.SetActive(false);
            }
           
        }
        

        GUI.color = color;

       

    }
}
#endif
