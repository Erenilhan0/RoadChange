using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelStageController : MonoBehaviour
{

    [SerializeField] private Button button;
    [SerializeField] private GameObject ActiveLevelGO;

    [SerializeField] private GameObject UnlockedLevelGO;
    [SerializeField] private GameObject LockedLevelGO;



    public int LevelNumber;

    [SerializeField] private TextMeshProUGUI LevelText;

    private void Start()
    {
        button.onClick.AddListener(ButtonClicked);

    }

    public void SetLevelStage(int levelNumber)
    {
        LevelNumber = levelNumber;
        LevelText.text = LevelNumber.ToString();
    }


    public void SetActiveLevel(bool active)
    {
        ActiveLevelGO.SetActive(active);
    }
    

    public void LockedLevel()
    {
        LockedLevelGO.SetActive(true);
        UnlockedLevelGO.SetActive(false);
        button.interactable = false;
    }

    public void UnlockLevel()
    {
        LockedLevelGO.SetActive(false);
        UnlockedLevelGO.SetActive(true);
        
        button.interactable = true;
        
    }

    public void ButtonClicked()
    {
        LevelManager.I.SetActiveLevel(LevelNumber);
    }
}
