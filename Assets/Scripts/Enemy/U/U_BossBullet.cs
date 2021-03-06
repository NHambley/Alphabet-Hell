﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_BossBullet : GenericBulletScript {

    // Use this for initialization
    Vector3 position;
    GameObject player;
    SceneManagerScript sM;
    int lOrR;
    float distanceTravelled;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
        
        position = transform.position;

        distanceTravelled = 0;
        if (transform.position.x >= 1)
        {
            lOrR = 0;
        }
        if (transform.position.x <= -1)
        {
            lOrR = 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        position.y -= .01f;
        if (lOrR == 0)
        {
            if (distanceTravelled < 1.75f)
                position.x += .02f;
            else
                position.x -= .02f;
        }
        if (lOrR == 1)
        {
            if (distanceTravelled < 1.75f)
                position.x -= .02f;
            else
                position.x += .02f;  
        }
        distanceTravelled += .01f;
        if (distanceTravelled > 3.5f)
            distanceTravelled = 0;



        transform.position = position;

    }

}
