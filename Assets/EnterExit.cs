using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExit : MonoBehaviour {

    float _targetX = 0f;
    public float speed = 10f;
    float initialPosition = Camera.main.aspect + 2.5f;

    // Use this for initialization
    void Start () {

        transform.localPosition =  new Vector3(initialPosition, 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x > _targetX) {
            transform.localPosition = new Vector3(transform.localPosition.x-speed*Time.deltaTime, 0f, 1f);
            //transform.Translate(new Vector3(-speed*Time.deltaTime, 0f));
        }

        // reached end
        if (transform.localPosition.x < _targetX) { 
            transform.localPosition = new Vector3(_targetX, 0f, 1f);
            GameObject.Find("Conveyor").GetComponent<ConveyorAnim>().StopAnimation();

        }

        if(transform.localPosition.x <= -2f * Camera.main.aspect) {
            Object.Destroy(this);
        }
	}

    public void Leave() {
        _targetX = -2f * Camera.main.aspect;
    }
}
