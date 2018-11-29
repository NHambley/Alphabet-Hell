using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour {

    // attributes
    GameObject sceneManager;
    GameObject player;
    List<GameObject> checkList;

    public int livesMax;
    int lives;

	// Use this for initialization
	void Start () {
        sceneManager = GameObject.FindGameObjectWithTag("GameController");
        player = GameObject.FindGameObjectWithTag("Player");
        checkList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update () {
        if (sceneManager == null)
            sceneManager = GameObject.FindGameObjectWithTag("GameController");
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            checkList.Add(enemy);
        }

        foreach (GameObject enemyBullet in sceneManager.GetComponent<SceneManagerScript>().EnemyBullets)
        {
            checkList.Add(enemyBullet);
        }

        foreach(GameObject check in checkList)
        {
            if (CollisionCheck(check))
            {
                PlayerHit(check);
            }
        }

        checkList = new List<GameObject>();
    }

    // Check Player Collisions
    bool CollisionCheck(GameObject toCheck) { return sceneManager.GetComponent<SceneManagerScript>().CheckCollisions(player, toCheck); }

    // When the player gets hit by enemy, enemybullet, etc.
    void PlayerHit(GameObject hit)
    {
        if (hit.tag == "Enemy" || hit.tag == "EnemyBullet")
        {
            Debug.Log("HIT");
        }
    }
}
