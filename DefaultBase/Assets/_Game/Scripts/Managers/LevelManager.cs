using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager I;
    

    public LevelStageController[] levels;
    public LevelStageController activeLevel;


    public int levelNumber;

    public TutorialController tutorialController;


    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        SetLevelsData();
    }


    private void SetLevelsData()
    {
        var maxLevel = SaveLoad.I.playerProgress.maxLevel;

        var currentLevel = SaveLoad.I.playerProgress.currentLevel;
        
        for (int i = 0; i < levels.Length; i++)
        {
            var level = levels[i];
            level.SetLevelStage(i + 1);

            if (i == currentLevel)
            {
                activeLevel = level;
                level.SetActiveLevel(true);
            }
            else if (i > maxLevel)
            {
                level.LockedLevel();
            }
        }


        OpenLevel();
    }


    public void SetActiveLevel(int level)
    {
        activeLevel.SetActiveLevel(false);
        
        var selectedLevel = levels[level - 1];
        
        activeLevel = selectedLevel;
        
        activeLevel.SetActiveLevel(true);
        
        
        SaveLoad.I.playerProgress.currentLevel = level - 1;

    }

    private void OpenLevel()
    {
        // var levelGO = Instantiate(levelPrefabs[levelNumber]);
        // currentLevel = levelGO.GetComponent<LevelController>();
        // currentLevel.SetLevel();
    }
}