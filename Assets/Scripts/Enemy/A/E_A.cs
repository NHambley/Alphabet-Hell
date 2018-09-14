using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_A : MonoBehaviour
{
    /// <summary>
    /// The A enemy will act similar to a Galaga enemy, moving down towards the player while shooting at them. 
    /// Assuming the player dodges the bullets and the enemy the enemy will loop back around off screen to the top and repeat the process again
    /// </summary>

    GameObject target;

    Vector2 speed;
    float topScreen; // an y position slightly above the top of the camera so the enemy can know where to head back to

    // get a reference to the state manager
    SM_A stateManager;

	// Use this for initialization
	void Start ()
    {
        stateManager = GetComponent<SM_A>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
