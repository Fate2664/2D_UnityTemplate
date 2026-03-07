using System;
using DG.Tweening;
using Nova;
using UnityEngine;

[Serializable]
public class DialogueVisuals
{
    public UIBlock2D Background;
    public TextBlock DialogueText;
    public TextBlock NameText;
    public float PopinDuration = 0.35f;
    
    public void Show()
    {
        Background.Visible = true;
        Background.transform.DOScale(Vector3.one, PopinDuration).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        Background.transform.DOScale(Vector3.zero, PopinDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            Background.Visible = false;
        });
    }
}
