using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;

public class EyeTrackCursor : MonoBehaviour {

    private GazeAware _gazeAware;

    // Use this for initialization
    void Start () {
        _gazeAware = GetComponent<GazeAware>();
	}

    // Update is called once per frame
    void Update()
    {
    }
}