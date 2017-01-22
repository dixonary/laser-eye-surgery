using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTrackingHideText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        enabled = !(InputControls.inputMode == InputMode.EYES) ;
    }
}
