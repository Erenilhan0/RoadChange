using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  GamePhase{Menu,Game,End}

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public event Action<GamePhase> OnGamePhaseChange;
    public GamePhase currentGamePhase;

    public bool didPlayerWon;
    public int GoldCollectedThisLevel;
    

    
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
        SaveLoad.I.playerProgress.curentLevel++;
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
        didPlayerWon = false;
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

}
