using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCube : MonoBehaviour {
    
    private Camera cam;
    [SerializeField] private Color[] colours;
    private int activeColourIndex;

    void Awake() {
        cam = Camera.main;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            activeColourIndex = Modulo(activeColourIndex + 1, colours.Length);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            activeColourIndex = Modulo(activeColourIndex - 1, colours.Length);
            
        UpdateFaceletColours();
    }
    
    private void UpdateFaceletColours() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                if (hit.transform.TryGetComponent(out MeshRenderer mr)) {
                    mr.material.color = colours[activeColourIndex];
                }
            }
        }
    }
    
    private int Modulo(int value, int moduloValue) {
        return (Mathf.Abs(value * moduloValue) + value) % moduloValue;
    }
}
