using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_BossBullet : MonoBehaviour {

    // Use this for initialization
    Vector3 position;
    GameObject player;
    SceneManagerScript sM;
    int r;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
        r = 0;
        position = transform.position;

        transform.Rotate(Vector3.forward * Random.Range(-15.0f, 15.0f));
        r = Random.Range(0, 2);
        if (r == 0)
            transform.localRotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        else
            transform.localRotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);

    }

    // Update is called once per frame
    void Update()
    {
        position.y -= .01f;

        if (sM.CheckCollisions(player, gameObject))
        {
            // deal damage to the player and then destroy the bullet
            player.GetComponent<S_Player>().Health -= 10;
        }
        transform.position = position;

        EnemyOffScreen();
    }
    void EnemyOffScreen()
    {
        if (this.position.y > Screen.height - 1)
        {
            Destroy(this);
        }
    }
}
