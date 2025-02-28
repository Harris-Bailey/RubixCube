using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColourPickerUpdater : MonoBehaviour {
    public Action<Color> OnColourPicked;
    [SerializeField] private RawImage colourPickerImage;
    [SerializeField] private Scrollbar hueScroller;
    [SerializeField] private RectTransform pickerIcon;
    private int pickerTextureDimensions = 16;
    private float currentHue = 0, currentSaturation = 0, currentBrightness = 0;
    private float colourPickerWidth, colourPickerHeight;

    void Awake() {
        colourPickerWidth = (int)colourPickerImage.rectTransform.rect.width;
        colourPickerHeight = (int)colourPickerImage.rectTransform.rect.height;
        
        hueScroller.onValueChanged.AddListener((value) => {
            UpdateSaturationValueTexture(value);
            OnColourPicked(Color.HSVToRGB(currentHue, currentSaturation, currentBrightness));
        });
        UpdateSaturationValueTexture(0);
        UpdateHueScrollerTexture();
    }
    
    private void PositionIconFromCurrents() {
        // saturation is a value 0 to 1, so multiply it by the width of the colour picker to get the position from 0 to height
        float xPosition = currentSaturation * colourPickerWidth;
        // brightness is a value 0 to 1, so multiply it by the height of the colour picker to get the position from 0 to width
        float yPosition = currentBrightness * colourPickerHeight;
        
        // need the origin to be at the bottom left so the anchor should be set to bottom left
        pickerIcon.anchoredPosition = new Vector2(xPosition, yPosition);
    }
    
    public void ChooseColourOnPicker(Color colour) {
        Color.RGBToHSV(colour, out currentHue, out currentSaturation, out currentBrightness);
        hueScroller.value = currentHue;
        
        PositionIconFromCurrents();
        
        OnColourPicked(colour);
    }
    
    public void ChooseColourOnPicker(PointerEventData eventData) {
        UpdateOutputImageColour(eventData);
    }
    
    public void ChooseColourWithHexcode(string RGBHexCode) {
        // the hex code must have 6 characters
        if (RGBHexCode.Length != 6)
            return;
        
        float redValue; 
        float greenValue;
        float blueValue;    
        
        // try to convert the hex code into its 3 colours
        try {
            redValue = Convert.ToInt32(RGBHexCode[0..2], 16) / 255f;
            greenValue = Convert.ToInt32(RGBHexCode[2..4], 16) / 255f;
            blueValue = Convert.ToInt32(RGBHexCode[4..6], 16) / 255f;
        }
        // ignore if any of the characters aren't hexadecimal
        catch (FormatException) {
            return;
        }
        
        // convert the rgb values to hsv
        Color.RGBToHSV(new Color(redValue, greenValue, blueValue), out currentHue, out currentSaturation, out currentBrightness);
        
        // the hue is between 0 and 1 so can directly set that to the slider value
        hueScroller.value = currentHue;
        
        PositionIconFromCurrents();
        
        // return the chosen colour
        return;
    }
    
    private void UpdateOutputImageColour(PointerEventData eventData) {
        // changes the mouse position from screen point which is its position on the users screen
        // to its position inside the actual UI component
        RectTransformUtility.ScreenPointToLocalPointInRectangle(colourPickerImage.rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPositionFromCenter);
        
        // from the center, the x value goes from left to right, from -width/2 to width/2
        // so need to offset it with the half width to get it from 0 to the width and then round it
        int adjustedXPosition = (int)Mathf.Round(localPositionFromCenter.x + colourPickerWidth / 2);
        // clamp it so it can't exceed the object in the left or right directions
        adjustedXPosition = (int)Mathf.Clamp(adjustedXPosition, 0, colourPickerWidth);
        
        // from the center the y value goes bottom to top, from -height/2 to height/2
        // so need to offset by half height which gets the values from 0 to height and then round it
        int adjustedYPosition = (int)Mathf.Round(localPositionFromCenter.y + colourPickerHeight / 2);
        // clamp it so it can't exceed the object in the up or down directions
        adjustedYPosition = (int)Mathf.Clamp(adjustedYPosition, 0, colourPickerHeight);
        
        
        Vector2Int localPositionFromBottomLeft = new Vector2Int(adjustedXPosition, adjustedYPosition);
        // proportion of the x position in relation to the colour picker and its width
        // the more the x position approaches the width the more saturation, the more the x position approaches 0 the less the saturation
        float currentSaturation = localPositionFromBottomLeft.x / colourPickerWidth;

        // proportion of the y position in relation to the colour picker and its height
        // the more the y position approaches the height the more the brightness, the more the y position approaches 0 the less the brightness
        currentBrightness = localPositionFromBottomLeft.y / colourPickerHeight;
    
        // need the origin to be at the bottom left so the anchor should be set to bottom left
        pickerIcon.anchoredPosition = new Vector2(adjustedXPosition, adjustedYPosition);
        
        // invoke the event for when a colour is chosen
        OnColourPicked(Color.HSVToRGB(currentHue, currentSaturation, currentBrightness));
    }
    
    private void UpdateHueScrollerTexture() {
        RawImage scrollerImage = hueScroller.GetComponent<RawImage>();
        
        int hueTextureWidth = 16;
        
        Texture2D hueScrollTexture = new Texture2D(hueTextureWidth, 1);
        hueScrollTexture.wrapMode = TextureWrapMode.Clamp;
        Color32[] rowColours = new Color32[hueTextureWidth];
        
        for (int i = 0; i < hueTextureWidth; i++) {
            rowColours[i] = Color.HSVToRGB(i / (float)hueTextureWidth, 1, 1);
        }
        
        hueScrollTexture.SetPixels32(rowColours);
        hueScrollTexture.Apply();
        
        scrollerImage.texture = hueScrollTexture;
    }

    private void UpdateSaturationValueTexture(float hueValue) {
        hueValue = Mathf.Clamp01(hueValue);
        currentHue = hueValue;
        
        Texture2D saturationValueTexture = new Texture2D(pickerTextureDimensions, pickerTextureDimensions);
        saturationValueTexture.wrapMode = TextureWrapMode.Clamp;
        Color32[] colours = new Color32[pickerTextureDimensions * pickerTextureDimensions];
        
        int saturation = 0, brightness = 0;
        for (int y = 0; y < pickerTextureDimensions; y++) {
            for (int x = 0; x < pickerTextureDimensions; x++) {
                Color pixelColour = Color.HSVToRGB(hueValue, saturation / (float)pickerTextureDimensions, brightness / (float)pickerTextureDimensions);
                colours[y * pickerTextureDimensions + x] = pixelColour;
                
                saturation++;
            }
            saturation = 0;
            brightness++;
        }
        
        saturationValueTexture.SetPixels32(colours);
        saturationValueTexture.Apply();
        this.colourPickerImage.texture = saturationValueTexture;
    }
}
