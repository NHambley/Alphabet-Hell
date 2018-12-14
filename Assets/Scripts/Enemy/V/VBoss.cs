using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBoss : GenericBossScript {
    Vector3 position, tPosition;
    Vector2 ePosition; // positino of this gameobject
    float damageTaken;
    public Vector2 speed;
    SceneManagerScript sceneManager;

    float bTimer = 1.5f; // to keep track of when to fire another bullet
    float timerTrack = 1.5f;




    // get a reference to the state manager
    [SerializeField]
    bool state;

    [SerializeField]
    GameObject bullet;

    Camera cam;
    public override void OnHit(Vector3 hit)
    {
        Health -= 2;
    }

    // Use this for initialization
    void Start()
    {
        position = gameObject.transform.position;
        velocity = speed;
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        Health = 100;

    }

    // Update is called once per frame
    void Update()
    {
        Attacking();
    }

    void Attacking()
    {
        // using the position that is set in the resetting method (or if it is the first attacking run of this enemy the one set in Start)have the enemy move down in some form towards that position 

        // subtract time from the timer to fire bullets
        timerTrack -= Time.deltaTime;
        if (timerTrack <= 0)
        {

            // instantiate a new bullet
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<VBossBullet>().dirOfBullet = 1;

            GameObject newBullet1 = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet1.GetComponent<VBossBullet>().dirOfBullet = 2;

            GameObject newBullet2 = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<VBossBullet>().dirOfBullet = 3;
            


            sceneManager.AddEnemyBullet(newBullet);
            sceneManager.AddEnemyBullet(newBullet1);
            sceneManager.AddEnemyBullet(newBullet2);
            //Debug.Log("got here");
            timerTrack = bTimer;
        }



    }
}
