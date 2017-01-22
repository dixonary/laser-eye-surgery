using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBG : MonoBehaviour {

    RawImage _image;
	// Use this for initialization
	void Start () {
        _image = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var rect = _image.uvRect;
        rect.x += 0.005f;
        rect.y += 0.005f;
        rect.x += 0.005f;
        rect.y += 0.005f;
        _image.uvRect = rect;
    }
}
