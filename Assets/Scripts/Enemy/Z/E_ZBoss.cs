using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ZBoss : GenericBossScript
{

    public GameObject bulletPrefab;
    SceneManagerScript manager;
    float lastShootTime = -10.0f;
    bool needsNewVel = true;

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
            if (Time.time - lastShootTime > 3.0f && gameObject.GetComponent<Renderer>().isVisible)
            {
                Shoot();
                lastShootTime = Time.time;
            }
            if (needsNewVel)
            {
                velocity = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.05f, 0.05f), 0.0f);
                needsNewVel = false;
            }
            Vector3 cameraMin = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
            Vector3 cameraMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0.0f));
            Debug.DrawLine(cameraMin, cameraMax, Color.black);
            if (gameObject.transform.position.x + gameObject.GetComponent<SpriteRenderer>().bounds.extents.x > cameraMax.x)
            {
                needsNewVel = true;
                Vector3 tempPos = gameObject.transform.position;
                tempPos.x = cameraMax.x - gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
                gameObject.transform.position = tempPos;
            }
            if (gameObject.transform.position.x - gameObject.GetComponent<SpriteRenderer>().bounds.extents.x < cameraMin.x)
            {
                needsNewVel = true;
                Vector3 tempPos = gameObject.transform.position;
                tempPos.x = cameraMin.x + gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
                gameObject.transform.position = tempPos;
            }
            if (gameObject.transform.position.y + gameObject.GetComponent<SpriteRenderer>().bounds.extents.y > cameraMax.y)
            {
                needsNewVel = true;
                Vector3 tempPos = gameObject.transform.position;
                tempPos.y = cameraMax.y - gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
                gameObject.transform.position = tempPos;
            }
            if (gameObject.transform.position.y - gameObject.GetComponent<SpriteRenderer>().bounds.extents.y < cameraMin.y)
            {
                needsNewVel = true;
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
        newBullet1.GetComponent<E_ZBullet>().InitializeBullet(new Vector3(0f, -0.1f, 0f), new Vector3(0, 0, 0), gameObject);
        manager.AddEnemyBullet(newBullet1);
        manager.gameObject.GetComponent<AudioManager>().PlaySound("zshoot");
    }

    public override void OnHit(Vector3 pos)
    {
        gameObject.GetComponent<ParticleGenerator>().GenerateParticles(SPRITE.WATER, 3, pos, new Vector3(0.0f, 0.1f, 0.0f), new Vector3(0.05f, 0.05f, 0.05f), 90, 0.5f, -0.5f);
        manager.gameObject.GetComponent<AudioManager>().PlaySound("pop2");
    }
}
