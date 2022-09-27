using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : ControllerBaseModel
{
    [SerializeField] private List<GameObject> screens;
    [SerializeField] private List<Text> levelTexts;
    
    public override void Initialize()
    {
        base.Initialize();
        ActivateScreen(0);
        UpdateLevelText();
    }

    public void ActivateScreen(int index)
    {
        for (int i = 0; i < screens.Count; i++)
        {
            if (i == index)
            {
                screens[i].SetActive(true);
            }else
                screens[i].SetActive(false);
        }
    }

    public void UpdateLevelText()
    {
        foreach (var text in levelTexts)
        {
            text.text = $"LEVEL {PlayerDataModel.Data.Level}";
        }
    }
    
}
