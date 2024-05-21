using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    Menu,
    Game,
    End
}

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public event Action<GamePhase> OnGamePhaseChange;
    public GamePhase currentGamePhase;

    public int GoldCollectedThisLevel;


    private int finishedDoorCount;

    [SerializeField] private LevelController LevelController;

    private void Awake()
    {
        I = this;
    }


    public void ChangeGameState(GamePhase to)
    {
        currentGamePhase = to;
        OnGamePhaseChange?.Invoke(to);
    }


    public void OnClickNextLevel()
    {
        SaveLoad.I.playerProgress.currentLevel++;
        CurrencyManager.GainGold(GoldCollectedThisLevel);
        SaveLoad.I.SaveToJson();
        /*
        GameAnalytics.NewProgressionEvent
        (GAProgressionStatus.Complete, SaveLoad.I.playerProgress.curentLevel.ToString(), 0);
        */

        StartLevel();
    }

    public void StartLevel()
    {
        GoldCollectedThisLevel = 0;
        ChangeGameState(GamePhase.Game);
        /*
        GameAnalytics.NewProgressionEvent
        (GAProgressionStatus.Start, SaveLoad.I.playerProgress.curentLevel.ToString(), 0);
        */
    }

    public void OnClickRestart()
    {
        /*
        GameAnalytics.NewProgressionEvent
        (GAProgressionStatus.Fail, SaveLoad.I.playerProgress.curentLevel.ToString(), 0);
        */
        CurrencyManager.GainGold(GoldCollectedThisLevel);
        SaveLoad.I.SaveToJson();
        StartLevel();
    }


    public void OnBallTriggered(bool finished)
    {
        if (finished)
        {
            finishedDoorCount++;
        }
        else
        {
            finishedDoorCount--;


            if (finishedDoorCount <= 0)
            {
                finishedDoorCount = 0;
            }
        }

        if (finishedDoorCount >= LevelController.DoorControllers.Length)
        {
            if (!SaveLoad.I.playerProgress.tutorialFinished)
            {
                SaveLoad.I.playerProgress.tutorialFinished = true;
            }

            ChangeGameState(GamePhase.End);
        }
    }
}