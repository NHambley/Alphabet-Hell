﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss : GenericBossScript {

    Transform[] children;

    [SerializeField]
    GameObject bullet;
    public GameObject obstacle;

    float bTimer = 1.5f; // to keep track of when to fire another bullet
    float timerTrack = 1.5f;
    float obstacleTimerTrack = 4.0f;
    float bObstacleTimer = 4.0f;
    Vector3 obstaclePos;
    SceneManagerScript sM;
	AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        obstaclePos = transform.position;
        sM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

	}

	// Update is called once per frame
	void Update()
    {
        Attacking();
        spawnObstacle();
    }

    // rotate each frame by a set amount

    void Attacking()
    {
        // subtract time from the timer to fire bullets
        timerTrack -= Time.deltaTime;
        if (timerTrack <= 0)
        {
            // instantiate a new bullet
            sM.AddEnemyBullet(Instantiate(bullet, transform.position, Quaternion.identity));
            audioManager.PlaySound("boomerang");
            //Debug.Log("got here");
            timerTrack = bTimer;
        }
    }

    void spawnObstacle()
    {
        obstaclePos.x = Random.Range(-3.0f, 3.5f);

        obstacleTimerTrack -= Time.deltaTime;
        if (obstacleTimerTrack <= 0)
        {
            // instantiate a new bullet
            sM.AddEnemyBullet(Instantiate(obstacle, obstaclePos, Quaternion.identity));
            //Debug.Log("got here");
            obstacleTimerTrack = bObstacleTimer;
        }
    }

    public override void OnHit(Vector3 pos)
    {
        Health -= 2;
        audioManager.PlaySound("ting");
    }
}
