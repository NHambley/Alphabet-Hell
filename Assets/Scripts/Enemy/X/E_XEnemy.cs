using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_XEnemy:GenericEnemyScript
{

    public GameObject bulletPrefab;
    SceneManagerScript manager;
    float lastShootTime = -10.0f;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastShootTime > 3.0f && gameObject.GetComponent<Renderer>().isVisible)
        {
            Shoot();
            lastShootTime = Time.time;
        }
	}

    public void Shoot()
    {
        GameObject newBullet1 = Instantiate(bulletPrefab);
        newBullet1.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y - gameObject.GetComponent<SpriteRenderer>().bounds.extents.y, gameObject.transform.position.z);
        newBullet1.GetComponent<E_XBullet>().InitializeBullet(new Vector3(0f, -0.3f, 0f), new Vector3(0, 0, 0), gameObject);
        manager.AddEnemyBullet(newBullet1);

        GameObject newBullet2 = Instantiate(bulletPrefab);
        newBullet2.GetComponent<E_XBullet>().opposite = true;
        newBullet2.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.GetComponent<SpriteRenderer>().bounds.extents.y, gameObject.transform.position.z);
        newBullet2.GetComponent<E_XBullet>().InitializeBullet(new Vector3(0f, -0.3f, 0f), new Vector3(0, 0, 0), gameObject);
        manager.AddEnemyBullet(newBullet2);
    }

    public override void OnHit(Vector3 pos)
    {
        Health -= 20;
        gameObject.GetComponent<ParticleGenerator>().GenerateParticles(SPRITE.SPARK, 3, pos, new Vector3(0.0f, 0.1f, 0.0f), new Vector3(0.2f, 0.2f, 0.2f), 90, 0.5f, -0.5f);
    }
}
