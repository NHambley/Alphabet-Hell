using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_E : GenericEnemyScript {

    // attributes
    Vector3 position, tPosition;
    float shieldHealth;
    public Vector3 speed;
    SceneManagerScript sceneManager;

    // Use this for initialization
    void Start()
    {
        position = gameObject.transform.position;
        velocity = speed;
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        //sceneManager.AddEnemy(gameObject);

        shieldHealth = Health * 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        // Collisions
        foreach (GameObject playerBullet in GameObject.FindGameObjectsWithTag("BulletP"))
        {
            Debug.Log(playerBullet.tag);
            if (sceneManager.CheckCollisions(gameObject, playerBullet))
            {
                if (shieldHealth <= 0)
                    Health -= 5;
            }
        }
    }

    // movement method
    void Move()
    {
        position += -velocity;
        transform.position = position;
    }

    // On Hit function
    public override void OnHit()
    {
        
    }
}
