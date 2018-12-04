using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_B : GenericEnemyScript {
    Vector3 position, tPosition;
    Vector2 ePosition; // positino of this gameobject
    float damageTaken;
    public Vector2 speed;
    SceneManagerScript sceneManager;

    float bTimer = 2.0f; // to keep track of when to fire another bullet
    float timerTrack = 2.0f;



    // get a reference to the state manager
    [SerializeField]
    bool state;

    [SerializeField]
    GameObject bullet;

    Camera cam;
    public override void OnHit(Vector3 hit)
    {
        Health -= 50;
    }

    // Use this for initialization
    void Start () {
        position = gameObject.transform.position;
        velocity = speed;
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        Health = 100;
    }
    void Move()
    {
        position += -velocity;
        transform.position = position;
    }
    // Update is called once per frame
    void FixedUpdate () {


        Move();
        Attacking();
        if (this.IsDead)
            Destroy(this);
        EnemyOffScreen();
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
            //Debug.Log("got here");
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
