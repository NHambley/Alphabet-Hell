using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_JBoss : GenericBossScript
{

    public GameObject bulletPrefab;
    SceneManagerScript manager;
    float lastShootTime = -10.0f;
    bool startVel = true;

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
            if (startVel)
            {
                velocity = new Vector3(0.1f, 0.1f, 0.0f);
                startVel = false;
            }
                
            if (Time.time - lastShootTime > 3.0f && gameObject.GetComponent<Renderer>().isVisible)
            {
                Shoot();
                lastShootTime = Time.time;
            }
            Vector3 cameraMin = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
            Vector3 cameraMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0.0f));
            Debug.DrawLine(cameraMin, cameraMax, Color.black);
            if (gameObject.transform.position.x + gameObject.GetComponent<SpriteRenderer>().bounds.extents.x > cameraMax.x)
            {
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
                Vector3 tempPos = gameObject.transform.position;
                tempPos.x = cameraMax.x - gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
                gameObject.transform.position = tempPos;
            }
            if (gameObject.transform.position.x - gameObject.GetComponent<SpriteRenderer>().bounds.extents.x < cameraMin.x)
            {
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
                Vector3 tempPos = gameObject.transform.position;
                tempPos.x = cameraMin.x + gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
                gameObject.transform.position = tempPos;
            }
            if (gameObject.transform.position.y + gameObject.GetComponent<SpriteRenderer>().bounds.extents.y > cameraMax.y)
            {
                velocity = new Vector3(velocity.x, -velocity.y, velocity.z);
                Vector3 tempPos = gameObject.transform.position;
                tempPos.y = cameraMax.y - gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
                gameObject.transform.position = tempPos;
            }
            if (gameObject.transform.position.y - gameObject.GetComponent<SpriteRenderer>().bounds.extents.y < cameraMin.y)
            {
                velocity = new Vector3(velocity.x, -velocity.y, velocity.z);
                Vector3 tempPos = gameObject.transform.position;
                tempPos.y = cameraMin.y + gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
                gameObject.transform.position = tempPos;
            }
        }
    }

    public void Shoot()
    {
        GameObject newBullet1 = Instantiate(bulletPrefab);
        newBullet1.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.GetComponent<SpriteRenderer>().bounds.extents.y, gameObject.transform.position.z - 1);
        newBullet1.GetComponent<E_JBullet>().InitializeBullet(new Vector3(0f, -0.1f, 0f), new Vector3(0, 0, 0), gameObject);
        manager.AddEnemyBullet(newBullet1);
    }

    public override void OnHit(Vector3 pos)
    {
        gameObject.GetComponent<ParticleGenerator>().GenerateParticles(SPRITE.WATER, 3, pos, new Vector3(0.0f, 0.1f, 0.0f), new Vector3(0.05f, 0.05f, 0.05f), 90, 0.5f, -0.5f);
    }
}
