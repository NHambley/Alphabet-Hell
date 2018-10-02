using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_C : GenericEnemyScript {

    // attributes
    Vector3 position, tPosition, bulletVelocity;
    float shotTimer;
    List<GameObject> bullets;
    GameObject firingPosition;

    public float shotTimerMax;
    public GameObject bulletPrefab;
    public Vector3 speed, bulletSpeed;

	// Use this for initialization
	void Start ()
    {
        position = gameObject.transform.position;
        velocity = speed;
        bulletVelocity = bulletSpeed;
        firingPosition = transform.GetChild(0).gameObject;
        bullets = new List<GameObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
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

        // Bullet checking
        BulletUpdate();
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
        //Debug.Log("Firing Cork");
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = firingPosition.transform.position;
        bullets.Add(newBullet);
    }

    // Updating bullet positions
    void BulletUpdate()
    {
        foreach(GameObject bullet in bullets)
        {
            Vector3 newPosition = bullet.transform.position;
            newPosition += -bulletVelocity;
            bullet.transform.position = newPosition;
        }

        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].transform.position.y < -15 || bullets[i].transform.position.y > 15)
            {
                GameObject toDelete = bullets[i];
                bullets.Remove(bullets[i]);
                Destroy(toDelete);
            }
        }
    }
}
