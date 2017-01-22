using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorAnim : MonoBehaviour {

    float timeTracker = 0f;
    int animStep = 0;
    float animTickLength = 0.1f;
    bool running = false;

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
        timeTracker -= Time.deltaTime;
        if (running) {
            while (timeTracker <= 0) {
                timeTracker += animTickLength;
                Step();
            }
        }
        else {
            timeTracker = 0;
        }
        for (var i = 0; i < lines.Length; i++) {
            lines[i].GetComponent<Renderer>().enabled = i == animStep ;
        }
	}

    void Step() {
        animStep++;
        animStep %= lines.Length;
    }

    public void RunAnimation() {
        if (running) return;
        timeTracker = animTickLength;
        Step();
        running = true;
        GameObject.Find("LaserPointer").GetComponent<LaserDrawer>().Deactivate();
        AkSoundEngine.PostEvent("ConvOn", gameObject);
    }

    public void StopAnimation() {
        if (!running) return;
        running = false;
        AkSoundEngine.PostEvent("ConvOff", gameObject);
    }
}