using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour {

    #region Attributes
    // general
    GameObject sceneManager;
    GameObject player;
    List<GameObject> checkList; // collision checking list

    // lives
    List<GameObject> livesList;
    int livesMax;
    public int lives;

    // hitTimer
    public float hitTimerMax;
    float hitTimer;

    // Game Over
    public GameObject ggPref;
    GameObject gg;
    float ggTimer;
    float ggTimerMax;

    #endregion

    // Use this for initialization
    void Start () {
        sceneManager = GameObject.FindGameObjectWithTag("GameController");
        player = GameObject.FindGameObjectWithTag("Player");
        checkList = new List<GameObject>();
        livesMax = 3;
        ggTimerMax = 120;

        if (lives > livesMax)
            lives = livesMax;

        GenerateLifeSprites();
    }

    // Update is called once per frame
    void Update()
    {
        // makes sure the important things are set
        if (sceneManager == null)
            sceneManager = GameObject.FindGameObjectWithTag("GameController");
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        #region CollisionCheck
        if (lives != 0)
        {
            if (hitTimer == 0)
            {
                // adds everything with tag enemy
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    checkList.Add(enemy);
                }

                /*
                // adds the enemy bullets from the scene manager
                foreach (GameObject enemyBullet in sceneManager.GetComponent<SceneManagerScript>().EnemyBullets)
                {
                    checkList.Add(enemyBullet);
                }
                */

                // checks everything in the list
                foreach (GameObject check in checkList)
                {
                    if (CollisionCheck(check))
                    {
                        PlayerHit(check);
                    }
                }

                // resets the list
                checkList = new List<GameObject>();
            }

            #region HitTimer
            else
            {
                hitTimer--;
                if (hitTimer == 0)
                    player.GetComponent<SpriteRenderer>().enabled = true;
                else if (hitTimer % 2 == 1)
                    player.GetComponent<SpriteRenderer>().enabled = false;
                else if (hitTimer % 2 == 0)
                    player.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        #endregion

        #endregion

        if (lives < livesList.Count)
        {
            GameObject popped = livesList[lives];
            livesList.RemoveAt(lives);
            Destroy(popped);
        }

        if (lives == 0 && player != null)
        {
            Destroy(player);
            gg = Instantiate(ggPref);
            ggTimer = ggTimerMax;
            gg.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }

        if (player == null)
        {
            if (ggTimer != 0)
            {
                ggTimer--;
                Debug.Log((ggTimerMax - ggTimer) / ggTimerMax);
                if (ggTimer == 0)
                    gg.GetComponent<SpriteRenderer>().color = Color.white;
                else
                    gg.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (ggTimerMax - ggTimer) / ggTimerMax);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mp.z = 0;
                if (gg.GetComponent<Collider2D>().bounds.Contains(mp))
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }
    // Check Player Collisions
    bool CollisionCheck(GameObject toCheck) { return sceneManager.GetComponent<SceneManagerScript>().CheckCollisions(player, toCheck); }

    // When the player gets hit by enemy, enemybullet, etc.
    public void PlayerHit(GameObject hit)
    {
        // if hit by an enemy or enemybullet
        if (hit.tag == "Enemy" || hit.tag == "EnemyBullet")
        {
            hitTimer = hitTimerMax;
            //player.GetComponent<S_Player>().SetToSpawn();
            lives--;
        }
    }

    // Creates the Life Sprites
    void GenerateLifeSprites()
    {
        livesList = new List<GameObject>();
        Sprite pSprite = player.GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < lives; i++)
        {
            GameObject life = new GameObject("Life" + (i+1));
            life.AddComponent<SpriteRenderer>().sprite = pSprite;
            life.GetComponent<SpriteRenderer>().sortingOrder = 1;
            life.transform.localScale = new Vector3(.1f, .1f, 1f);
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight));
            pos.x += .5f + (i * life.GetComponent<SpriteRenderer>().bounds.extents.x * 2.5f);
            pos.y -= life.GetComponent<SpriteRenderer>().bounds.extents.y * 1.4f;
            pos.z = 0;
            life.transform.position = pos;
            livesList.Add(life);
        }
    }

    // returns true if the player can be hit
    public bool PlayerCanBeHit() { return hitTimer == 0; }
}
