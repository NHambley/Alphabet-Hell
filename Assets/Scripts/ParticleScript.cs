using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {
    float startTime;
    public bool isDead = false;
    public float timeout = 0.0f;
    public Vector3 velocity;
    public Vector3 acceleration;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(Time.time > startTime + timeout)
        {
            isDead = true;
        }
        else
        {
            velocity += acceleration;
            gameObject.transform.position += velocity;
            Color current = gameObject.GetComponent<SpriteRenderer>().color;
            current.a = 1 - ((Time.time - startTime) / timeout);
            gameObject.GetComponent<SpriteRenderer>().color = current;
        }
	}

    public void SetSprite(Sprite s)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = s;
    }
}
