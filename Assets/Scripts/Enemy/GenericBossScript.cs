using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericBossScript : MonoBehaviour {

    private bool isDead = false;

    public Vector3 velocity;
    public Vector3 acceleration;
    public bool isActive = false;

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }


    private int health = 100;

    public int Health
    {
        get { return health; }
        set {
            health = value;
            if(health <= 0)
            {
                health = 0;
                isDead = true;
            }
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
        velocity += acceleration;
        gameObject.transform.position += velocity;
	}

    public abstract void OnHit(Vector3 pos);
}
