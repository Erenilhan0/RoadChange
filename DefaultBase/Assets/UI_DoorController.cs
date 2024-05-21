using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UI_DoorController : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI DoorText;
    

    public void OnMovableTriggered(int maxCount, int currentCount)
    {
        DoorText.text = currentCount + "/" + maxCount;
    }

    
}
