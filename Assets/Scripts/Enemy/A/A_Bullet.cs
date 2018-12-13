using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Bullet : MonoBehaviour
{
    float speed;
    Vector2 position;
    Vector2 movement;
    SceneManagerScript sM;

    GameObject player;
	// Use this for initialization
	void Start ()
    {
        speed = 7;
        position = transform.position;
        movement = Vector2.down;
        player = GameObject.FindGameObjectWithTag("Player");
        sM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        position += movement * speed * Time.deltaTime;
        transform.position = position;

        transform.rotation = Quaternion.Euler(0, 0, 0);
	}
}
