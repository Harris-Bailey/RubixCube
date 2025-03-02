using UnityEngine;

public class CameraFOV : MonoBehaviour {
    
    [SerializeField] private Camera cam;
    private void Update() {
        cam.fieldOfView = SettingsSaveLoad.GetIntFromKey(SettingsSaveLoad.FieldOfViewKey);
    }
}
