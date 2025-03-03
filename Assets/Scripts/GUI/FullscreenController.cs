using UnityEngine;

public class FullscreenController : MonoBehaviour {
    
    private void Awake() {
        Screen.fullScreen = SettingsSaveLoad.FullscreenActive;
        SettingsSaveLoad.OnAnySettingChanged += UpdateFullscreen;
    }
    
    private void UpdateFullscreen() {
        Screen.fullScreen = SettingsSaveLoad.FullscreenActive;
    }
}
