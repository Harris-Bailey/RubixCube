using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour {
    [SerializeField] private string nameKey;
    [SerializeField] private Toggle toggle;
    
    private void Awake() {
        toggle.isOn = SettingsSaveLoad.ConvertIntToBool(SettingsSaveLoad.GetIntFromKey(nameKey));
        toggle.onValueChanged.AddListener((on) => SettingsSaveLoad.SaveData(SettingsSaveLoad.FullscreenActiveKey, SettingsSaveLoad.ConvertBoolToInt(on)));
    }
}
