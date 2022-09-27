using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LevelItemsHandler : MonoBehaviour
{
   public MultiplePoolModel collectables;
   public MultiplePoolModel obstacles;
   public MultiplePoolModel cubes;
   public MultiplePoolModel gates;
   public MultiplePoolModel ramps;

   public GameObject GetItem(int id,ItemType itemType )
   {
      switch (itemType)
      {
         case ItemType.Collectable:
            return collectables.GetDeactiveItem<GameObject>(id);
         case ItemType.Obstacle:
            return obstacles.GetDeactiveItem<GameObject>(id);
         case ItemType.Cube:
            return cubes.GetDeactiveItem<GameObject>(id);
         case ItemType.Gate:
            return gates.GetDeactiveItem<GameObject>(id);
         case ItemType.Ramp:
            return ramps.GetDeactiveItem<GameObject>(id);
         default:
            return cubes.GetDeactiveItem<GameObject>(id);
      }
   }
   public enum ItemType
   {
      Collectable,
      Obstacle,
      Cube,
      Gate,
      Ramp
   }
 
}
