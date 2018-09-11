using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyScript : MonoBehaviour {

    private bool isDead = false;

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }


    private int health = 100;

    public int Health
    {
        get { return health; }
        set {
            health = value;
            if(health <= 0)
            {
                health = 0;
                isDead = true;
            }
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
