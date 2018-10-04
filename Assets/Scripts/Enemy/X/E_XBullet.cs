using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_XBullet : GenericBulletScript {

    Vector3 startingPos;
    public float angleVel = Mathf.PI / 50;
    public bool opposite = false;

	// Use this for initialization
	void Start () {
        startingPos = gameObject.transform.position;
        damage = 10.0f;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        angleVel += Mathf.PI / 50;
        if (angleVel > Mathf.PI * 2)
        {
            angleVel = angleVel - Mathf.PI * 2;
        }
        
        UpdatePosition();
    }

    public override void UpdatePosition()
    {
        if (opposite) gameObject.transform.position += new Vector3(-Mathf.Cos(angleVel) * 0.1f, -0.15f, 0);
        else gameObject.transform.position += new Vector3(Mathf.Cos(angleVel) * 0.1f, -0.15f, 0);
    }
}
