using UnityEngine;

public class FullscreenController : MonoBehaviour {
    
    private void Update() {
        Screen.fullScreen = SettingsSaveLoad.ConvertIntToBool(SettingsSaveLoad.GetIntFromKey(SettingsSaveLoad.FullscreenActiveKey));
    }
}
