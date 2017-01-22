using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControls : MonoBehaviour {

    private static InputMode _inputMode;
    public static InputMode inputMode {
        get {
            if (inputMode == InputMode.UNKNOWN) {
                var res = Tobii.EyeTracking.EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus;
                if (res == Tobii.EyeTracking.DeviceStatus.Pending || res == Tobii.EyeTracking.DeviceStatus.Tracking) {
                    _inputMode = InputMode.EYES;
                }
                else _inputMode = InputMode.MOUSE;
            }
            return _inputMode;
        }
    }

	// Use this for initialization
	void Start () {
        _inputMode = InputMode.UNKNOWN;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum InputMode { MOUSE, EYES, UNKNOWN }