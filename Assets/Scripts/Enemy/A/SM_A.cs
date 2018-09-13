using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_A : MonoBehaviour
{
    /// <summary>
    /// this is the state manager for the A enemy 
    /// A has two states
    ///    - attacking: this state has the enemey charging down the screen at the player shooting bullets at rapid fire
    ///    - reset: this state triggers when the enemy has gone off screen and needs to reset back to the top of the screen, once it get back to the top it begins attacking again
    /// </summary>
    
    // since there are only two states a simple boolean variable will represent which state it is in
    // true: attacking 
    // false: resetting

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
