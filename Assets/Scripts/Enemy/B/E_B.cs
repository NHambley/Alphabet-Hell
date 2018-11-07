using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_B : GenericEnemyScript {
    Vector3 position, tPosition;
    Vector2 ePosition; // positino of this gameobject
    float damageTaken;
    public Vector2 speed;
    SceneManagerScript sceneManager;

    float bTimer = 0.5f; // to keep track of when to fire another bullet
    float timerTrack = 0.5f;



    // get a reference to the state manager
    [SerializeField]
    bool state;

    [SerializeField]
    GameObject bullet;

    float hp;

    Camera cam;
    public override void OnHit(Vector3 pos)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
        position = gameObject.transform.position;
        velocity = speed;
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();

    }
    void Move()
    {
        position += -velocity;
        transform.position = position;
    }
    // Update is called once per frame
    void FixedUpdate () {
        Move();
	}

    void Attacking()
    {
        // using the position that is set in the resetting method (or if it is the first attacking run of this enemy the one set in Start)have the enemy move down in some form towards that position 

        // subtract time from the timer to fire bullets
        timerTrack -= Time.deltaTime;
        if (timerTrack <= 0)
        {
            // instantiate a new bullet
            Instantiate(bullet, transform.position, Quaternion.identity);

            timerTrack = bTimer;
        }



    }
    void EnemyOffScreen()
    {
        if (this.position.y > Screen.height - 1)
        {
            Destroy(this);
        }
    }
}
