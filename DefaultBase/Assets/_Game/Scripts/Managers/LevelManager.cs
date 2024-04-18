using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager I;
    
    public GameObject[] levelPrefabs;
    
    public LevelController currentLevel;


    public bool isRestarted;

    public int levelNumber;

    public TutorialController tutorialController;

    private void Awake()
    {
        I = this;
    }


    private void Start()
    {
        SetLevel();
        GameManager.I.OnGamePhaseChange += OnOnGamePhaseChange;
    }

    private void OnOnGamePhaseChange(GamePhase obj)
    {
        if (obj == GamePhase.Menu)
        {
            SetLevel();
        }
        
    }

    public void SetLevel()
    {
        if (!SaveLoad.I.playerProgress.tutorialFinished)
        {
            //tutorialController.OpenTutorialLevel();
        }
        else
        {
            if (!isRestarted)
            {
                if (SaveLoad.I.playerProgress.currentLevel < levelPrefabs.Length)
                {
                    levelNumber = SaveLoad.I.playerProgress.currentLevel;
                }
                else
                {
                    levelNumber = Random.Range(0, levelPrefabs.Length);
                }
            }
        
            isRestarted = false;

            OpenLevel();
        }

     

    }


    private void OpenLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        
        var levelGO = Instantiate(levelPrefabs[levelNumber]);
        currentLevel = levelGO.GetComponent<LevelController>();
        currentLevel.SetLevel();
    }

}
