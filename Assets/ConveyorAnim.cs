using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorAnim : MonoBehaviour {

    float timeTracker = 0f;
    int animStep = 0;
    float animTickLength = 0.1f;

    GameObject bg;
    GameObject[] lines;

    // Use this for initialization
    void Start () {
        SpriteRenderer sr;
        Sprite res;

        lines = new GameObject[3];
        for (var i = 0; i < lines.Length; i++) {
            lines[i] = new GameObject();
            sr = lines[i].AddComponent<SpriteRenderer>();
            res = Resources.Load<Sprite>("Images/conveyor-"+(i+1));
            sr.sprite = res;
            sr.transform.parent = transform;
        }
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        transform.localPosition = new Vector3(0f, 0f, 2.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if(animStep != 0) {
            timeTracker += Time.deltaTime;
            while(timeTracker >= animTickLength) {
                timeTracker -= animTickLength;
                animStep++;
                animStep %= lines.Length;
                if(animStep == 0) {
                    timeTracker = 0;
                    break;
                }
            }
        }
        for (var i = 0; i < lines.Length; i++) {
            lines[i].GetComponent<Renderer>().enabled = i == animStep ;
        }

        if(Input.GetKeyDown(KeyCode.S)) {
            RunAnimation();
        }
	}

    public void RunAnimation() {
        animStep = 1;
    }
}