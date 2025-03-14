using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColourPicker : MonoBehaviour {
    [SerializeField] private RawImage image;
    [SerializeField] private Scrollbar hueScroller;
    [SerializeField] private RectTransform indicator;
    private readonly int textureDimensions = 16;
    private float imageWidth, imageHeight;
    private float currentHue = 0, currentSaturation = 0, currentBrightness = 0;

    void Awake() {
        imageWidth = (int)image.rectTransform.rect.width;
        imageHeight = (int)image.rectTransform.rect.height;
        
        hueScroller.onValueChanged.AddListener((value) => {
            UpdateSaturationValueTexture(value);
        });
        UpdateSaturationValueTexture(0);
        UpdateHueScrollerTexture();
    }
    
    public Color GetCurrentColour() {
        return Color.HSVToRGB(currentHue, currentSaturation, currentBrightness);
    }
    
    public void SetInitialColourOnPicker(Color colour) {
        Color.RGBToHSV(colour, out currentHue, out currentSaturation, out currentBrightness);
        hueScroller.value = currentHue;
        
        PositionIconFromCurrents();
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
        
        Color colour = new Color(redValue, greenValue, blueValue);
        // convert the rgb values to hsv
        Color.RGBToHSV(colour, out currentHue, out currentSaturation, out currentBrightness);
        
        // the hue is between 0 and 1 so can directly set that to the slider value
        hueScroller.value = currentHue;
        
        PositionIconFromCurrents();
    }
    
    private void PositionIconFromCurrents() {
        // saturation is a value 0 to 1, multiply it by the width of the colour picker to get the position from 0 to height
        float xPosition = currentSaturation * imageWidth;
        // brightness is a value 0 to 1, multiply it by the height of the colour picker to get the position from 0 to width
        float yPosition = currentBrightness * imageHeight;
        
        // to keep both the positions inside the bounds of the box, the positions need
        // subtracting by their respective unit (height or width), and then offset by half of those units
        // this only works if the pivots are set to center on the pickerIcon
        xPosition = xPosition - indicator.rect.width + (indicator.rect.width / 2);
        yPosition = yPosition - indicator.rect.height + (indicator.rect.height / 2);
        
        // need the origin to be at the bottom left so the anchor should be set to bottom left
        indicator.anchoredPosition = new Vector2(xPosition, yPosition);
    }
    
    private void UpdateOutputImageColour(PointerEventData eventData) {
        // changes the mouse position from screen point which is its position on the users screen
        // to its position inside the actual UI component
        RectTransformUtility.ScreenPointToLocalPointInRectangle(image.rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPositionFromCenter);
        
        // from the center, the x value goes from left to right, from -width/2 to width/2
        // so need to offset it with the half width to get it from 0 to the width and then round it
        float adjustedXPosition = localPositionFromCenter.x + imageWidth / 2;
        // clamp it so the bounds of the icon can't exceed the bounds of the picker in the left or right directions
        adjustedXPosition = Mathf.Clamp(adjustedXPosition, 0 + (indicator.rect.width / 2), imageWidth - (indicator.rect.width / 2));
        
        // from the center the y value goes bottom to top, from -height/2 to height/2
        // so need to offset by half height which gets the values from 0 to height and then round it
        float adjustedYPosition = localPositionFromCenter.y + imageHeight / 2;
        //  clamp it so the bounds of the icon can't exceed the bounds of the picker in the up or down directions
        adjustedYPosition = Mathf.Clamp(adjustedYPosition, 0 + (indicator.rect.height / 2), imageWidth - (indicator.rect.height / 2));
        
        
        Vector2 localPositionFromBottomLeft = new Vector2(adjustedXPosition, adjustedYPosition);
        
        // proportion of the x position in relation to the colour picker and its width
        // the more the x position approaches the width the more saturation, the more the x position approaches 0 the less the saturation
        // the same is said for the brightness, except that occurs relative to the height
        currentSaturation = localPositionFromBottomLeft.x / imageWidth;
        currentBrightness = localPositionFromBottomLeft.y / imageHeight;
    
        // need the origin to be at the bottom left so the anchor should be set to bottom left
        indicator.anchoredPosition = new Vector2(adjustedXPosition, adjustedYPosition);
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
        
        Texture2D saturationBrightnessTexture = new Texture2D(textureDimensions, textureDimensions);
        saturationBrightnessTexture.wrapMode = TextureWrapMode.Clamp;
        Color32[] colours = new Color32[textureDimensions * textureDimensions];
        
        int saturation = 0, brightness = 0;
        for (int y = 0; y < textureDimensions; y++) {
            for (int x = 0; x < textureDimensions; x++) {
                Color pixelColour = Color.HSVToRGB(hueValue, saturation / (float)textureDimensions, brightness / (float)textureDimensions);
                colours[y * textureDimensions + x] = pixelColour;
                
                saturation++;
            }
            saturation = 0;
            brightness++;
        }
        
        saturationBrightnessTexture.SetPixels32(colours);
        saturationBrightnessTexture.Apply();
        image.texture = saturationBrightnessTexture;
    }
}
