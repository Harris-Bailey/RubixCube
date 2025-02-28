using UnityEngine;
using UnityEngine.UI;

public class UIColourButton : MonoBehaviour {
    [field: SerializeField] public string NameKey { get; private set; }
    [field: SerializeField] public Button Button { get; private set; }

    private void Awake() {
        Button.image.color = SettingsSaveLoad.ConvertStringToColour(SettingsSaveLoad.GetStringFromKey(NameKey));
    }
}
