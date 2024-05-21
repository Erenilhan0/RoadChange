using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI;

    [SerializeField] private MeshController MeshController;


    private void Start()
    {
        MeshController.ControlPointHolding += ControlPointOnHold;
    }
    

    public void ControlPointOnHold()
    {
        tutorialUI.SetActive(false);
    }


}
