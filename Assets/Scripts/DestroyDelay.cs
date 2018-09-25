using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Object.Destroy(gameObject, 3f);
	}
	

}
