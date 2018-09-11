using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBulletScript : MonoBehaviour {

    Vector3 velocity = new Vector3(0.0f,0.3f, 0.0f);
    Vector3 acceleration = Vector3.zero;

    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnBecameInvisible()
    {
        isDead = true;
    }

    private void FixedUpdate()
    {
        velocity += acceleration;
        gameObject.transform.position += velocity;
    }
}
