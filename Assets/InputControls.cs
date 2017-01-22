using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControls : MonoBehaviour {

    private static InputMode _inputMode = InputMode.UNKNOWN;
    public static InputMode inputMode {
        get {
            var res = Tobii.EyeTracking.EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus;
            Debug.Log(res);
            if (res == Tobii.EyeTracking.DeviceStatus.Pending || res == Tobii.EyeTracking.DeviceStatus.Tracking) {
                return InputMode.EYES;
            }
            else {
                return InputMode.MOUSE;
            }
        }
    }

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum InputMode { MOUSE, EYES, UNKNOWN }