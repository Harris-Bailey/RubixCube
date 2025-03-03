using System;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour {
    
    [SerializeField] private Button centerOneButton;
    [SerializeField] private Button centerTwoButton;
    [SerializeField] private Button centerThreeButton;
    [SerializeField] private Button centerFourButton;
    [SerializeField] private Button centerFiveButton;
    [SerializeField] private Button centerSixButton;
    [SerializeField] private ColourPickerUpdater colourPicker;
    [SerializeField] private CanvasGroup[] groupDisabledByColourPicker;
    private Button activeColourButton;
    private Color previousColour;
    
    private void Awake() {
        Button[] colourButtons = {
            centerOneButton,
            centerTwoButton,
            centerThreeButton,
            centerFourButton,
            centerFiveButton,
            centerSixButton
        };
        for (int i = 0; i < colourButtons.Length; i++) {
            Button button = colourButtons[i];
            button.onClick.AddListener(() => {
                // assign the button that is most recently clicked
                activeColourButton = button;
                // assign the colour of the button to save it in case the user presses cancel
                previousColour = button.image.color;
                // set the colour picker to active in the scene so the user can interact with it
                colourPicker.gameObject.SetActive(true);
                // match the colour with the colour of the button
                colourPicker.SetInitialColourOnPicker(button.image.color);
                // only allow the colour picker to be interacted with
                DisableInteractables();
            });
        }
        
        centerOneButton.image.color = SettingsSaveLoad.CenterOneColour;
        centerTwoButton.image.color = SettingsSaveLoad.CenterTwoColour;
        centerThreeButton.image.color = SettingsSaveLoad.CenterThreeColour;
        centerFourButton.image.color = SettingsSaveLoad.CenterFourColour;
        centerFiveButton.image.color = SettingsSaveLoad.CenterFiveColour;
        centerSixButton.image.color = SettingsSaveLoad.CenterSixColour;
    }
    
    // this gets called by a unity event on the apply button in the canvas
    public void ApplyColourFromPicker() {
        Color colour = colourPicker.GetCurrentColour();
        activeColourButton.image.color = colour;
        AssignColourToButton(activeColourButton, colour);
    }
    
    // this gets called by a unity event on the cancel button in the canvas
    public void CancelColourFromPicker() {
        activeColourButton.image.color = previousColour;
        AssignColourToButton(activeColourButton, previousColour);
    }
    
    private void AssignColourToButton(Button button, Color colour) {
        if (button == centerOneButton)
            SettingsSaveLoad.CenterOneColour = colour;
        else if (button == centerTwoButton)
            SettingsSaveLoad.CenterTwoColour = colour;
        else if (button == centerThreeButton)
            SettingsSaveLoad.CenterThreeColour = colour;
        else if (button == centerFourButton)
            SettingsSaveLoad.CenterFourColour = colour;
        else if (button == centerFiveButton)
            SettingsSaveLoad.CenterFiveColour = colour;
        else if (button == centerSixButton)
            SettingsSaveLoad.CenterSixColour = colour;
    }
    
    public void DisableInteractables() {
        foreach (CanvasGroup group in groupDisabledByColourPicker) {
            group.interactable = false;
        }
    }
    
    // this gets called by both the apply and cancel buttons in the canvas
    public void EnableInteractables() {
        foreach (CanvasGroup group in groupDisabledByColourPicker) {
            group.interactable = true;
        }
    }
}
