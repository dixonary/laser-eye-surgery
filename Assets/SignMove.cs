using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignMove : MonoBehaviour
{

    float _initY;
    float _period = 3f;
    float ticker = 0f;
    // Use this for initialization
    void Start() {
        _initY = transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        ticker += Time.deltaTime;
        ticker %= _period;
        transform.position = new Vector3(transform.position.x, _initY + 10 * Mathf.Sin(ticker/_period*Mathf.PI*2));
    }
}
