using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(BoxCollider2D))]
public class WoundHitbox : MonoBehaviour {

    bool _hit = false;
    public bool hit { 
        get {
            return _hit;
        }
    }

    // Use this for initialization
    void Start () {
        var body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        var spr = GetComponent<SpriteRenderer>();
        spr.sprite = Resources.Load<Sprite>("Square");
        spr.material.color = new Color(1f, 0f, 0f, 0.5f);
        gameObject.AddComponent<BoxCollider2D>();
    }

	void Update ()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_hit)
        {
            var spr = GetComponent<SpriteRenderer>();
            spr.material.color = new Color(0.0f, 0f, 0f, 0.2f);
            spr.transform.localScale = new Vector3(spr.transform.localScale.x, spr.transform.localScale.y / 2, spr.transform.localScale.z);
            _hit = true;
        }
    }

}
