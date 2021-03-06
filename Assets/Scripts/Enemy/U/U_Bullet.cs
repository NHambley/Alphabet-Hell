﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Bullet : GenericBulletScript {

    float speed;
    Vector2 position;
    
    float xRot;
    Vector2 movement;
    SceneManagerScript sM;
    int leftOrRight;
    int lengthOfArc;
    int lengthOfArc1;
    GameObject player;



    // Use this for initialization
    void Start()
    {
        speed = 700;
        position = transform.position;
        movement = Vector2.down;
        player = GameObject.FindGameObjectWithTag("Player");
        sM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();

        //transform.eulerAngles = (Vector3.down);

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (position.x < player.transform.position.x)
            {
                position.x += .008f;
            }
            else if (position.x > player.transform.position.x)
            {
                position.x -= .008f;
            }
            position.y -= .04f;

            transform.up = (transform.position - player.transform.position).normalized;
            transform.position = position;
        }
        position.y -= .04f;

        transform.position = position;
    }
}
