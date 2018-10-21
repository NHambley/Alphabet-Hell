using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_HBullet : MonoBehaviour
{
    // if a bullet is false it will slowly fade away and deal no damage to the player, this will be done by the enemy itself
    public bool real = true;
    float alpha = 1;

    float speed;
    Vector2 position;
    Vector2 movement;
    SceneManagerScript sM;

    GameObject player;

    Color bColor;
	// Use this for initialization
	void Start ()
    {
        speed = 7;
        position = transform.position;
        movement = Vector2.down;
        player = GameObject.FindGameObjectWithTag("Player");
        bColor = GetComponent<SpriteRenderer>().material.color;
        sM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // do something different depending on if the bullet is real or not
		if(real)
        {
            RealBullet();
        }
        if(!real)
        {
            FakeBullet();
        }
        transform.position = position;
	}

    // moves down the screen similar to the A_Bullet
    void RealBullet()
    {
        position += movement * speed * Time.deltaTime;

        // every frame check collision with the player
        if (sM.CheckCollisions(player, gameObject))
        {
            // deal damage to the player and then destroy the bullet
            player.GetComponent<S_Player>().Health -= 10;
        }
    }

    // move the bullet down the screen and slowly fade it out of existence until you destroy it
    void FakeBullet()
    {
        position += movement * speed * Time.deltaTime;
        bColor.a -= Time.deltaTime * 0.75f;

        // check if the alpha is less than or equal to zero then delete it
        if(bColor.a <= 0)
        {
            Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().material.color = bColor;
    }
}
