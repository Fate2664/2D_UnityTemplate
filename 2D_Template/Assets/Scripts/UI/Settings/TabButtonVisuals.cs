using Nova;
using UnityEngine;

[System.Serializable]
public class TabButtonVisuals : ItemVisuals
{
    public TextBlock label = null;
    public UIBlock2D Background = null;
    public UIBlock2D SelectedIndicator = null;

    public Color DefaultColor;
    public Color SelectedColor;

    public Color DefaultGradientColor;
    public Color HoveredGradientColor;
    public Color PressedGradientColor;

    public bool isSelected
    {
        get => SelectedIndicator.gameObject.activeSelf;
        set
        {
            SelectedIndicator.gameObject.SetActive(value);
            Background.Color = value ? SelectedColor : DefaultColor;
        }
    }

    internal static void HandlePress(Gesture.OnPress evt, TabButtonVisuals target, int index)
    {
        target.Background.Gradient.Color = target.PressedGradientColor;
        //Play Audio SFX
    }

    public static void HandleHover(Gesture.OnHover evt, TabButtonVisuals target, int index)
    {
        target.Background.Gradient.Color = target.HoveredGradientColor;
        //Play Audio SFX
    }

    public static void HandleUnHover(Gesture.OnUnhover evt, TabButtonVisuals target, int index)
    {
        target.Background.Gradient.Color = target.DefaultGradientColor;
    }

    public static void HandleRelease(Gesture.OnRelease evt, TabButtonVisuals target, int index)
    {
        target.Background.Gradient.Color = target.HoveredGradientColor;
    }
}
