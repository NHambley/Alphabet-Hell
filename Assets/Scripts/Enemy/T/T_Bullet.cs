using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Bullet : GenericBulletScript
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
        if (player != null)
            targetVec = player.transform.position;
        targetVec += (bPosition + targetVec);
        sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
        speed = 2;
        
        Vector3 dir = player.transform.position - transform.position;
        dir = gameObject.transform.InverseTransformDirection(dir);
        float angleBetween = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.Rotate(new Vector3(0, 0, 1) * angleBetween + new Vector3(0,0,90));
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 distance = targetVec - bPosition;
        bPosition += distance * speed * Time.deltaTime;
        transform.position = bPosition;
    }
}
