using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Bullet : MonoBehaviour {

    float speed;
    Vector2 position;
    
    float xRot;
    Vector2 movement;
    SceneManagerScript sM;
    int leftOrRight;
    int lengthOfArc;
    int lengthOfArc1;
    GameObject player;
    // Use this for initialization
    void Start()
    {
        speed = 700;
        position = transform.position;
        movement = Vector2.down;
        player = GameObject.FindGameObjectWithTag("Player");
        sM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
        xRot = 0;
        //Debug.Log(leftOrRight);
        

    }

    // Update is called once per frame
    void Update()
    {

        if (position.x < player.transform.position.x)
        {
            position.x+=.01f;
        }
        else if(position.x > player.transform.position.x)
        {
            position.x-=.01f;
        }
        position.y -= .02f;

        xRot = transform.position.x - player.transform.position.x;
        transform.eulerAngles = new Vector3(xRot, 0, 0);
        
        
        // every frame check collision with the player
        if (sM.CheckCollisions(player, gameObject))
        {
            // deal damage to the player and then destroy the bullet
            player.GetComponent<S_Player>().Health -= 10;
        }

        transform.position = position;
    }
}
