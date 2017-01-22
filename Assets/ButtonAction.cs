using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick() {
        //AkSoundEngine.PostEvent("StopTitle", gameObject);
        SceneManager.LoadScene("ggj17");
    }
}
