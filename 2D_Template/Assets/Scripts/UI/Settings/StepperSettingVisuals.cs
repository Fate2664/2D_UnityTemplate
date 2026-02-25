using DG.Tweening;
using Nova;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

    private StepperSetting DataSource;

    public void Initialize(StepperSetting dataSource)
    {
        this.DataSource = dataSource;

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
        if (SettingsMenu.Instance.popup.IsOpen) return;

        target.Background.DOKill();
        target.Background.transform.DOScale(target.SettingLabel.transform.localScale * HoverScale, 0.15f)
            .SetEase(Ease.OutBack);
        target.isSelected = true;
    }

    internal static void HandleUnHover(Gesture.OnUnhover evt, StepperSettingVisuals target)
    {
        if (SettingsMenu.Instance.popup.IsOpen) return;

        target.Background.DOKill();
        target.Background.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutQuad);
        target.isSelected = false;
    }

    internal static void HandlePress(Gesture.OnPress evt, StepperSettingVisuals target)
    {
        if (SettingsMenu.Instance.popup.IsOpen) return;

        //Play SFX
    }

    private void UpdateValue()
    {
        ValueLabel.Text = DataSource.Options[DataSource.SelectedIndex];
        
        CheckLabel();
    }

    private void CheckLabel()
    {
        if (DataSource.SelectedIndex !=
            PlayerPrefs.GetInt(DataSource.Key, DataSource.SelectedIndex))
        {
            if (SettingLabel.Text[SettingLabel.Text.Length - 1] != '*')
            {
                SettingLabel.Text += "*";
            }
        }
        else 
        if (DataSource.SelectedIndex == PlayerPrefs.GetInt(DataSource.Key, DataSource.SelectedIndex))
        {
            if (SettingLabel.Text[SettingLabel.Text.Length - 1] == '*')
            {
                SettingLabel.Text = SettingLabel.Text.Remove(SettingLabel.Text.Length - 1);
            }
        } 
    }

    private void HandleLeftArrowClicked(Gesture.OnClick evt)
    {
        DataSource.MoveSelection(-1);
    }

    private void HandleRightArrowClicked(Gesture.OnClick evt)
    {
        DataSource.MoveSelection(1);
    }

    #endregion
}