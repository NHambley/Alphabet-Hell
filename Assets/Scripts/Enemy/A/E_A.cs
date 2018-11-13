using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_A : GenericEnemyScript
{
    /// <summary>
    /// The A enemy will act similar to a Galaga enemy, moving down towards the player while shooting at them. 
    /// Assuming the player dodges the bullets and the enemy the enemy will loop back around off screen to the top and repeat the process again
    /// </summary>

    GameObject target;
    Vector2 tPosition; // use this for calculating the path of each attack run ***this one is set after each reset
    Vector2 ePosition; // positino of this gameobject
    Vector2 topCenterScreen; // an y position slightly above the top of the camera so the enemy can know where to head back to
    Vector2 topSideScreen;// used to get the enemy resetting off screen
    bool reset = true; // true is heading to the side point, false is heading to the center, top of the screen

    float bTimer = 0.5f; // to keep track of when to fire another bullet
    float timerTrack = 0.5f;

    [Range(2,10), SerializeField]
    float speed;

    // get a reference to the state manager
    [SerializeField]
    bool state;

    [SerializeField]
    GameObject bullet;

    float hp;

    Camera cam;
	// Use this for initialization
	void Start ()
    {
        Health = 100;

        //stateManager = GetComponent<SM_A>();
        state = true;
        target = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;

        // set to the player's current position for now but each will change throughout the code
        tPosition = new Vector2(Random.Range(-(2f * cam.orthographicSize) * cam.aspect, (2f * cam.orthographicSize) * cam.aspect), target.transform.position.y);
        ePosition = new Vector2(transform.position.x, transform.position.y);

        topCenterScreen = new Vector2(0, 10);
        topSideScreen = new Vector2(5, 10);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Health <= 0)
            Destroy(gameObject);

        // check what state the enemy is in and call the subsequent method
        // if the bool is true call attacking methods
        // if the bool is false call reset positioning methods
        if (state)
            Attacking();
        else
            Resetting();

        
        transform.position = ePosition;
    }

    // the attacking method will do a few key things
    // - rotate the enemy to look at the player
    // - take the player's position at the beginning of the attacking state. 
    // - while this is happening have the enemy also be shooting at the player
    void Attacking()
    {
        // using the position that is set in the resetting method (or if it is the first attacking run of this enemy the one set in Start)have the enemy move down in some form towards that position 

        // subtract time from the timer to fire bullets
        timerTrack -= Time.deltaTime;
        if(timerTrack <= 0)
        {
            // instantiate a new bullet
            Instantiate(bullet, transform.position, Quaternion.identity);

            timerTrack = bTimer;
        }

        // move the enemy towards its desired point on the screen
        Vector2 distance = tPosition - ePosition;
        distance.Normalize();
        ePosition += distance * Time.deltaTime * speed;

        // check if the enemy has reached near the same y value as the player
        // if they are at or around that value set them into reset 
        if (ePosition.y - tPosition.y < 0.5)
        {
            state = false;
            reset = true;
            // check the enemy's x value to determine which side of the screen it should head off
            if(ePosition.x <= 0)
            {
                topSideScreen = new Vector2(-5, 10);
            }
            else
            {
                topSideScreen = new Vector2(5, 10);
            }
        }

    }

    // whenever the enemy reaches a certain point on the screen it will stop attacking and return to the top of the screen
    // before it sets back to attacking it will take the current position the player is in and map out a 
    void Resetting()
    {
        Vector2 distance = Vector2.zero; 
        // if the enemy has reached the top of the screen give it a new attack vector
        if(Vector2.Distance(transform.position, topCenterScreen) < .5)
        {
            tPosition = new Vector2(Random.Range(-(2f * cam.orthographicSize) * cam.aspect, (2f * cam.orthographicSize) * cam.aspect), target.transform.position.y);
            state = true;
        }
        
        if(reset) // if the enemy is heading towards the side scrren still
        {
            distance = topSideScreen - ePosition;
            distance.Normalize();
            ePosition += distance * Time.deltaTime * speed;
            if (Vector2.Distance(ePosition, topSideScreen) < .5)
                reset = false;
        }
        else
        {
            distance = topCenterScreen - ePosition;
            distance.Normalize();
            ePosition += distance * Time.deltaTime * speed;
        }
    }

    public override void OnHit()
    {
        Health -= 10;
    }
}
