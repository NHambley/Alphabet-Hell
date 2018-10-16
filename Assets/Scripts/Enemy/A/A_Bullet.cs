﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Bullet : MonoBehaviour
{
    float speed;
    Vector2 position;
    Vector2 movement;
    SceneManagerScript sM;

    GameObject player;
	// Use this for initialization
	void Start ()
    {
        speed = 7;
        position = transform.position;
        movement = Vector2.down;
        player = GameObject.FindGameObjectWithTag("Player");
        sM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        position += movement * speed * Time.deltaTime;

        // every frame check collision with the player
        if(sM.CheckCollisions(player, gameObject))
        {
            // deal damage to the player and then destroy the bullet
            player.GetComponent<S_Player>().Health -= 10;
        }
        transform.position = position;
	}
}