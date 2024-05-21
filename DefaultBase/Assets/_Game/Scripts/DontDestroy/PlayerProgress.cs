using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class PlayerProgress
{
    
    public int maxLevel;
    public int currentLevel;
    public int gold;

    public bool tutorialFinished;
}
