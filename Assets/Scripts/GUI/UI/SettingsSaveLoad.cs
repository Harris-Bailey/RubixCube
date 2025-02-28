using Unity.VisualScripting;
using UnityEngine;

public static class SettingsSaveLoad {
    public static Color CenterOne { get; private set; }
    public static Color CenterTwo { get; private set; }
    public static Color CenterThree { get; private set; }
    public static Color CenterFour { get; private set; }
    public static Color CenterFive { get; private set; }
    public static Color CenterSix { get; private set; }
    public static int FieldOfView { get; private set; }
    public static int AnimationDuration { get; private set; }
    public static int MusicVolume { get; private set; }
    public static int SFXVolume { get; private set; }
    public static bool FullscreenActive { get; private set; }
    
    public const string CenterOneKey = "CenterOneColour";
    public const string CenterTwoKey = "CenterTwoColour";
    public const string CenterThreeKey = "CenterThreeColour";
    public const string CenterFourKey = "CenterFourColour";
    public const string CenterFiveKey = "CenterFiveColour";
    public const string CenterSixKey = "CenterSixColour";
    public const string FieldOfViewKey = "FieldOfView";
    public const string AnimationDurationKey = "AnimationDuration";
    public const string MusicVolumeKey = "MusicVolume";
    public const string SFXVolumeKey = "SFXVolume";
    public const string FullscreenActiveKey = "FullscreenActive";
    
    private static readonly string centerOneDefault = ConvertColourToString(Color.yellow);
    private static readonly string centerTwoDefault = ConvertColourToString(Color.red);
    private static readonly string centerThreeDefault = ConvertColourToString(Color.green);
    private static readonly string centerFourDefault = ConvertColourToString(new Color(1, 0.5f, 0, 1));
    private static readonly string centerFiveDefault = ConvertColourToString(Color.blue);
    private static readonly string centerSixDefault = ConvertColourToString(Color.white);
    private static readonly int fieldOfViewDefault = 60;
    private static readonly int aniamtionSpeedDefault = 1;
    private static readonly int musicVolumeDefault = 50;
    private static readonly int sfxVolumeDefault = 50;
    private static readonly int fullscreenDefault = 1;
    
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitialiseData() {
        // load the data into memory
        LoadData();
        
        // save the data so other scripts can use the Get methods below
        SaveAllData();
    }
    
    public static void LoadData() {
        CenterOne = ConvertStringToColour(PlayerPrefs.GetString(CenterOneKey, centerOneDefault));
        CenterTwo = ConvertStringToColour(PlayerPrefs.GetString(CenterTwoKey, centerTwoDefault));
        CenterThree = ConvertStringToColour(PlayerPrefs.GetString(CenterThreeKey, centerThreeDefault));
        CenterFour = ConvertStringToColour(PlayerPrefs.GetString(CenterFourKey, centerFourDefault));
        CenterFive = ConvertStringToColour(PlayerPrefs.GetString(CenterFiveKey, centerFiveDefault));
        CenterSix = ConvertStringToColour(PlayerPrefs.GetString(CenterSixKey, centerSixDefault));
        
        FieldOfView = PlayerPrefs.GetInt(FieldOfViewKey, fieldOfViewDefault);        
        AnimationDuration = PlayerPrefs.GetInt(AnimationDurationKey, aniamtionSpeedDefault);
        MusicVolume = PlayerPrefs.GetInt(MusicVolumeKey, musicVolumeDefault);
        SFXVolume = PlayerPrefs.GetInt(SFXVolumeKey, sfxVolumeDefault);
        FullscreenActive = ConvertIntToBool(PlayerPrefs.GetInt(FullscreenActiveKey, fullscreenDefault));
    }
    
    public static string GetStringFromKey(string key) {
        return PlayerPrefs.GetString(key, "");
    }
    
    public static int GetIntFromKey(string key) {
        return PlayerPrefs.GetInt(key, 0);
    }
    
    public static float GetFloatFromKey(string key) {
        return PlayerPrefs.GetFloat(key, 0);
    }
    
    public static void SaveAllData() {
        PlayerPrefs.SetString(CenterOneKey, ConvertColourToString(CenterOne));
        PlayerPrefs.SetString(CenterTwoKey, ConvertColourToString(CenterTwo));
        PlayerPrefs.SetString(CenterThreeKey, ConvertColourToString(CenterThree));
        PlayerPrefs.SetString(CenterFourKey, ConvertColourToString(CenterFour));
        PlayerPrefs.SetString(CenterFiveKey, ConvertColourToString(CenterFive));
        PlayerPrefs.SetString(CenterSixKey, ConvertColourToString(CenterSix));
        
        PlayerPrefs.SetInt(FieldOfViewKey, FieldOfView);
        PlayerPrefs.SetInt(AnimationDurationKey, AnimationDuration);
        PlayerPrefs.SetInt(MusicVolumeKey, MusicVolume);
        PlayerPrefs.SetInt(SFXVolumeKey, SFXVolume);
        PlayerPrefs.SetInt(FullscreenActiveKey, ConvertBoolToInt(FullscreenActive));
    }
    
    public static void SaveData(string key, string value) {
        PlayerPrefs.SetString(key, value);
    }
    
    public static void SaveData(string key, int value) {
        PlayerPrefs.SetInt(key, value);
    }
    
    public static void SaveData(string key, float value) {
        PlayerPrefs.SetFloat(key, value);
    }
    
    public static  void ResetToDefault() {
        PlayerPrefs.DeleteAll();
        LoadData();
        SaveAllData();
    }
    
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