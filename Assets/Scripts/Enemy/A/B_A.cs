using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_A : GenericBossScript
{
    Transform[] children;

    [SerializeField]
    GameObject[] firePoints;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float rotSpeed;//for rotating the object locally

    SceneManagerScript sceneManager;
    // Use this for initialization
    void Start ()
    {
        rotSpeed = 40f;
        Health = 500;
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        Rotate();
	}

    // rotate each frame by a set amount
    void Rotate()
    {
        transform.Rotate(new Vector3(0,0,1) * Time.deltaTime * rotSpeed);
    }

    // fire a bullet from the given position
    public void FireBullet(Transform position)
    {
        sceneManager.GetComponent<SceneManagerScript>().AddEnemyBullet(Instantiate(bullet, position));
    }

    public override void OnHit(Vector3 pos)
    {
        Health -= 15;
    }
}
