using System;
using System.Collections.Generic;
using DG.Tweening;
using Nova;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class PopupData
{
    public string message;
    public List<PopupButtonData> buttons;
    
    public PopupData(string message, List<PopupButtonData> buttons)
    {
        this.message = message;
        this.buttons =  buttons;
    }
}

[System.Serializable]
public class PopupButtonData
{
    public string label;
    public UnityAction Callback;

    public PopupButtonData(string label, UnityAction callback)
    {
        this.label = label;
        this.Callback = callback;
    }
}

