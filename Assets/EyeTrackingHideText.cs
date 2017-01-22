using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii;

public class EyeTrackingHideText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        enabled = !(InputControls.inputMode == InputMode.EYES);
        var res = Tobii.EyeTracking.EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus;
        Debug.Log(res);
    }
}
