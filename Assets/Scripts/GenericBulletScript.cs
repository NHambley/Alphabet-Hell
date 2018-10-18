using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBulletScript : MonoBehaviour {

    protected Vector3 velocity = new Vector3(0.0f,0.3f, 0.0f);
    protected Vector3 acceleration = Vector3.zero;
    public float damage;
    GameObject ownedBy;

    public GameObject OwnedBy
    {
        get { return ownedBy; }
    }

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
        UpdatePosition();
    }

    public void InitializeBullet(Vector3 vel, Vector3 accel, GameObject owner)
    {
        velocity = vel;
        acceleration = accel;
        ownedBy = owner;
    }

    public virtual void UpdatePosition()
    {
        velocity += acceleration;
        gameObject.transform.position += velocity;
    }

    public void SetAcceleration(Vector3 a)
    {
        acceleration = a;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
