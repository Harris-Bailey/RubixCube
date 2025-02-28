using UnityEngine;
using UnityEngine.EventSystems;

public class ColourPickerMouseActions : MonoBehaviour, IDragHandler, IPointerClickHandler {    
    [SerializeField] private ColourPickerUpdater colourPicker;
    
    public void OnPointerClick(PointerEventData eventData) {
        colourPicker.ChooseColourOnPicker(eventData);
    }
    
    public void OnDrag(PointerEventData eventData) {
        colourPicker.ChooseColourOnPicker(eventData);
    }
}
