using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_E : GenericEnemyScript {

    // attributes
    Vector3 position, tPosition;
    float shieldHealth, damageTaken;
    public Vector3 speed;
    SceneManagerScript sceneManager;
    bool shieldOn;
    public Sprite E;
    public Sprite E_Shield;

    // Use this for initialization
    void Start()
    {
        position = gameObject.transform.position;
        velocity = speed;
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        //sceneManager.AddEnemy(gameObject);

        shieldHealth = Health;
        damageTaken = 10;
        shieldOn = true;
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

        CheckSprite();
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
        if (shieldHealth > 0 && shieldOn)
            shieldHealth -= damageTaken;
        else
        {
            shieldOn = false;
            Health -= (int)damageTaken;
        }
    }

    // Checking which sprite to be using
    void CheckSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (!shieldOn && sr.sprite.name == "E_Shield")
        {
            sr.sprite = E;
        }

    }
}
