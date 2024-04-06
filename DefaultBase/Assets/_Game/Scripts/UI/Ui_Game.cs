using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Ui_Game : UiBase
{



    [SerializeField] private TextMeshProUGUI levelText;
        
    private void Start()
    {
        GameManager.I.OnGamePhaseChange+= OnOnGamePhaseChange;
    }

    private void OnOnGamePhaseChange(GamePhase obj)
    {
        if (obj == GamePhase.Game)
        {
            levelText.text = "Level " + (GlobalGameManager.I.currentLevel+1).ToString();
            levelText.gameObject.SetActive(true);
        }

    }


    public override void HideUi()
    {
    }


    public override void ShowUi()
    {
    }
}
