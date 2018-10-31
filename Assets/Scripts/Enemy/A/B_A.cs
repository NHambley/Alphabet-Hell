using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_A : MonoBehaviour
{
    Transform[] children;

    [SerializeField]
    GameObject[] firePoints;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float rotSpeed;//for rotating the object locally

	// Use this for initialization
	void Start ()
    {
        /*
        children = GetComponentsInChildren<Transform>();
        firePoints = new GameObject[children.Length - 1];// get the firepoints but not the actual boss

        for (int i = 1; i < children.Length; i++)
        {
            firePoints[i - 1] = children[i].gameObject;
        }
        */
        rotSpeed = 40f;
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
        Instantiate(bullet, position);
    }
}
