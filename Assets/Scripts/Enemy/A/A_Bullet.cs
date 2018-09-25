using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Bullet : MonoBehaviour
{
    float speed;
    Vector2 position;
    Vector2 movement;
	// Use this for initialization
	void Start ()
    {
        speed = 5;
        position = transform.position;
        movement = Vector2.down;
	}
	
	// Update is called once per frame
	void Update ()
    {
        position += movement * speed * Time.deltaTime;
        transform.position = position;
	}
}
