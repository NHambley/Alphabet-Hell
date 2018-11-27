﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_C : GenericBossScript
{

    // attributes
    Vector3 position;
    float damageTaken;
    //List<GameObject> bullets;
    List<GameObject> firingPositions;
    List<float> shotTimers;

    public float shotTimerMax;
    public GameObject bulletBigPrefab, bulletSmallPrefab;
    public Vector3 bulletSpeed;

    SceneManagerScript sceneManager;

    // Destructor

    // Use this for initialization
    void Start()
    {
        firingPositions = new List<GameObject>();
        shotTimers = new List<float>();
        position = gameObject.transform.position;
        for (int i = 0; i < 3; i++)
        {
            firingPositions.Add(transform.GetChild(i).gameObject);
        }

        shotTimers.Add(1);
        shotTimers.Add(1);
        shotTimers.Add(1);
        //bullets = new List<GameObject>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        damageTaken = 1.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < shotTimers.Count; i++)
        {
            // Firing
            if (i == 0)
            {
                if (shotTimers[i] == 0 || shotTimers[i] >= shotTimerMax * 1.5f)
                {
                    shotTimers[i] = 0;
                    Fire(i);
                }
                else
                    shotTimers[i]++;
            }
            else
            {
                if (shotTimers[i] == 0 || shotTimers[i] >= shotTimerMax)
                {
                    shotTimers[i] = 0;
                    Fire(i);
                }
                else
                    shotTimers[i]++;
            }
        }
    }

    // firing method
    void Fire(int num)
    {
        GenerateBullet(num);
        shotTimers[num]++;
    }

    // creating a bullet
    void GenerateBullet(int num)
    {
        GameObject newBullet;
        if (num == 0)
            newBullet = Instantiate(bulletBigPrefab);
        else
            newBullet = Instantiate(bulletSmallPrefab);

        newBullet.transform.position = firingPositions[num].transform.position;
        newBullet.GetComponent<CorkBullet>().InitializeBullet(bulletSpeed, Vector3.zero, gameObject);
        sceneManager.GetComponent<SceneManagerScript>().AddEnemyBullet(newBullet);
    }

    public override void OnHit(Vector3 pos)
    {
        Health -= (int)damageTaken;
    }
}
