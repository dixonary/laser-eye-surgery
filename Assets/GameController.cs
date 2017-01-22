using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour {

    State _state = State.PRE;
    float _startCountdown = 3f;
    GameObject currentPart;

	// Use this for initialization
	void Start () {
        AkSoundEngine.PostEvent("LaserOn",this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        var cdt = GameObject.Find("StartCountdownText");
        var cdt_text = cdt.GetComponent<Text>();


        if (_startCountdown <= -0.9f) {
            cdt_text.enabled = false;
            _startCountdown = 0f;
        }
        else {
            _startCountdown -= Time.deltaTime;
            var dp = _startCountdown - Mathf.Floor(_startCountdown);
            cdt_text.color = new Color(cdt_text.color.r, cdt_text.color.g, cdt_text.color.b, dp);
            cdt.transform.localScale.Set(0.6f + 0.4f * dp, 0.6f + 0.4f * dp, 1f);
        }

        switch (_state) {
            case State.PRE:
                if (_startCountdown > 0) { 
                    cdt_text.text = "... " + Mathf.Floor(_startCountdown+1) + " ...";
                }else {
                    cdt_text.text = "!!! GO !!!";
                    newPart();
                    _state = State.RUNNING;
                }
                break;
            case State.RUNNING:
                var w = currentPart.GetComponentInChildren<Wound>();
                var rem = w.hitboxes.Count(x => !x.hit);
                if (rem == 0) {
                    newPart();
                }
                break;
            case State.DONE:
                break;
        }

    }

    void newPart() {
        if(currentPart != null) {
            currentPart.GetComponent<EnterExit>().Leave();
        }
        currentPart = Instantiate(Resources.Load("BodyPart")) as GameObject;
        GameObject.Find("Conveyor").GetComponent<ConveyorAnim>().RunAnimation();
    }
}

enum State { PRE, RUNNING, DONE }
