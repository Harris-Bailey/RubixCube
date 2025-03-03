using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderSettings : MonoBehaviour {
    
    [System.Serializable]
    private struct SliderAndOutputDisplay {
        public Slider slider;
        public TextMeshProUGUI sliderOutputDisplay;
    }
    
    [SerializeField] private SliderAndOutputDisplay fieldOfView;
    [SerializeField] private SliderAndOutputDisplay musicVolume;
    [SerializeField] private SliderAndOutputDisplay sfxVolume;
    [SerializeField] private SliderAndOutputDisplay animationDuration;
    
    private void Awake() {        
        fieldOfView.slider.onValueChanged.AddListener((value) => {
            fieldOfView.sliderOutputDisplay.text = value.ToString();
            SettingsSaveLoad.FieldOfView = (int)value;
        });
        musicVolume.slider.onValueChanged.AddListener((value) => {
            musicVolume.sliderOutputDisplay.text = value.ToString();
            SettingsSaveLoad.MusicVolume = (int)value;
        });
        sfxVolume.slider.onValueChanged.AddListener((value) => {
            sfxVolume.sliderOutputDisplay.text = value.ToString();
            SettingsSaveLoad.SFXVolume = (int)value;
        });
        animationDuration.slider.onValueChanged.AddListener((value) => {
            animationDuration.sliderOutputDisplay.text = value.ToString();
            SettingsSaveLoad.AnimationDuration = (int)value;
        });
        
        // changing the value like this automatically calls the onValueChanged delegate
        fieldOfView.slider.value = SettingsSaveLoad.FieldOfView;
        musicVolume.slider.value = SettingsSaveLoad.MusicVolume;
        sfxVolume.slider.value = SettingsSaveLoad.SFXVolume;
        animationDuration.slider.value = SettingsSaveLoad.AnimationDuration;
    }
}