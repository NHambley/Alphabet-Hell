using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Boss : GenericBossScript {

    Transform[] children;

    [SerializeField]
    GameObject bullet;
    public GameObject bossBullet;

    float bTimer = 1.5f; // to keep track of when to fire another bullet
    float timerTrack = 1.5f;
    float obstacleTimerTrack = 4.0f;
    float bObstacleTimer = 4.0f;
    Vector3 bossBulletPos;
    Vector3 bossBulletPos1;

    // Use this for initialization
    void Start()
    {
        bossBulletPos = transform.position;
        bossBulletPos.x = transform.position.x - 1;
        bossBulletPos1 = transform.position;
        bossBulletPos1.x = transform.position.x + 1;


    }

    // Update is called once per frame
    void Update()
    {
        Attacking();
        bossAttack();
    }

    // rotate each frame by a set amount

    void Attacking()
    {
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

    void bossAttack()
    {
        

        obstacleTimerTrack -= Time.deltaTime;
        if (obstacleTimerTrack <= 0)
        {
            // instantiate a new bullet
            Instantiate(bossBullet, bossBulletPos, Quaternion.identity);
            Instantiate(bossBullet, bossBulletPos1, Quaternion.identity);

            //Debug.Log("got here");
            obstacleTimerTrack = bObstacleTimer;
        }
    }

    public override void OnHit(Vector3 pos)
    {
        Health -= 2;
    }
}
