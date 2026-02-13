using System;
using System.Collections.Generic;
using DG.Tweening;
using Nova;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PopupButtonVisuals : ItemVisuals
{
    public UIBlock2D Background = null;
    public TextBlock ButtonLabel = null;
}


[System.Serializable]
public class PopupBoxVisuals : ItemVisuals  
{
    public UIBlock2D Background = null;
    public TextBlock PopupText = null;
    public ListView ButtonList = null;
    
    public float PopinDuration = 0.35f;

    public event Action<int> OnButtonClicked;
    
    private bool EventsRegistered = false;
    private List<PopupButtonData> currentButtons;

    public void Bind(PopupData data)
    {
        EnsureEventHandlers();
                
        PopupText.Text = data.message;
        currentButtons = data.buttons;
        
        ButtonList.SetDataSource(currentButtons);
    }
    
    public void Show()
    {
        PopupText.Visible = false;
        Background.transform.localScale = Vector3.zero;
        Background.transform.DOScale(Vector3.one, PopinDuration).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        Background.transform.DOScale(Vector3.one, PopinDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            Background.Visible = false;
            ButtonList.SetDataSource<PopupButtonData>(null);
        });
    }

    public void EnsureEventHandlers()
    {
        if (!EventsRegistered) EventsRegistered = true;
        
        ButtonList.AddGestureHandler<Gesture.OnHover, PopupButtonVisuals>(HandleButtonHover);
        ButtonList.AddGestureHandler<Gesture.OnUnhover, PopupButtonVisuals>(HandleButtonUnHover);
        
        ButtonList.AddDataBinder<PopupButtonData, PopupButtonVisuals>(BindButtonData);
        ButtonList.AddGestureHandler<Gesture.OnClick, PopupBoxVisuals>(HandleButtonClick);
    }

    private void BindButtonData(Data.OnBind<PopupButtonData> evt, PopupButtonVisuals target, int index)
    {
        target.ButtonLabel.Text = evt.UserData.label;
    }

    internal static void HandleButtonHover(Gesture.OnHover evt, PopupButtonVisuals target, int index)
    {
        
    }

    internal static void HandleButtonUnHover(Gesture.OnUnhover evt, PopupButtonVisuals target, int index)
    {
    }

    private void HandleButtonClick(Gesture.OnClick evt, PopupBoxVisuals target, int index)
    {
        OnButtonClicked?.Invoke(index);
    }
}
