using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour {
    List<GameObject> playerBullets = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    // Use this for initialization
    void Start () {
        enemies.Add(GameObject.Find("EnemyTest"));
	}
	
	// Update is called once per frame
	void Update ()
    {
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
}
