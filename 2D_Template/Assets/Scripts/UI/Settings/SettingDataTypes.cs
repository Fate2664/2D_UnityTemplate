using System;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public abstract class Setting
{
    public string Key;
    public string Name;
    public int Order = 0;

    public enum SettingType
    {
        Audio,
        Video,
        Gameplay,
        Keybinds,
        Accessibility
    }
    public virtual void ResetToDefault() {}
}

[System.Serializable]
public class BoolSetting : Setting
{
    [FormerlySerializedAs("State")] public bool state;
    public bool DefaultState = false;
    public SettingType Type;
    public event Action<Setting> OnStateChanged;

    public bool State
    {
        get => state;
        set
        {
            this.state = value;
            OnStateChanged?.Invoke(this);
        }
    }
    
    public void Save() => PlayerPrefs.SetInt(Key, state ? 1 : 0);
    public void Load() => state = PlayerPrefs.GetInt(Key, DefaultState ? 1 : 0) == 1;

    public override void ResetToDefault()
    {
        State = DefaultState;
        Save();
    }
}

[System.Serializable]
public class FloatSetting : Setting
{
    [SerializeField]
    public float value;
    public float Min;
    public float Max;
    public string ValueFormat = "{0:0.0}";
    public float DefaultValue = 50f;
    public SettingType Type;
    public event Action<Setting> OnValueChanged;

    public float Value
    {
        get => Mathf.Clamp(value, Min, Max);
        set
        {
            this.value = Mathf.Clamp(value, Min, Max);
            OnValueChanged?.Invoke(this);
        }
    }
    
    public string DisplayValue => string.Format(ValueFormat, Value);
    
    public void Save() => PlayerPrefs.SetFloat(Key, Value);
    public void Load() => Value = PlayerPrefs.GetFloat(Key, DefaultValue);

    public override void ResetToDefault()
    {
        value = DefaultValue;
        Save();
    }
}

[System.Serializable]
public class MultiOptionSetting : Setting
{
    private const string NothingSelected = "None";
    public SettingType Type;
    public string[] Options = new string[0];
    public int selectedIndex = 0;
    public int DefaultIndex = 0;
    public event Action<Setting> OnIndexChanged;

    public int SelectedIndex
    {
        get => selectedIndex;
        set
        {
            this.selectedIndex = value;
            OnIndexChanged?.Invoke(this);
        }
    }
    
    public string CurrentSelection => SelectedIndex >= 0 && SelectedIndex < Options.Length ? Options[SelectedIndex] : NothingSelected;
    
    public void Save() => PlayerPrefs.SetInt(Key, SelectedIndex);
    public void Load() => SelectedIndex = PlayerPrefs.GetInt(Key, DefaultIndex);

    public override void ResetToDefault()
    {
        SelectedIndex = DefaultIndex;
        Save();
    }
}

[System.Serializable]
public class ResolutionSetting : MultiOptionSetting
{
    public Resolution[] Resolutions;
    public void Initialize()
    {
        Resolutions = Screen.resolutions;
        Options = new string[Resolutions.Length];
        int j = 0;
        for (int i = Resolutions.Length - 1; i >= 0; i--)
        {
            Resolution r =  Resolutions[i];
            Options[j] = $"{r.width} x {r.height} @ {r.refreshRate}Hz";
            j++;
        }
    }
    public Resolution GetSelectedResolution()
    {
        if (Resolutions == null || Resolutions.Length == 0)
        {
            Initialize();
        }
        return Resolutions[Mathf.Clamp(SelectedIndex, 0, Resolutions.Length - 1)];
    }
}



