using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class MidiUser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(MidiMaster.GetKnob(21));
	}
}
