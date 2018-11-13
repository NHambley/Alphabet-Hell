using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_AFirePoint : MonoBehaviour
{
    /// <summary>
    /// this will check the angle between the firepoint and a straight vertical vector
    /// </summary>

    Vector2 fPointVec;
    Vector2 vertical;

    float bTimer;

    float timerTrack;
	// Use this for initialization
	void Start ()
    {
        fPointVec = transform.position;
        vertical = Vector2.up;
        bTimer = 0.5f;
        timerTrack = bTimer;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timerTrack -= Time.deltaTime;
		// get the angle between the two and if the angle is small enough (say 15 degrees or so at first) then instantiate a bullet
        if(Vector2.Angle(fPointVec, vertical) < 15 && timerTrack <= 0)
        {
            GetComponentInParent<B_A>().FireBullet(transform);
            timerTrack = bTimer;
        }
        fPointVec = transform.position;
	}
}
