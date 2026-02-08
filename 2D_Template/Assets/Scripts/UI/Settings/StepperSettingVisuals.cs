using DG.Tweening;
using Nova;
using UnityEngine;

[System.Serializable]
public class StepperSettingVisuals : ItemVisuals
{
    public UIBlock2D Background = null;
    public TextBlock SettingLabel = null;
    public TextBlock ValueLabel = null;
    public UIBlock2D LeftArrow = null;
    public UIBlock2D RightArrow = null;
    public static float HoverScale = 1.1f;
        
    public bool isSelected
    {
        get => Background.BodyEnabled;
        set
        {
            Background.BodyEnabled = value;
            SettingLabel.Color = value ? Color.black : Color.white;
        }
            
    }

    private MultiOptionSetting dataSource = null;
    
    public static void HandleHover(Gesture.OnHover evt, StepperSettingVisuals target)
    {
        target.SettingLabel.DOKill();
        target.SettingLabel.transform.DOScale(target.SettingLabel.transform.localScale * HoverScale, 0.2f).SetEase(Ease.OutBack);
    }

    public static void HandleUnHover(Gesture.OnUnhover evt, StepperSettingVisuals target)
    {
        target.SettingLabel.DOKill();
        target.SettingLabel.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuad);
    }

    public static void HandlePress(Gesture.OnPress evt, StepperSettingVisuals target)
    {
        //Play SFX
    }

    public void Initialize(MultiOptionSetting dataSource)
    {
        this.dataSource = dataSource;

        UpdateValue();
        
        //When OnIndexChanged also call this code -> UpdateValue()
        //We don't care about the paramenter
        dataSource.OnIndexChanged += _ => UpdateValue();
        
        LeftArrow.AddGestureHandler<Gesture.OnPress>(HandleLeftArrowPressed);
        RightArrow.AddGestureHandler<Gesture.OnPress>(HandleRightArrowPressed);
    }

    private void UpdateValue()
    {
        ValueLabel.Text = dataSource.Options[dataSource.SelectedIndex];
    }

    private void HandleLeftArrowPressed(Gesture.OnPress evt)
    {
        dataSource.SelectedIndex = Mathf.Max(0, dataSource.SelectedIndex - 1);
    }

    private void HandleRightArrowPressed(Gesture.OnPress evt)
    {
        dataSource.SelectedIndex = Mathf.Min(dataSource.Options.Length - 1, dataSource.SelectedIndex + 1);
    }
}
