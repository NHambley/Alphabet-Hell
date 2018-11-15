﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ZBoss : GenericBossScript
{

    public GameObject bulletPrefab;
    SceneManagerScript manager;
    float lastShootTime = -10.0f;

    // Use this for initialization
    void Start()
    { 
        manager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Time.time - lastShootTime > 3.0f && gameObject.GetComponent<Renderer>().isVisible)
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    public void Shoot()
    {
        GameObject newBullet1 = Instantiate(bulletPrefab);
        newBullet1.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.GetComponent<SpriteRenderer>().bounds.extents.y, gameObject.transform.position.z - 1);
        newBullet1.GetComponent<E_ZBullet>().InitializeBullet(new Vector3(0f, -0.1f, 0f), new Vector3(0, 0, 0), gameObject);
        manager.AddEnemyBullet(newBullet1);
    }

    public override void OnHit(Vector3 pos)
    {
        gameObject.GetComponent<ParticleGenerator>().GenerateParticles(SPRITE.WATER, 3, pos, new Vector3(0.0f, 0.1f, 0.0f), new Vector3(0.05f, 0.05f, 0.05f), 90, 0.5f, -0.5f);
    }
}
