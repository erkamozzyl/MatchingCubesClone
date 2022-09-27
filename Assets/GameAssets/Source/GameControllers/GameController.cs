using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : ControllerBaseModel
{
   [SerializeField] private LevelController levelController;
   public override void Initialize()
   {
      base.Initialize();
      Application.targetFrameRate = 60;
      LevelDataControl();
      LoadLevel();
   }
   public void ReloadScene()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   private void LoadLevel()
   {
      if (levelController.currentLevel != null)
      {
         levelController.InstantiateLevel();
      }else
         levelController.InstantiateLevel();
        
   }
   public void Win()
   {
      var data = PlayerDataModel.Data;
      data.Level++;
      data.LevelIndex++;
      data.Save();
   }

 
   private void LevelDataControl()
   {
      var data = PlayerDataModel.Data;
      if (data.Level == 0)
      {
         data.Level = 1;
         data.Save();
      }
        
   }
}
