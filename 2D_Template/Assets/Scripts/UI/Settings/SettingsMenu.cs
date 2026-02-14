using System;
using UnityEngine;
using Nova;
using NovaSamples.UIControls;
using System.Collections.Generic;

[System.Serializable]
public class SettingsMenu : MonoBehaviour
{
    public UIBlock Root = null;
    public GameInput gameInput = null;
    public List<SettingsCollection> SettingsCollection = null;
    public ListView TabBar = null;
    public ListView SettingsList = null;
    
    private int selectedIndex = -1;
    private List<Setting> CurrentSettings => SettingsCollection[selectedIndex].Settings;
    private List<Setting> currentSortedSettings;
    private int currentIndex = 0;
    private float inputCooldown = 0.15f;
    private float inputTimer;
    private float verticalNav;
    private float horizontalNav;
    
    private void Start()
    {
        //SettingsManager.Instance.LoadAllSettings();    
        
        //Visual
        Root.AddGestureHandler<Gesture.OnHover, StepperSettingVisuals>(StepperSettingVisuals.HandleHover);
        Root.AddGestureHandler<Gesture.OnUnhover, StepperSettingVisuals>(StepperSettingVisuals.HandleUnHover);
        Root.AddGestureHandler<Gesture.OnPress, StepperSettingVisuals>(StepperSettingVisuals.HandlePress);
        
        //State Changing
        SettingsList.AddGestureHandler<Gesture.OnClick, StepperSettingVisuals>(HandleStepperClick);
        
        //Data Binding
        SettingsList.AddDataBinder<MultiOptionSetting, StepperSettingVisuals>(BindStepperSetting);
        
        
        //Tabs
        TabBar.AddDataBinder<SettingsCollection, TabButtonVisuals>(BindTab);
        TabBar.AddGestureHandler<Gesture.OnHover, TabButtonVisuals>(TabButtonVisuals.HandleHover);
        TabBar.AddGestureHandler<Gesture.OnPress, TabButtonVisuals>(TabButtonVisuals.HandlePress);
        TabBar.AddGestureHandler<Gesture.OnUnhover, TabButtonVisuals>(TabButtonVisuals.HandleUnHover);
        TabBar.AddGestureHandler<Gesture.OnClick, TabButtonVisuals>(HandleTabClicked);
        
        TabBar.SetDataSource(SettingsCollection);

        if (TabBar.TryGetItemView(0, out ItemView firstTab))
        {
            SelectTab(firstTab.Visuals as TabButtonVisuals, 0);
        }
    }

    private void Update()
    {
        HandleKeyboardNavigation();
    }

    private void HandleKeyboardNavigation()
    {
        float nav = gameInput.GetVerticalNav();
        Debug.Log(nav.ToString());
        if (Time.unscaledTime < inputTimer) return;

        if (nav > 0.5f)
        {
            MoveSelection(-1);
        }
        else if (nav < -0.5f)
        {
            MoveSelection(1);
        }
    }

    private void MoveSelection(int direction)
    {
        int newIndex = Mathf.Clamp(currentIndex + direction, 0, SettingsCollection.Count - 1);

        if (newIndex == currentIndex) return;
        
        currentIndex = newIndex;

        HighlightCurrentSetting();
        SettingsList.JumpToIndex(currentIndex);
        
        inputTimer = Time.unscaledTime + inputCooldown;
    }

    private void HighlightCurrentSetting()
    {
        for (int i = 0; i < SettingsCollection.Count; i++)
        {
            if (SettingsList.TryGetItemView(i, out ItemView itemView))
            {
                StepperSettingVisuals visuals = itemView.Visuals as StepperSettingVisuals;
                visuals.isSelected = (i == currentIndex);
            }
        }
    }

    #region HandleData

    private void SelectTab(TabButtonVisuals visuals, int index)
    {
        if (index == selectedIndex)
        {
            return;
        }
        if (selectedIndex >= 0 && TabBar.TryGetItemView(selectedIndex, out ItemView currentItemView))
        {
            (currentItemView.Visuals as TabButtonVisuals).isSelected = false;
        }
        
        selectedIndex = index;
        visuals.isSelected = true;
        currentSortedSettings = new List<Setting>(CurrentSettings);
        currentSortedSettings.Sort((a, b) => a.Order.CompareTo(b.Order));
        
        SettingsList.SetDataSource(currentSortedSettings);
        currentIndex = 0;
        HighlightCurrentSetting();
    }

    private void HandleStepperClick(Gesture.OnClick evt, StepperSettingVisuals target, int index)
    {
        var data = (MultiOptionSetting)currentSortedSettings[index];
    }
    private void HandleTabClicked(Gesture.OnClick evt, TabButtonVisuals target, int index)
    {
        SelectTab(target, index);
    }

    #endregion

    #region BindData

    private void BindTab(Data.OnBind<SettingsCollection> evt, TabButtonVisuals target, int index)
    {
        target.label.Text = evt.UserData.Category;
    }
    
    private void BindStepperSetting(Data.OnBind<MultiOptionSetting> evt, StepperSettingVisuals target, int index)
    {
        target.SettingLabel.Text = evt.UserData.Name;
        target.Initialize(evt.UserData);
    }

    #endregion
}
