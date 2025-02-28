using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SliderDisplay : MonoBehaviour {
    [SerializeField] private string nameKey;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderValue;
    
    private void Awake() {        
        slider.onValueChanged.AddListener(SetSliderText);
        
        int savedValue = SettingsSaveLoad.GetIntFromKey(nameKey);
        slider.value = savedValue;
    }
    
    private void SetSliderText(float value) {
        sliderValue.text = value.ToString();
        SettingsSaveLoad.SaveData(nameKey, (int)value);
    }
}
