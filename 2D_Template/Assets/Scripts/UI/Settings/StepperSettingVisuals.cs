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
    public Texture2D WhiteArrow = null;
    public Texture2D BlackArrow = null;
    public static float HoverScale = 1.05f;
    
    public bool isSelected
    {
        get => Background.BodyEnabled;
        set
        {
            Background.BodyEnabled = value;
            SettingLabel.Color = value ? Color.black : Color.white;
            ValueLabel.Color = value ? Color.black : Color.white;
            LeftArrow.SetImage(value ? BlackArrow : WhiteArrow);
            RightArrow.SetImage(value ? BlackArrow : WhiteArrow);
        }
            
    }

    private MultiOptionSetting dataSource = null;

    public void Initialize(MultiOptionSetting dataSource)
    {
        this.dataSource = dataSource;

        UpdateValue();
        
        //When OnIndexChanged also call this code -> UpdateValue()
        //We don't care about the paramenter
        dataSource.OnIndexChanged += _ => UpdateValue();
        
        //State Changing
        LeftArrow.AddGestureHandler<Gesture.OnClick>(HandleLeftArrowClicked);
        RightArrow.AddGestureHandler<Gesture.OnClick>(HandleRightArrowClicked);
    }

    #region HandleData
    
    internal static void HandleHover(Gesture.OnHover evt, StepperSettingVisuals target)
    {
        target.Background.DOKill();
        target.Background.transform.DOScale(target.SettingLabel.transform.localScale * HoverScale, 0.15f).SetEase(Ease.OutBack);
        target.isSelected = true;
    }

    internal static void HandleUnHover(Gesture.OnUnhover evt, StepperSettingVisuals target)
    {
        target.Background.DOKill();
        target.Background.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutQuad);
        target.isSelected = false;
    }

    internal static void HandlePress(Gesture.OnPress evt, StepperSettingVisuals target)
    {
        //Play SFX
    }
    
    private void UpdateValue()
    {
        ValueLabel.Text = dataSource.Options[dataSource.SelectedIndex];
    }

    private void HandleLeftArrowClicked(Gesture.OnClick evt)
    {
        if (dataSource.SelectedIndex == 0)
        {
            dataSource.SelectedIndex = dataSource.Options.Length - 1;
        }else 
            dataSource.SelectedIndex = Mathf.Max(0, dataSource.SelectedIndex - 1);
    }

    private void HandleRightArrowClicked(Gesture.OnClick evt)
    {
        if (dataSource.SelectedIndex == dataSource.Options.Length - 1)
        {
            dataSource.SelectedIndex = 0;
        }else
            dataSource.SelectedIndex = Mathf.Min(dataSource.Options.Length - 1, dataSource.SelectedIndex + 1);
    }

    #endregion
}
