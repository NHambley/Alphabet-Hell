using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_JBullet : GenericBulletScript {
    
    float prevTime = 0.0f;
    bool jack = false;

	// Use this for initialization
	void Start () {
        damage = 10.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time - prevTime > 0.1f && !jack)
        {
            jack = true;
            prevTime = Time.time;
        }
        else if (Time.time - prevTime > 0.01f && jack)
        {
            jack = false;
            prevTime = Time.time;
        }
	}

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    public override void UpdatePosition()
    {
        if(!jack) gameObject.transform.position += velocity;
        else gameObject.transform.position -= velocity;
    }
}
