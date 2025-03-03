using UnityEngine;

public class CameraFOV : MonoBehaviour {
    [SerializeField] private Camera cam;

    void Awake() {
        SettingsSaveLoad.OnAnySettingChanged += UpdateCameraFOV;
    }
    
    private void UpdateCameraFOV() {
        cam.fieldOfView = SettingsSaveLoad.FieldOfView;
    }
}
