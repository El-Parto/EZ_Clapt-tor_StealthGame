using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class DetectStatus : MonoBehaviour
{

    
    public enum DetectState {Caution, Clear, Caught};

    [SerializeField] protected TextMeshProUGUI detectStateText;
    
    private int stateID;

    void Start () 
    {
        DetectState DetectState;
        
        DetectState = DetectState.Clear;
        detectStateText = FindObjectOfType<TextMeshProUGUI>();
        
    }

    private void Update()
    {
        
    }
    
    DetectState WhatStateAmI (DetectState state)
    {
        Color stateColor;

        switch(state)
        {
            case DetectState.Clear:
            {
                detectStateText.text = "Clear";
                detectStateText.color = Color.green;
                break;
            }
            case DetectState.Caution:
            {
                
                detectStateText.text = "Warning";
                stateColor = new Color(1, 0.5f, 0);
                detectStateText.color = stateColor;
                break;
            }
            case DetectState.Caught:
            {
                detectStateText.text = "Caught. GAME OVER.";
                stateColor = new Color(1, 0, 0);
                detectStateText.color = stateColor;
                break;
            }
            
        }

        return state;
    }
        
        
    
}
