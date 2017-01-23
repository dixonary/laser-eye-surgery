using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class valset : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Text>().text = GameController._completed.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
