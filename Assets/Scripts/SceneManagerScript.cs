using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour {
    List<GameObject> playerBullets = new List<GameObject>();
    List<GameObject> enemyBullets = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();
    GameObject boss;
    public GameObject player;
    GameObject currentEnemyPrefab;
    GameObject currentBossPrefab;
    GameObject[] backgrounds;
    Vector3[] parallaxSpeeds;
    public GameObject[] levels;
    bool levelInProgress = false;
    float timeBetweenEnemies = 0.0f;
    int numOfEnemiesLeft = 0;
    float lastEnemySpawnTime = 0.0f;
    //GameObject background;
    static public int level = -1;
    public int levelDebug = -1;
    bool isBossFight = false;
    PlayerLives playerLives;

    public List<GameObject> EnemyBullets
    {
        get { return enemyBullets; }
    }

    // Use this for initialization
    void Start () {
        if (level == -1)
        {
            if (levelDebug > -1)
                level = levelDebug;
            else
                level = 0;
        }
        GenerateLevel(level);
        lastEnemySpawnTime = Time.time;
        playerLives = GameObject.FindObjectOfType<PlayerLives>().GetComponent<PlayerLives>();
        gameObject.GetComponent<AudioManager>().PlaySound("Music");
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateParallax();
        UpdateLevel();
        UpdateEnemyCheck();
        UpdatePlayerBullets();
        UpdateEnemyBullets();
	}

    public void UpdateLevel()
    {
        if (levelInProgress && numOfEnemiesLeft > 0 && lastEnemySpawnTime + timeBetweenEnemies < Time.time)
        {
            lastEnemySpawnTime = Time.time;
            numOfEnemiesLeft--;
            GameObject newEnemy = Instantiate(currentEnemyPrefab);
            newEnemy.GetComponent<GenericEnemyScript>().velocity = new Vector3(0.0f, -0.05f, 0.0f);
            newEnemy.transform.position = new Vector3(Random.Range(-3.0f, 3.0f), 9.0f, 0f);
            enemies.Add(newEnemy);
            isBossFight = false;
        }

        // Boss has spawned, moving to correct position
        if (numOfEnemiesLeft <= 0 /*&& enemies.Count <= 0*/ && !isBossFight)
        {
            isBossFight = true;
            boss = Instantiate(currentBossPrefab);
            boss.GetComponent<GenericBossScript>().isActive = false;
            boss.transform.position = new Vector3(0.0f, 9.0f, 0.0f);
            boss.GetComponent<GenericBossScript>().velocity = new Vector3(0.0f, -0.04f, 0.0f);
        }

        if (isBossFight && boss != null)
        {
            // should the boss start shooting / doing its thing?
            GenericBossScript bossScript = boss.GetComponent<GenericBossScript>();
            if (bossScript.isActive) return;
            else if (boss.transform.position.y <= Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Camera.main.pixelHeight, 0.0f)).y / 1.5f)
            {
                bossScript.isActive = true;
                bossScript.acceleration = Vector3.zero;
                bossScript.velocity = Vector3.zero;
            }
        }
    }

    public void UpdateEnemyCheck()
    {
        int max = enemies.Count;
        for (int i = max - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }

    public void UpdatePlayerBullets()
    {
        for (int i = playerBullets.Count - 1; i >= 0; i--)
        {
            if (!playerBullets[i].GetComponent<GenericBulletScript>().IsDead)
            {
                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    if (enemies[j] != null)
                    {
                        if (enemies[j].GetComponent<GenericEnemyScript>().IsDead)
                        {
                            GameObject e = enemies[j];
                            enemies.RemoveAt(j);
                            Destroy(e);
                        }
                        else
                        {
                            if ((playerBullets[i].transform.position - enemies[j].transform.position).magnitude < 3)
                            {
                                if (CheckCollisions(playerBullets[i], enemies[j]))
                                {
                                    playerBullets[i].GetComponent<GenericBulletScript>().IsDead = true;
                                    enemies[j].GetComponent<GenericEnemyScript>().OnHit(playerBullets[i].transform.position);
                                }
                            }
                        }
                    }
                }
                if (boss != null)
                {
                    if ((playerBullets[i].transform.position - boss.transform.position).magnitude < 3 && !boss.GetComponent<GenericBossScript>().IsDead)
                    {
                        if (CheckCollisions(playerBullets[i], boss))
                        {
                            playerBullets[i].GetComponent<GenericBulletScript>().IsDead = true;
                            boss.GetComponent<GenericBossScript>().OnHit(playerBullets[i].transform.position);
                            if (boss.GetComponent<GenericBossScript>().IsDead)
                            {
                                Destroy(boss);
                                playerLives.bossIsDead = true;
                            }
                        }
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

    public void UpdateEnemyBullets()
    {
        for (int i = enemyBullets.Count - 1; i >= 0; i--)
        {
            if (!enemyBullets[i].GetComponent<GenericBulletScript>().IsDead)
            {
                if (CheckCollisions(player, enemyBullets[i]) && playerLives.PlayerCanBeHit())
                {
                    playerLives.PlayerHit(enemyBullets[i]);
                    enemyBullets[i].GetComponent<GenericBulletScript>().IsDead = true;
                    player.GetComponent<ParticleGenerator>().GenerateParticles(SPRITE.SPARK, 5, enemyBullets[i].transform.position, enemyBullets[i].GetComponent<GenericBulletScript>().GetVelocity().normalized * 0.3f, new Vector3(1.0f, 1.0f, 1.0f), 90, 0.5f, -0.5f);
                    gameObject.GetComponent<AudioManager>().PlaySound("ting");
                    //player.GetComponent<S_Player>().health -= enemyBullets[i].GetComponent<GenericBulletScript>().damage;
                    if (enemyBullets[i].GetComponent<GenericBulletScript>().IsDead)
                    {
                        GameObject b = enemyBullets[i];
                        enemyBullets.RemoveAt(i);
                        Destroy(b);
                    }
                }
            }
            else
            {
                GameObject b = enemyBullets[i];
                enemyBullets.RemoveAt(i);
                Destroy(b);
            }
        }
    }

    public void AddPlayerBullet(GameObject b)
    {
        playerBullets.Add(b);
    }

    public void AddEnemyBullet(GameObject b)
    {
        b.tag = "EnemyBullet";
        enemyBullets.Add(b);
    }

    public void GenerateLevel(int level)
    {
        LevelScript levelScript = levels[level].GetComponent<LevelScript>();
        currentEnemyPrefab = levelScript.enemy;
        currentBossPrefab = levelScript.boss;
        levelInProgress = true;
        timeBetweenEnemies = levelScript.enemySpawnTime;
        numOfEnemiesLeft = levelScript.numOfEnemies;
        backgrounds = new GameObject[levelScript.backgrounds.Length * 2];
        for (int i = 0; i < levelScript.backgrounds.Length * 2; i+=2)
        {
            backgrounds[i] = new GameObject("Parallax " + i);
            backgrounds[i].AddComponent<SpriteRenderer>();
            backgrounds[i].GetComponent<SpriteRenderer>().sprite = levelScript.backgrounds[i / 2].sprite;
            backgrounds[i].transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, 0, 0)).x, Camera.main.ScreenToWorldPoint(Vector3.zero).y + backgrounds[i].GetComponent<SpriteRenderer>().bounds.extents.y, levelScript.backgrounds[i/2].zIndex);
            backgrounds[i].transform.localScale = levelScript.backgrounds[i].scale;

            backgrounds[i + 1] = new GameObject("Parallax " + (i + 1));
            backgrounds[i + 1].AddComponent<SpriteRenderer>();
            backgrounds[i + 1].GetComponent<SpriteRenderer>().sprite = levelScript.backgrounds[i / 2].sprite;
            backgrounds[i + 1].transform.position = new Vector3(backgrounds[i].transform.position.x, backgrounds[i].transform.position.y + backgrounds[i].GetComponent<SpriteRenderer>().bounds.extents.y * 2, backgrounds[i].transform.position.z);
            backgrounds[i + 1].transform.localScale = levelScript.backgrounds[i].scale;
        }
        parallaxSpeeds = new Vector3[levelScript.backgrounds.Length];
        for (int i = 0; i < parallaxSpeeds.Length; i++)
        {
            parallaxSpeeds[i] = levelScript.backgrounds[i].speed;
        }

        ScaleBackground();// make sure the background fits the phone screen
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

    // adjust the background sprites to the background of the device that is being played 
    void ScaleBackground()
    {
        foreach (GameObject bg in backgrounds)
        {
            // access the sprite renderer
            SpriteRenderer sr = bg.GetComponent<SpriteRenderer>();
            if (sr == null)
                return;
            // using the width and height of the sprite and the width and height of the camera calculate the scale the backgruond needs to be set to and scale it
            float width = sr.sprite.bounds.size.x;
            float height = sr.sprite.bounds.size.y;

            double worldScreenHeight = Camera.main.orthographicSize * 2.0;
            double worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            bg.transform.localScale = new Vector3((float)worldScreenWidth / width, (float)worldScreenHeight / height, 1);
        }
    }

    public bool CheckCollisions(GameObject a, GameObject b)
    {
        if (a == null || b == null)
            return false;

        Vector2 aMin;
        Vector2 aMax;
        Vector2 bMin;
        Vector2 bMax;

        // A object checks
        // if the gameobject doesn't have a collider
        if (a.GetComponent<Collider2D>() == null)
        {
            SpriteRenderer aSpriteRenderer = a.GetComponent<SpriteRenderer>();
            aMin = a.transform.position - aSpriteRenderer.bounds.extents;
            aMax = a.transform.position + aSpriteRenderer.bounds.extents;
        }
        // if it does
        else
        {
            aMin = a.transform.position - a.GetComponent<Collider2D>().bounds.extents;
            aMax = a.transform.position + a.GetComponent<Collider2D>().bounds.extents;
        }

        // B object checks
        // if the gameobject doesn't have a collider
        if (b.GetComponent<Collider2D>() == null)
        {
            SpriteRenderer bSpriteRenderer = b.GetComponent<SpriteRenderer>();
            bMin = b.transform.position - bSpriteRenderer.bounds.extents;
            bMax = b.transform.position + bSpriteRenderer.bounds.extents;
        }
        // if it does
        else
        {
            bMin = b.transform.position - b.GetComponent<Collider2D>().bounds.extents;
            bMax = b.transform.position + b.GetComponent<Collider2D>().bounds.extents;
        }
        //AABB check
        if (aMin.x < bMax.x && aMin.y < bMax.y && aMax.x > bMin.x && aMax.y > bMin.y)
        {
            return true;
        }
        return false;
    }

    public void AddEnemy(GameObject newEnemy)
    {
        enemies.Add(newEnemy);
    }
}