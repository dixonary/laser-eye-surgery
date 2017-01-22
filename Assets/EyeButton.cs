using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tobii.EyeTracking;

[RequireComponent(typeof(GazeAware))]
public class EyeButton : MonoBehaviour {

    private GazeAware _ga;
    private float _ticker = 0f;
    private float _maxTime = 2.5f;
	// Use this for initialization
	void Start () {
        _ga = GetComponent<GazeAware>();
	}
	
	// Update is called once per frame
	void Update () {
        var _gp = EyeTracking.GetGazePoint();
        if(_gp.IsValid) {
            float x = transform.position.x / Screen.width;
            float y = transform.position.y / Screen.height;
            float diam = (transform as RectTransform).rect.width/Screen.width;
            bool focused = (new Vector2(x,y) - _gp.Viewport).magnitude < diam*3;

            if (focused) {
                _ticker += Time.deltaTime;
                if (_ticker > _maxTime) {
                    InputControls.inputMode = InputMode.EYES;
                    //AkSoundEngine.PostEvent("StopTitle", gameObject);
                    SceneManager.LoadScene("ggj17");
                }
            }
            else {
                _ticker *= 0.9f;
            }
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, _ticker*_ticker*300));
	}
}
