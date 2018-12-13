using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_HBullet : GenericBulletScript
{
    // if a bullet is false it will slowly fade away and deal no damage to the player, this will be done by the enemy itself
    bool real = true;
    float alpha = 1;

    public float speed;
    Vector2 position;
    Vector2 movement;
    SceneManagerScript sM;

    GameObject player;

    Color bColor;

    public bool Real
    {
        get { return real; }
        set { real = value; }
    }
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
            RealBullet();
        if(!real)
            FakeBullet();
        transform.position = position;
	}

    // moves down the screen similar to the A_Bullet
    void RealBullet()
    {
        position += movement * speed * Time.deltaTime;
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
