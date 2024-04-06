using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    
    public static UiManager I;


    [SerializeField] private UiBase[] Uis;

    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI goldText;
    private bool canLerpGold;

    [SerializeField] private TextMeshProUGUI plusGold;
    private float targetGold;
    private float currentGem;


    
    
    
    private void Awake()
    {
        I = this;
    }

    private void OnOnGamePhaseChange(GamePhase obj)
    {
        for (int i = 0; i < Uis.Length; i++)
        {
            if ((int) obj == i)
            {
                Uis[i].ShowUi();
            }
            else
            {
                Uis[i].HideUi();
            }
        }
        
        switch (obj)
        {
            case GamePhase.Menu:
                break;
            case GamePhase.Game:
                break;
            case GamePhase.End:
                break;
        }
    }


    
    

    private void Start()
    {
        GameManager.I.OnGamePhaseChange += OnOnGamePhaseChange;

        SetGoldAndGemValues();
    }






    

    private void SetGoldAndGemValues()
    {
        ChangeGemValue(SaveLoad.I.playerProgress.gold);

        currentGem = SaveLoad.I.playerProgress.gold;
        targetGold = currentGem;
    }



    private void Update()
    {
        if (canLerpGold)
        {

            currentGem = Mathf.Lerp(currentGem, targetGold, .1f);
            ChangeGemValue(Mathf.CeilToInt(currentGem));
        }

        if (GameManager.I.currentGamePhase == GamePhase.Menu &&
            Input.GetMouseButtonDown(0))
        {
            OnClickPlayButton();
        }

    }



    public void StartGoldAnim(int changeAmount)
    {
        plusGold.gameObject.SetActive(false);
        if (changeAmount > 0)
        {
            plusGold.text = "+" + changeAmount.ToString();

        }
        else
        {
            plusGold.text = changeAmount.ToString();

        }

        plusGold.gameObject.SetActive(true);
        UpgradeGoldAmount();
    }



    public void UpgradeGoldAmount()
    {
        CancelInvoke();
        targetGold = SaveLoad.I.playerProgress.gold;
        canLerpGold = true;
        Invoke(nameof(StopInvoke), 1);
    }



    public void StopInvoke()
    {
        canLerpGold = false;
        ChangeGemValue(targetGold);
    }



    public void ChangeGemValue(float targetValue)
    {
        goldText.text = targetValue.ToString();
    }




    public void OnClickNextLevel()
    {
        GameManager.I.OnClickNextLevel();
    }

    public void OnClickRestart()
    {
        GameManager.I.OnClickRestart();
    }
    

    public void OnClickPlayButton()
    {
        GameManager.I.StartLevel();

    }


    public void OpenCloseBgFade(bool open)
    {
        if (open)
        {
            bg.DOFade(.8f, .5f);
        }
        else
        {
            bg.DOFade(0, .5f);
        }
    }
 
    
    

}
