using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSwing : MonoBehaviour {


    public float direction=1f;
    public int baseAngle = 5;
    public float delay = 0f;
    float _initY;
    float _period = 3f;
    float ticker = 0.3f;
    // Use this for initialization
    void Start() {
        _initY = transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        ticker += Time.deltaTime;
        ticker %= _period;
        transform.localRotation = Quaternion.Euler(0,0,baseAngle+(5*Mathf.Sin((delay+ticker/_period)*Mathf.PI*2)*direction));
    }
}
