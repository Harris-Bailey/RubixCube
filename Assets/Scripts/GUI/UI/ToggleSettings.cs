using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour {
    
    [SerializeField] private Toggle FullscreenToggle;
    
    private void Awake() {
        FullscreenToggle.isOn = SettingsSaveLoad.FullscreenActive;
        FullscreenToggle.onValueChanged.AddListener((value) => SettingsSaveLoad.FullscreenActive = value);
    }
}
