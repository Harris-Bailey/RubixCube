using UnityEngine;

public class FullscreenController : MonoBehaviour {
    
    private void Awake() {
        SettingsSaveLoad.OnAnySettingChanged += UpdateFullscreen;
    }
    
    private void UpdateFullscreen() {
        Screen.fullScreen = SettingsSaveLoad.FullscreenActive;
    }
}
