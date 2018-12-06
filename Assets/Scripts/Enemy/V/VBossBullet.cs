using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBossBullet : GenericBulletScript {

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
        if (dirOfBullet == 1)
            transform.Rotate(Vector3.forward * -65);
        if (dirOfBullet == 2)
            transform.Rotate(Vector3.forward * -25);
        if (dirOfBullet == 3)
            transform.Rotate(Vector3.forward * -85);
        if (dirOfBullet == 4)
            transform.Rotate(Vector3.forward * -5);
        //Debug.Log(leftOrRight);

    }

    // Update is called once per frame
    void Update()
    {
        if (dirOfBullet == 1)
        { 
            position.x -= .01f;
        }
        if (dirOfBullet == 3)
        {
            position.x -= .02f;
        }
        if (dirOfBullet == 2 )
        {
            position.x += .01f;
        }
        if (dirOfBullet == 4)
        {
            position.x += .02f;
        }

        position.y -= .03f;

        transform.position = position;
    }
}
