﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Player : MonoBehaviour
{
    public GameObject sceneManager;

    SceneManagerScript managerScript;

    // look at https://docs.unity3d.com/Manual/MobileInput.html for touch controls
    // and https://docs.unity3d.com/Manual/HOWTO-UIMultiResolution.html for resizing the screen depending on mobile device
    int score;
    int health;
    int lives;

    float timer;

    [SerializeField, Range(0,20)]
    float seekScalar;
    [SerializeField, Range(0, 20)]
    float maxSpeed;

    //float yPos; // keep the y position at the same value all game

    // used to determine the movements of the player's mouse/finger this will be used to determine the direction the player is moved
    Vector2 prevMPos; 
    Vector2 mPosition;
    Vector2 pPosition;// keep track of where the player is
    Vector2 pVelocity = Vector2.zero;
    Vector2 force = Vector2.zero;


    //Variables for touch controls 
    Vector2 fingerPos;
    // end variables for touch controls
    [SerializeField]
    GameObject bullet;

    float lastBulletTime = -1.0f;

	// Use this for initialization
	void Start ()
    {
        mPosition = Input.mousePosition;
        pPosition = gameObject.transform.position;
        managerScript = sceneManager.GetComponent<SceneManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // check input here then call the shoot and move methods depending on whether it's true or not
        if(Input.GetMouseButton(0))
        {
            //mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Move(mPosition);
            Shoot();
        }
        else
        {
            pVelocity *= 0.95f;
        }
        // lock the y position, just have the player moving side to side for now
        pPosition += pVelocity;
        force = Vector2.zero;
        gameObject.transform.position = pPosition;

        //Debug.Log(mPosition);
    }

    // periodically have the player shoot a projectile using a timer and a cooldown 
    void Shoot()
    {
        if(Time.time - lastBulletTime >= 0.1f || lastBulletTime == -1.0f)
        {
            GameObject newBullet = Instantiate(bullet,gameObject.transform.position,Quaternion.identity);
            //newBullet.transform.position.Set(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            managerScript.AddPlayerBullet(newBullet);
            lastBulletTime = Time.time;
        }
    }

    // move the player in relation to how the player is moving their finger across the screen. Don't match the player and finger positions instead
    // see the direction the finger/mouse is moving in and then apply that movement to the player
    // compare the finger (mouse) position to its position last frame, normalize that vector, multiply by Time.deltaTime, then add it to the player position
    void Move(Vector2 mPos)
    {
        float distanceRatio = (mPosition - pPosition).magnitude;
        if(distanceRatio >= 0.01f)
        {
            if (distanceRatio > 1f) distanceRatio = 1f;
            //else if (distanceRatio < 0.3f) distanceRatio = 0.3f;
            Vector2 desiredVelocity = (mPosition - pPosition).normalized * seekScalar;
            force = (desiredVelocity - pVelocity) * Time.deltaTime;
            force = new Vector2(force.x, 0);
            if (distanceRatio < 1) pVelocity *= 0.96f;
            pVelocity += force;
            pVelocity = Vector2.ClampMagnitude(pVelocity, maxSpeed);
        }
        else if(pVelocity.magnitude <= 0.05f)
        {
            pVelocity = Vector2.zero;
        }

        /*
        prevMPos = mPosition;
        mPosition = Input.mousePosition;
        Vector2 moveVec = mPosition - prevMPos;
        moveVec = new Vector2(moveVec.x, 0);
        moveVec.Normalize();
        pPosition += moveVec * Time.deltaTime * seekScalar;
        // check if the player is heading off screen by comparing the x value of moveVec 
        pPosition += moveVec * Time.deltaTime * speed;
        */

    }

    /*
    // currently testing this 
    // uses the players touch positions to move the player instead of mouse position
    void TouchMove()
    {
        fingerPos = Input.GetTouch(0).position;
        Debug.Log("Touch: " + Input.GetTouch(0).deltaPosition);
        float distanceRatio = (fingerPos - pPosition).magnitude;
        if (distanceRatio >= 0.01f)
        {
            if (distanceRatio > 1f) distanceRatio = 1f;
            //else if (distanceRatio < 0.3f) distanceRatio = 0.3f;
            Vector2 desiredVelocity = (fingerPos - pPosition).normalized * seekScalar;
            force = (desiredVelocity - pVelocity) * Time.deltaTime;
            force = new Vector2(force.x, 0);
            if (distanceRatio < 1) pVelocity *= 0.96f;
            pVelocity += force;
            pVelocity = Vector2.ClampMagnitude(pVelocity, maxSpeed);
        }
        else if (pVelocity.magnitude <= 0.05f)
        {
            pVelocity = Vector2.zero;
        }
    }
    */
}