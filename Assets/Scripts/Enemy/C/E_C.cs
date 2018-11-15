using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_C : GenericEnemyScript
{

    // attributes
    Vector3 position, tPosition;
    float shotTimer, damageTaken;
    //List<GameObject> bullets;
    GameObject firingPosition;

    public float shotTimerMax;
    public GameObject bulletPrefab;
    public Vector3 speed, bulletSpeed;

    SceneManagerScript sceneManager;

    // Destructor

    // Use this for initialization
    void Start()
    {
        position = gameObject.transform.position;
        velocity = speed;
        firingPosition = transform.GetChild(0).gameObject;
        //bullets = new List<GameObject>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        sceneManager.AddEnemy(gameObject);
        damageTaken = 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        // Firing
        if (shotTimer == 0 || shotTimer >= shotTimerMax)
        {
            shotTimer = 0;
            Fire();
        }
        else
            shotTimer++;
    }

    // movement method
    void Move()
    {
        position += -velocity;
        transform.position = position;
    }

    // firing method
    void Fire()
    {
        GenerateBullet();
        shotTimer++;
    }

    // creating a bullet
    void GenerateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = firingPosition.transform.position;
        newBullet.GetComponent<CorkBullet>().InitializeBullet(bulletSpeed, Vector3.zero, gameObject);
    }

    public override void OnHit(Vector3 pos)
    {
        Health -= (int)damageTaken;
    }
}
