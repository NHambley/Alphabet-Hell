using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_T : GenericBossScript
{
    public override void OnHit(Vector3 pos)
    {
        Health -= 10;
    }

    // Use this for initialization
    void Start ()
    {
        Health = 250;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
