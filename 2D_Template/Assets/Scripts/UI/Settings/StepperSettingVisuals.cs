using DG.Tweening;
using Nova;
using UnityEngine;

[System.Serializable]
public class StepperOptionVisuals : ItemVisuals
{
    public TextBlock label = null;
    public UIBlock2D LeftArrow = null;
    public UIBlock2D RightArrow = null;
    public static float HoverScale = 1.1f;
}


[System.Serializable]
public class StepperSettingVisuals : ItemVisuals
{
    public UIBlock2D Background = null;
    public TextBlock label = null;
    public static float HoverScale = 1.1f;
    public ListView OptionsList = null;

    public bool isSelected
    {
        get => Background.BodyEnabled;
        set
        {
            Background.BodyEnabled = value;
            label.Color = value ? Color.black : Color.white;
        }
            
    }

    private MultiOptionSetting dataSource = null;
    private bool eventHandlesRegistered = false;
    
    public static void HandleHover(Gesture.OnHover evt, StepperSettingVisuals target)
    {
        target.label.DOKill();
        target.label.transform.DOScale(target.label.transform.localScale * HoverScale, 0.2f).SetEase(Ease.OutBack);
    }

    public static void HandleUnHover(Gesture.OnUnhover evt, StepperSettingVisuals target)
    {
        target.label.DOKill();
        target.label.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuad);
    }

    public static void HandlePress(Gesture.OnPress evt, StepperSettingVisuals target)
    {
        //Play SFX
    }

    public void Initialize(MultiOptionSetting dataSource)
    {
        this.dataSource = dataSource;

        EnsureEventHandlers();

        OptionsList.SetDataSource(dataSource.Options);
        OptionsList.JumpToIndex(dataSource.selectedIndex);
    }

    private void EnsureEventHandlers()
    {
        if (eventHandlesRegistered)
            return;
        eventHandlesRegistered = true;

        OptionsList.AddGestureHandler<Gesture.OnHover, StepperOptionVisuals>(HandleOptionHovered);
        OptionsList.AddGestureHandler<Gesture.OnUnhover, StepperOptionVisuals>(HandleOptionUnhovered);
        OptionsList.AddGestureHandler<Gesture.OnClick, StepperOptionVisuals>(HandleOptionClicked);

        OptionsList.AddDataBinder<string, StepperOptionVisuals>(BindOption);
    }

    private void BindOption(Data.OnBind<string> evt, StepperOptionVisuals target, int index)
    {
    }

    private void HandleOptionClicked(Gesture.OnClick evt, StepperOptionVisuals target, int index)
    {
    }

    private void HandleOptionUnhovered(Gesture.OnUnhover evt, StepperOptionVisuals target, int index)
    {
    }

    private void HandleOptionHovered(Gesture.OnHover evt, StepperOptionVisuals target, int index)
    {
    }
}
