using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_B : GenericEnemyScript {
    Vector3 position, tPosition;
    float damageTaken;
    public Vector3 speed;
    SceneManagerScript sceneManager;
    public override void OnHit()
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
        position = gameObject.transform.position;
        velocity = speed;
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }
    void Move()
    {
        position += -velocity;
        transform.position = position;
    }
    // Update is called once per frame
    void FixedUpdate () {
        Move();
	}
}
