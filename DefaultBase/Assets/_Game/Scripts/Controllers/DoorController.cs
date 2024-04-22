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
    private int _currentCount;

    [SerializeField] private float TimeBetween;
    private float _currentTime;
    private bool _timerOn;

    private void Start()
    {
        RequiredText.text = _currentCount + "/" + RequiredCount;
    }

    private void OnMovableTriggered()
    {
        if (_currentCount < RequiredCount)
        {
            _currentCount++;

            RequiredText.text = _currentCount + "/" + RequiredCount;
        }

        _timerOn = true;
        _currentTime = 0;

    }


    private void FixedUpdate()
    {
        if (!_timerOn) return;

        _currentTime+= Time.deltaTime;

        if (!(_currentTime >= TimeBetween)) return;
        
        _currentTime = 0;
        _currentCount--;
            
        RequiredText.text = _currentCount + "/" + RequiredCount;

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