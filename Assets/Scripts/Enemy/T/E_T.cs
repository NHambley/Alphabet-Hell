using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_T : GenericEnemyScript
{
    /// <summary>
    /// the T enemy moves slowly around the screen and after it reaches its desired position, takes aim, and fires a fast bullet towards the player
    /// </summary>
    
    
    GameObject target; // where the enemy is at the point of charging up the shot
    float chargeTimer = 2.0f;
    [SerializeField]
    float timer = 2.0f;

    Vector2 ePosition;// position of this enemy
    //Rigidbody2D rb;
    Vector3 eulerAngleVelocity;
    Vector3 nextPos;

    [Range(1,5), SerializeField]
    float speed;// speed for the enemy
    
    [SerializeField]
    GameObject bullet;

    Camera cam;
    float hp;
    [SerializeField]
    bool firing = false;// is the enemy firing a bullet or not?

    SceneManagerScript sm;
	// Use this for initialization
	void Start ()
    {
        Health = 500;
        target = GameObject.FindGameObjectWithTag("Player");
        sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
        cam = Camera.main;

        speed = 1.0f;

        ePosition = transform.position;
        //rb = GetComponent<Rigidbody2D>();
        // Find the first position the tank will move to
        FindPosition();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		// if the tank is firing call the methods required, else it is moving to its next position 
        if(firing)
        {
            Fire();
        }
        else if(firing == false)// moving positions
        {
            Move();
        }
        transform.position = ePosition;
	}



    void Fire()
    {
        // mark down the fire timer and have the player track the movements of the player
        if(timer <= 0)
        {
            // fire a bullet and find a new position\
            sm.AddEnemyBullet(Instantiate(bullet, gameObject.transform.position, Quaternion.identity));
            firing = false;
            FindPosition();
            timer = chargeTimer;
        }
        else
        {
            timer -= Time.deltaTime;

            if (target != null)
            {
                Vector3 dir = target.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            }
        }
    }

    // move the enemy to its next position then change the firing bool to true
    void Move()
    {
        // rotate the tank to look at where it is going 
        Vector3 dir = nextPos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        // first get the angle between the enemy and the target position to rotate it
        Vector2 moveFrame = (nextPos - transform.position);
        moveFrame = moveFrame.normalized * speed * Time.deltaTime;
        ePosition += moveFrame;

        if(Vector2.Distance(ePosition, nextPos) < 1f)
        {
            firing = true;
        }
    }

    // find a new position for the tank to move to
    void FindPosition()
    {
        nextPos = new Vector2(Random.Range(-(cam.orthographicSize) * cam.aspect, (cam.orthographicSize) * cam.aspect), Random.Range(-(cam.orthographicSize) + 2, (cam.orthographicSize)));
    }

    public override void OnHit(Vector3 pos)
    {
        Health -= 20;
    }
}
