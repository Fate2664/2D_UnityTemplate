using Nova;
using UnityEngine;

[System.Serializable]
public class TabButtonVisuals : ItemVisuals
{
    public TextBlock label = null;
    public UIBlock2D Background = null;
    public float HoverScale = 1.1f;

    public bool isSelected
    {
        get => Background.BodyEnabled;
        set
        {
            Background.BodyEnabled = value;
            label.Color = value ? Color.black : Color.white;
        }
    }

    internal static void HandlePress(Gesture.OnPress evt, TabButtonVisuals target, int index)
    {
        //Play Audio SFX
    }

    internal static void HandleHover(Gesture.OnHover evt, TabButtonVisuals target, int index)
    {
        //USE DOTWEENS
        target.label.transform.localScale = Vector3.one * target.HoverScale;
        //Play Audio SFX
    }

    internal static void HandleUnHover(Gesture.OnUnhover evt, TabButtonVisuals target, int index)
    {
        //USE DOTWEENS
        target.label.transform.localScale = Vector3.one;
    }
}