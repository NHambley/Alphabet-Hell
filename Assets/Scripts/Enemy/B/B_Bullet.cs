using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Bullet : MonoBehaviour {

    float speed;
    Vector2 position;
    Vector2 movement;
    SceneManagerScript sM;
    int leftOrRight;
    int lengthOfArc;
    GameObject player;
    // Use this for initialization
    void Start()
    {
        speed = 7;
        position = transform.position;
        movement = Vector2.down;
        player = GameObject.FindGameObjectWithTag("Player");
        sM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
        leftOrRight = Random.Range(1, 2);
        lengthOfArc = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.zero;

        //left side arc of bullet
        if (leftOrRight == 1)
        {
            lengthOfArc++;
            direction = Vector2.left;
            //after two seconds, the bullet arcs the other way
            if (lengthOfArc > 120)
                leftOrRight = 2;
        }
        //left side arc of bullet
        else if (leftOrRight == 2)
        {
            direction = Vector2.right;
        }
        //shoots a bullet on an "arc" like how a boomerang goes
        position += movement * speed * Time.deltaTime * direction;

        // every frame check collision with the player
        if (sM.CheckCollisions(player, gameObject))
        {
            // deal damage to the player and then destroy the bullet
            player.GetComponent<S_Player>().Health -= 10;
        }
        transform.position = position;
    }
}
