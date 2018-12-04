using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
	// Use this for initialization
	void Awake ()
    {
        // make sure the screen doesn't rotate
        Screen.orientation = ScreenOrientation.Portrait;
	}
}
