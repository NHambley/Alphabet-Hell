using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Bullet : MonoBehaviour
{
    GameObject player;
    Vector2 targetVec;
    Vector2 bPosition;
    [SerializeField, Range(5,10)]
    float speed;
    SceneManagerScript sm;

	// Use this for initialization
	void Start ()
    {
        // find the player 
        player = GameObject.FindGameObjectWithTag("Player");
        bPosition = transform.position;
        targetVec = player.transform.position;
        targetVec += (bPosition + targetVec);
        sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
        speed = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 distance = targetVec - bPosition;
        bPosition += distance * speed * Time.deltaTime;
        transform.position = bPosition;

        // check collisions with the player
        if(sm.CheckCollisions(gameObject, player))
        {
            player.GetComponent<S_Player>().Health -= 5;
            Destroy(gameObject);
        }

        
	}
}
