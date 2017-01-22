using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTrackingHideText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (InputControls.inputMode == InputMode.EYES) {
            enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
