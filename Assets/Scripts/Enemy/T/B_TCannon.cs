﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_TCannon : MonoBehaviour
{
    /// <summary>
    /// Each gun is meant track the player and make sure that their cannons do not rotate too far past their tracking point
    /// if they do stop the firing timer
    /// </summary>
    
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject bullet;

    public float chargeTimer = 2.0f;
    [SerializeField]
    float timer = 2.0f;

    [SerializeField, Range(20,50)]
    float rotSpeed;

    bool seePlayer = false;// determines if the timer is subtracted from 

    SceneManagerScript sm;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
        rotSpeed = 20f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SeePlayer();

        // if they see the player subtract from the timer and rotate them to look towards the player
        if(seePlayer)
        {
            timer -= Time.deltaTime;
            Vector3 dir = player.transform.position - transform.position;
            dir = gameObject.transform.InverseTransformDirection(dir);
            float angleBetween = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotSpeed * angleBetween); 
        }

        if (timer <= 0)
            Fire();
	}

    // check if the cannon is able to actually see the player
    void SeePlayer()
    {
        if (player != null)
        {
            // get raycast hit info from the transform of the cannon to the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y));

            // if it hits the player decrement the charge timer, if not don't
            if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            {
                seePlayer = true;
            }
            else
                seePlayer = false;
        }
    }

    // fire a bullet at the player's current position
    void Fire()
    {
        sm.AddEnemyBullet(Instantiate(bullet, transform.position, Quaternion.identity));
        timer = chargeTimer;
    }
}
