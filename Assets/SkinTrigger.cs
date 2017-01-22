using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D c) {
        AkSoundEngine.PostEvent("FleshOn", gameObject);
    }

    void OnTriggerStay2D(Collider2D c) {

    }

    void OnTriggerExit2D(Collider2D c) {
        AkSoundEngine.PostEvent("FleshOff", gameObject);
    }




}
