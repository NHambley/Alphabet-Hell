using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ZBullet : GenericBulletScript {
    
    public float angleVel = Mathf.PI / 50;

	// Use this for initialization
	void Start () {
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
        gameObject.transform.position += velocity;
        gameObject.transform.localScale = new Vector3(Mathf.Sin(angleVel), Mathf.Sin(angleVel), 1);
    }
}
