using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class PartGen : MonoBehaviour {

    /* Generate a body part at random. */
    GameObject skin;
    GameObject lines;

    String[] parts = { "arm", "leg" };

    void Start () {

        var part = parts[(int)(UnityEngine.Random.value * parts.Length)];
        SpriteRenderer sr;
        Sprite res;
        
        lines = new GameObject();
        sr = lines.AddComponent<SpriteRenderer>();
        res = Resources.Load<Sprite>("Images/" + part + "_lines");
        sr.sprite = res;
        sr.transform.parent = transform;


        skin = new GameObject();
        sr = skin.AddComponent<SpriteRenderer>();
        res = Resources.Load<Sprite>("Images/"+part + "_colour");;
        sr.sprite = res;

        float r, g, b;
        Func<float> colRandom = (() => UnityEngine.Random.value * 1 - 0.5f);
        if (UnityEngine.Random.value > 0.5) {
            // lighter skin tones (http://johnthemathguy.blogspot.co.uk/2013/08/what-color-is-human-skin.html)
            r = 235.3f + 9.6f* colRandom();
            g = 193.1f + 17f * colRandom();
            b = 177.6f + 21f * colRandom();

        }
        else {
            // darker skin tones
            r = 168.8f + 38.5f * colRandom();
            g = 122.5f + 32.1f * colRandom();
            b =  96.7f + 26.3f * colRandom();
        }

        sr.color = new Color(r / 255f, g / 255f, b / 255f);
        sr.transform.parent = transform;
        sr.transform.localPosition = new Vector3(sr.transform.localPosition.x, sr.transform.localPosition.y, sr.transform.localPosition.z + 0.11f);

        /* Must add polygon collider after sprite */
        var polygon = skin.AddComponent<PolygonCollider2D>();
        skin.AddComponent<Rigidbody2D>().isKinematic = true;
        skin.AddComponent<SkinTrigger>();

        var woundObject = new GameObject();
        woundObject.transform.parent = transform;
        //woundObject.AddComponent<MeshFilter>(); // This might happen automatically because of Wound's RequireComponent?
        var woundComponent = woundObject.AddComponent<Wound>();
        woundComponent.partName = part;
        //woundComponent.generateWoundWithin(polygon);


        transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        //transform.localPosition = new Vector3(0f, 0f, 1.0f);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, (UnityEngine.Random.value - 0.5f) * 40);
    }
	
	void Update () {
        var s = /*25f / 127;*/ MidiMaster.GetKnob(21, 0.15f);
        gameObject.transform.localScale = new Vector3(s,s,s);
        var r = /*25f / 127;*/ 10*MidiMaster.GetKnob(22, 0.15f);
        //gameObject.transform.rotation = new Quaternion(0, 0, 1, r);
    }
}
