using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameLevel
{
    public ItemDataModel[] Collectables;
    public ItemDataModel[] Obstacles;
    public CubeItemDataModel[] Cubes;
    public ItemDataModel[] Gates;
    public ItemDataModel[] Ramps;
}
