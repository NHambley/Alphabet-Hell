﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_T : MonoBehaviour
{
    /// <summary>
    /// the T enemy moves slowly around the screen and after it reaches its desired position, takes aim, and fires a fast bullet towards the player
    /// </summary>
    
    
    GameObject target; // where the enemy is at the point of charging up the shot
    float chargeTimer = 2.0f;
    float timer = 2.0f;

    Vector2 ePosition;// position of this enemy
    //Rigidbody2D rb;
    Vector3 eulerAngleVelocity;
    Vector2 nextPos;
    [Range(1,5), SerializeField]
    float speed;// speed for the enemy
    
    [SerializeField]
    GameObject bullet;

    Camera cam;
    float hp;

    bool firing = false;// is the enemy firing a bullet or not?
	// Use this for initialization
	void Start ()
    {
        hp = 20;
        target = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;

        speed = 1.0f;

        ePosition = transform.position;
        //rb = GetComponent<Rigidbody2D>();
        // Find the first position the tank will move to
        FindPosition();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		// if the tank is firing call the methods required, else it is moving to its next position 
        if(firing)
        {
            Fire();
        }
        else if(firing == false)// moving positions
        {
            Move();
        }
        transform.position = ePosition;
	}



    void Fire()
    {
        // mark down the fire timer and have the player track the movements of the player
        if(timer <= 0)
        {
            // fire a bullet and find a new position\
            Instantiate(bullet, gameObject.transform.GetChild(0).transform);
        }
        else
        {
            timer -= Time.deltaTime;
            transform.rotation = new Quaternion(0, 0, Vector2.Angle(ePosition, target.transform.position) * Time.deltaTime, 0);
        }
    }

    // move the enemy to its next position then change the firing bool to true
    void Move()
    {
        // first get the angle between the enemy and the target position to rotate it
        Vector2 moveFrame = (nextPos - (Vector2)transform.position);
        moveFrame = moveFrame.normalized * speed * Time.deltaTime;
        ePosition += moveFrame;

        if(Vector2.Distance(ePosition, target.transform.position) < .5f)
        {
            firing = true;
        }
    }

    // find a new position for the tank to move to
    void FindPosition()
    {
        nextPos = new Vector2(Random.Range(-(2f * cam.orthographicSize) * cam.aspect, (2f * cam.orthographicSize) * cam.aspect), target.transform.position.y + 2);
    }
}
