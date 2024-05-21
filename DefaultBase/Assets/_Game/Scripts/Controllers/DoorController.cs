using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorController : MonoBehaviour
{
    [SerializeField] private TextMeshPro RequiredText;

    [SerializeField] private int RequiredCount;
    public int _currentCount;

    [SerializeField] private float TimeBetween;
    private float _currentTime;
    private bool _timerOn;


    [SerializeField] private UI_DoorController UIDoorController;
    
    
    private void Start()
    {
        RequiredText.text = _currentCount + "/" + RequiredCount;
        
        UIDoorController.OnMovableTriggered(RequiredCount,_currentCount);

    }

    private void OnMovableTriggered()
    {
        if (_currentCount < RequiredCount)
        {
            _currentCount++;

            RequiredText.text = _currentCount + "/" + RequiredCount;
            
            UIDoorController.OnMovableTriggered(RequiredCount,_currentCount);
        }
        
        if (_currentCount == RequiredCount)
        {
            GameManager.I.OnBallTriggered(true);
        }
     

        _timerOn = true;
        _currentTime = 0;
    }


    private void FixedUpdate()
    {
        if (!_timerOn) return;

        _currentTime += Time.deltaTime;

        if (!(_currentTime >= TimeBetween)) return;

        if (_currentCount == RequiredCount)
        {
            GameManager.I.OnBallTriggered(false);
        }

        _currentTime = 0;
        _currentCount--;

        RequiredText.text = _currentCount + "/" + RequiredCount;
        
        UIDoorController.OnMovableTriggered(RequiredCount,_currentCount);

        if (_currentCount == 0)
        {
            _timerOn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnMovableTriggered();
        other.GetComponent<Movable>().OnCrash();
    }
}