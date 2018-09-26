using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour {
    List<GameObject> playerBullets = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();
    GameObject currentEnemy;
    GameObject[] backgrounds;
    Vector3[] parallaxSpeeds;
    public GameObject[] levels;
    bool levelInProgress = false;
    float timeBetweenEnemies = 0.0f;
    int numOfEnemiesLeft = 0;
    float lastEnemySpawnTime = 0.0f;
    //GameObject background;

    // Use this for initialization
    void Start () {
        GenerateLevel(0);
        lastEnemySpawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateParallax();
        if (levelInProgress && numOfEnemiesLeft > 0 && lastEnemySpawnTime + timeBetweenEnemies < Time.time)
        {
            lastEnemySpawnTime = Time.time;
            numOfEnemiesLeft--;
            GameObject newEnemy = Instantiate(currentEnemy);
            newEnemy.GetComponent<GenericEnemyScript>().velocity = new Vector3(0.0f, -0.05f, 0.0f);
            newEnemy.transform.position = new Vector3(Random.Range(-6.0f, 6.0f), 9.0f, 0f);
            enemies.Add(newEnemy);
        }
        for (int i = playerBullets.Count-1; i >= 0; i--)
        {
            if (!playerBullets[i].GetComponent<GenericBulletScript>().IsDead)
            {
                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    if (!enemies[j].GetComponent<GenericEnemyScript>().IsDead)
                    {
                        if ((playerBullets[i].transform.position - enemies[j].transform.position).magnitude < 3)
                        {
                            SpriteRenderer bulletSR = playerBullets[i].GetComponent<SpriteRenderer>();
                            Vector2 bulletMin = playerBullets[i].transform.position - bulletSR.bounds.extents;
                            Vector2 bulletMax = playerBullets[i].transform.position + bulletSR.bounds.extents;
                            SpriteRenderer enemySR = enemies[j].GetComponent<SpriteRenderer>();
                            Vector2 enemyMin = enemies[j].transform.position - enemySR.bounds.extents;
                            Vector2 enemyMax = enemies[j].transform.position + enemySR.bounds.extents;
                            if (bulletMin.x < enemyMax.x && bulletMin.y < enemyMax.y && bulletMax.x > enemyMin.x && bulletMax.y > enemyMin.y)
                            {
                                playerBullets[i].GetComponent<GenericBulletScript>().IsDead = true;
                                enemies[j].GetComponent<GenericEnemyScript>().Health = enemies[j].GetComponent<GenericEnemyScript>().Health - 20;
                                if (enemies[j].GetComponent<GenericEnemyScript>().IsDead)
                                {
                                    Debug.Log("Dead");
                                    GameObject e = enemies[j];
                                    enemies.RemoveAt(j);
                                    Destroy(e);
                                }
                            }
                        }
                    }
                    else
                    {
                        GameObject e = enemies[j];
                        enemies.RemoveAt(j);
                        Destroy(e);
                    }
                }
            }
            else
            {
                GameObject b = playerBullets[i];
                playerBullets.RemoveAt(i);
                Destroy(b);
            }
        }
	}

    public void AddPlayerBullet(GameObject b)
    {
        playerBullets.Add(b);
    }

    public void GenerateLevel(int level)
    {
        LevelScript levelScript = levels[level].GetComponent<LevelScript>();
        currentEnemy = levelScript.enemy;
        levelInProgress = true;
        timeBetweenEnemies = levelScript.enemySpawnTime;
        numOfEnemiesLeft = levelScript.numOfEnemies;
        backgrounds = new GameObject[levelScript.backgrounds.Length * 2];
        for (int i = 0; i < levelScript.backgrounds.Length * 2; i+=2)
        {
            backgrounds[i] = new GameObject("Parallax " + i);
            backgrounds[i].AddComponent<SpriteRenderer>();
            backgrounds[i].GetComponent<SpriteRenderer>().sprite = levelScript.backgrounds[i / 2];
            backgrounds[i].transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, 0, 0)).x, Camera.main.ScreenToWorldPoint(Vector3.zero).y + backgrounds[i].GetComponent<SpriteRenderer>().bounds.extents.y, 1);

            backgrounds[i + 1] = new GameObject("Parallax " + (i + 1));
            backgrounds[i + 1].AddComponent<SpriteRenderer>();
            backgrounds[i + 1].GetComponent<SpriteRenderer>().sprite = levelScript.backgrounds[i / 2];
            backgrounds[i + 1].transform.position = new Vector3(backgrounds[i].transform.position.x, backgrounds[i].transform.position.y + backgrounds[i].GetComponent<SpriteRenderer>().bounds.extents.y * 2, backgrounds[i].transform.position.z);
        }
        parallaxSpeeds = levelScript.backgroundSpeeds;
    }

    public void UpdateParallax()
    {
        for (int i = 0; i < backgrounds.Length; i+=2)
        {
            backgrounds[i].transform.position += parallaxSpeeds[i / 2];
            backgrounds[i + 1].transform.position += parallaxSpeeds[i / 2];
            
            if (backgrounds[i].transform.position.y + backgrounds[i].GetComponent<SpriteRenderer>().bounds.extents.y < Camera.main.ScreenToWorldPoint(Vector3.zero).y)
            {
                backgrounds[i].transform.position += new Vector3(0, backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.y*2, 0);
            }
            else if(backgrounds[i+1].transform.position.y + backgrounds[i+1].GetComponent<SpriteRenderer>().bounds.extents.y < Camera.main.ScreenToWorldPoint(Vector3.zero).y)
            {
                backgrounds[i + 1].transform.position += new Vector3(0, backgrounds[i + 1].GetComponent<SpriteRenderer>().bounds.size.y*2, 0);
            }
        }
    }
}
