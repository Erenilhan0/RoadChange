using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyManager 
{
    private static int gold
    {
        get => SaveLoad.I.playerProgress.gold;
        set => SaveLoad.I.playerProgress.gold = value;
    }

    
    

    public static bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            SaveLoad.I.SaveToJson();

            UiManager.I.StartGoldAnim(-amount);
            return true;
        }

     
        return false;
    }



    public static void GainGold(int amount)
    {
        gold += amount;

        UiManager.I.StartGoldAnim(+amount);

        SaveLoad.I.SaveToJson();
        
    }


    
    
}
