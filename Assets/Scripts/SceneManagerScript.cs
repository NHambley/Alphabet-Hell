﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour {
    List<GameObject> playerBullets = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddPlayerBullet(GameObject b)
    {
        playerBullets.Add(b);
    }
}