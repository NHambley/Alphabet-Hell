using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_Bullet : MonoBehaviour {

    float speed;
    Vector2 position;
    Vector2 movement;
    SceneManagerScript sM;
    int leftOrRight;
    int lengthOfArc;
    int lengthOfArc1;
    GameObject player;
    public V_Enemy vScript;
    public int dirOfBullet;
    // Use this for initialization
    void Start()
    {
        speed = 700;
        position = transform.position;
        movement = Vector2.down;
        player = GameObject.FindGameObjectWithTag("Player");
        sM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();

        //Debug.Log(leftOrRight);
       
    }

    // Update is called once per frame
    void Update()
    {

        
        if (dirOfBullet == 1)
        { 
            position.x -= .03f;
        }
        if (dirOfBullet == 2)
        {
            position.x += .03f;
        }
        position.y -= .03f;
        // every frame check collision with the player
        if (sM.CheckCollisions(player, gameObject))
        {
            // deal damage to the player and then destroy the bullet
            player.GetComponent<S_Player>().Health -= 10;
        }

        transform.position = position;
    }
}
