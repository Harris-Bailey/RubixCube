using UnityEngine;

public class SettingsUI : MonoBehaviour {
    
    [SerializeField] private ColourPickerUpdater colourPicker;
    [SerializeField] private UIColourButton[] cubeColourButtons;
    [SerializeField] private CanvasGroup[] groupDisabledByColourPicker;
    private UIColourButton activeColourButton;
    private Color previousColour;
    
    private void Awake() {
        foreach (UIColourButton colourButton in cubeColourButtons) {
            colourButton.Button.onClick.AddListener(() => {
                OnButtonClicked(colourButton);
                colourPicker.ChooseColourOnPicker(colourButton.Button.image.color);
                DisableInteractables();
                colourPicker.gameObject.SetActive(true);
            });
        }
        
        colourPicker.OnColourPicked += ApplyColourFromPicker;
    }
    
    private void OnButtonClicked(UIColourButton clickedButton) {
        activeColourButton = clickedButton;
        previousColour = clickedButton.Button.image.color;
    }
    
    public void ApplyColourFromPicker(Color colour) {
        activeColourButton.Button.image.color = colour;
        SettingsSaveLoad.SaveData(activeColourButton.NameKey, SettingsSaveLoad.ConvertColourToString(colour));
    }
    
    public void CancelColourFromPicker() {
        activeColourButton.Button.image.color = previousColour;
        SettingsSaveLoad.SaveData(activeColourButton.NameKey, SettingsSaveLoad.ConvertColourToString(previousColour));
    }
    
    public void DisableInteractables() {
        foreach (CanvasGroup group in groupDisabledByColourPicker) {
            group.interactable = false;
        }
    }
    
    public void EnableInteractables() {
        foreach (CanvasGroup group in groupDisabledByColourPicker) {
            group.interactable = true;
        }
    }
}
