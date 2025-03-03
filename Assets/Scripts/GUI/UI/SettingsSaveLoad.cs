using System;
using UnityEngine;

public static class SettingsSaveLoad {
    
    static SettingsSaveLoad() {
        OnAnySettingChanged += PlayerPrefs.Save;
    }
    
    public static event Action OnAnySettingChanged;
    
    public static Color CenterOneColour {
        get => ConvertStringToColour(PlayerPrefs.GetString(CenterOneKey, centerOneDefault));
        set { 
            PlayerPrefs.SetString(CenterOneKey, ConvertColourToString(value));
            OnAnySettingChanged.Invoke();
        }
    }
    public static Color CenterTwoColour {
        get => ConvertStringToColour(PlayerPrefs.GetString(CenterTwoKey, centerTwoDefault));
        set { 
            PlayerPrefs.SetString(CenterTwoKey, ConvertColourToString(value));
            OnAnySettingChanged.Invoke();
        }
    }
    public static Color CenterThreeColour {
        get => ConvertStringToColour(PlayerPrefs.GetString(CenterThreeKey, centerThreeDefault));
        set { 
            PlayerPrefs.SetString(CenterThreeKey, ConvertColourToString(value));
            OnAnySettingChanged.Invoke();
        }
    }
    public static Color CenterFourColour {
        get => ConvertStringToColour(PlayerPrefs.GetString(CenterFourKey, centerFourDefault));
        set { 
            PlayerPrefs.SetString(CenterFourKey, ConvertColourToString(value));
            OnAnySettingChanged.Invoke();
        }
    }
    public static Color CenterFiveColour {
        get => ConvertStringToColour(PlayerPrefs.GetString(CenterFiveKey, centerFiveDefault));
        set { 
            PlayerPrefs.SetString(CenterFiveKey, ConvertColourToString(value));
            OnAnySettingChanged.Invoke();
        }
    }
    public static Color CenterSixColour {
        get => ConvertStringToColour(PlayerPrefs.GetString(CenterSixKey, centerSixDefault));
        set { 
            PlayerPrefs.SetString(CenterSixKey, ConvertColourToString(value));
            OnAnySettingChanged.Invoke();
        }
    }
    public static int FieldOfView {
        get => PlayerPrefs.GetInt(FieldOfViewKey, fieldOfViewDefault);
        set { 
            PlayerPrefs.SetInt(FieldOfViewKey, value);
            OnAnySettingChanged.Invoke();
        }
    }
    public static int AnimationDuration {
        get => PlayerPrefs.GetInt(AnimationDurationKey, animationDurationDefault);
        set { 
            PlayerPrefs.SetInt(AnimationDurationKey, value);
            OnAnySettingChanged.Invoke();
        }
    }
    public static int MusicVolume {
        get => PlayerPrefs.GetInt(MusicVolumeKey, musicVolumeDefault);
        set { 
            PlayerPrefs.SetInt(MusicVolumeKey, value);
            OnAnySettingChanged.Invoke();
        }
    }
    public static int SFXVolume {
        get => PlayerPrefs.GetInt(SFXVolumeKey, sfxVolumeDefault);
        set { 
            PlayerPrefs.SetInt(SFXVolumeKey, value);
            OnAnySettingChanged.Invoke();
        }
    }
    public static bool FullscreenActive {
        get => ConvertIntToBool(PlayerPrefs.GetInt(FullscreenActiveKey, fullscreenDefault));
        set {
            PlayerPrefs.SetInt(FullscreenActiveKey, ConvertBoolToInt(value));
            OnAnySettingChanged.Invoke();
        }
    }
    
    private const string CenterOneKey = "CenterOneColour";
    private const string CenterTwoKey = "CenterTwoColour";
    private const string CenterThreeKey = "CenterThreeColour";
    private const string CenterFourKey = "CenterFourColour";
    private const string CenterFiveKey = "CenterFiveColour";
    private const string CenterSixKey = "CenterSixColour";
    private const string FieldOfViewKey = "FieldOfView";
    private const string AnimationDurationKey = "AnimationDuration";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";
    private const string FullscreenActiveKey = "FullscreenActive";
    
    private static readonly string centerOneDefault = ConvertColourToString(Color.yellow);
    private static readonly string centerTwoDefault = ConvertColourToString(Color.red);
    private static readonly string centerThreeDefault = ConvertColourToString(Color.green);
    private static readonly string centerFourDefault = ConvertColourToString(new Color(1, 0.5f, 0, 1));
    private static readonly string centerFiveDefault = ConvertColourToString(Color.blue);
    private static readonly string centerSixDefault = ConvertColourToString(Color.white);
    private static readonly int fieldOfViewDefault = 60;
    private static readonly int animationDurationDefault = 1;
    private static readonly int musicVolumeDefault = 50;
    private static readonly int sfxVolumeDefault = 50;
    private static readonly int fullscreenDefault = 1;
    
    public static int ConvertBoolToInt(bool value) {
        return value ? 1 : 0;
    }
    
    public static bool ConvertIntToBool(int value) {
        return value == 1;
    }
    
    public static Color ConvertStringToColour(string colour) {
        string[] colourSegments = colour.Split(' ');
        return new Color(float.Parse(colourSegments[0]), float.Parse(colourSegments[1]), float.Parse(colourSegments[2]));
    }
    
    public static string ConvertColourToString(Color colour) {
        string colourAsString = $"{colour.r} {colour.g} {colour.b}";
        return colourAsString;
    }
}