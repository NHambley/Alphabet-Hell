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
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        if (!opposite)
        {
            angleVel += Mathf.PI / 50;
            if (angleVel > Mathf.PI * 2)
            {
                angleVel = angleVel - Mathf.PI * 2;
            }
        }
        else
        {
            angleVel -= Mathf.PI / 50;
            if (angleVel < -Mathf.PI * 2)
            {
                angleVel = angleVel + Mathf.PI * 2;
            }
        }
        
        Debug.Log(Mathf.Cos(angleVel));
        UpdatePosition();
    }

    public override void UpdatePosition()
    {
        gameObject.transform.position += new Vector3(Mathf.Cos(angleVel) * 0.1f, -0.15f, 0);
    }
}
