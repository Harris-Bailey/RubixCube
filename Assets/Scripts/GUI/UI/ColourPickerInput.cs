using UnityEngine;
using UnityEngine.EventSystems;

public class ColourPickerMouseActions : MonoBehaviour, IDragHandler, IPointerClickHandler {    
    [SerializeField] private ColourPicker colourPicker;
    
    public void OnPointerClick(PointerEventData eventData) {
        colourPicker.ChooseColourOnPicker(eventData);
    }
    
    public void OnDrag(PointerEventData eventData) {
        colourPicker.ChooseColourOnPicker(eventData);
    }
}
