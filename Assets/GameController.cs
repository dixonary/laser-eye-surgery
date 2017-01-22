using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    State _state = State.PRE;
    float _startCountdown = 3f;
    public static int _completed;
    float initialTime = 60f;
    float _timeLeft;

    float _timeGain = 12f;
    float _minTime = 4f;
    GameObject currentPart;

	// Use this for initialization
	void Start () {
        _timeLeft = initialTime;
        _completed = 0;
        AkSoundEngine.PostEvent("StopTitle", gameObject);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        var cdt = GameObject.Find("StartCountdownText");
        var cdt_text = cdt.GetComponent<Text>();


        if (_startCountdown <= -0.9f) {
            cdt_text.enabled = false;
            _startCountdown = 0f;
        }
        else {
            _startCountdown -= Time.fixedDeltaTime;
            var dp = _startCountdown - Mathf.Floor(_startCountdown);
            //cdt_text.color = new Color(cdt_text.color.r, cdt_text.color.g, cdt_text.color.b, dp);
            cdt.transform.localScale = new Vector3(dp*dp, dp*dp, 1f);
        }

        switch (_state) {
            case State.PRE:
                if (_startCountdown > 0) { 
                    cdt_text.text = "... " + Mathf.Floor(_startCountdown+1) + " ...";
                }else {
                    cdt_text.text = "!!! GO !!!";
                    newPart();
                    _state = State.RUNNING;
                    AkSoundEngine.PostEvent("EndMusic", gameObject);
                    AkSoundEngine.SetState("MusicStates", "None");
                }
                break;
            case State.RUNNING:
                _timeLeft -= Time.fixedDeltaTime;
                GameObject.Find("TimerText").GetComponent<Text>().text = (Mathf.Round(_timeLeft * 100) / 100).ToString(".00");
                var w = currentPart.GetComponentInChildren<Wound>();
                var rem = w.hitboxes.Count(x => !x.hit);
                if (rem == 0) {
                    _completed++;
                    _timeLeft += _timeGain;
                    _timeGain = (_timeGain - _minTime) * 0.9f + _minTime;
                    AkSoundEngine.SetRTPCValue("CompletedLimbs", _completed);
                    AkSoundEngine.PostEvent("ding", gameObject);
                    GameObject.Find("ScoreText").GetComponent<Text>().text = _completed.ToString();
                    Wound._maxSplitsPerUnitLength+= 0.15f;
                    Wound._maxWobbleAmount += 0.15f;
                    Wound._minimumWoundLength = Mathf.FloorToInt(_completed/5);
                    Wound._splitsMod = Mathf.FloorToInt(_completed / 5);
                    newPart();
                }
                if (_timeLeft > 30) {
                    AkSoundEngine.SetState("MusicStates", "None");
                }
                if (_timeLeft > 20 && _timeLeft <= 30) {
                    Debug.Log("Play!");
                    AkSoundEngine.SetState("MusicStates", "First");
                }
                if (_timeLeft > 10 && _timeLeft <= 20) {
                    AkSoundEngine.SetState("MusicStates", "Second");
                }
                if (_timeLeft > 0 && _timeLeft <= 10) {
                    AkSoundEngine.SetState("MusicStates", "Third");
                }
                if (_timeLeft <= 0) {
                    Debug.Log("test");
                    SceneManager.LoadScene("gameover");
                    AkSoundEngine.SetState("MusicStates", "Final");
                }
                AkSoundEngine.SetRTPCValue("Progress",(33-_timeLeft)*100f/30f);
                break;
            case State.DONE:

                AkSoundEngine.SetState("MusicStates", "First");
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
