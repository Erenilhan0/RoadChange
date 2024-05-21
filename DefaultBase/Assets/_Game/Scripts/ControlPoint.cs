using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ControlPoint : MonoBehaviour
{
    [SerializeField] private int MaxChangeAmount;
    [SerializeField] private int ChangeAmount;

    private Vector3 _startPos;


    [SerializeField] private MeshRenderer _meshRenderer;
    
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color unSelectedColor;

    private void Start()
    {
        _startPos = transform.localPosition;
    }

    public void OnHold(bool selected)
    {
        if (selected)
        {
            _meshRenderer.material.color = selectedColor;

        }
        else
        {
            _meshRenderer.material.color = unSelectedColor;
        }

    }
    
    
    public void ChangePosition(bool upwards,float moveAmount)
    {
        if (upwards)
        {
            if(ChangeAmount >= MaxChangeAmount) return;

            ChangeAmount++;
            transform.localPosition += new Vector3(0, moveAmount, 0);
        }
        else
        {
            if(ChangeAmount <= -MaxChangeAmount) return;

            ChangeAmount--;
            transform.localPosition -= new Vector3(0, moveAmount, 0);
        }

    }


    public void SetGroupParent(Transform pointsToChild)
    {
        pointsToChild.SetParent(transform);
   
    }

    public void ResetPosition()
    {
        transform.localPosition = _startPos;
        ChangeAmount = 0;
    }
}
