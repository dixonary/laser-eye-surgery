using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;

public class InputControls : MonoBehaviour {

    public static InputMode inputMode = InputMode.MOUSE;
    private static GazePoint lastGP = new GazePoint(new Vector2(0,0),-1,Time.time);

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }

    public static Vector2 getPosition() {
        
        switch (inputMode) {
            case InputMode.MOUSE:
                var mp = Input.mousePosition;
                return new Vector2(mp.x / Screen.width, mp.y / Screen.height);
            case InputMode.EYES:
                var gp = EyeTracking.GetGazePoint();
                if (gp.IsValid) {
                    lastGP = gp;
                    return gp.Viewport;
                }
                else return lastGP.Viewport;
            case InputMode.UNKNOWN:
                throw new System.Exception("Input mode is unknown!");
        }
        return new Vector2();

    }
}

public enum InputMode { MOUSE, EYES, UNKNOWN }