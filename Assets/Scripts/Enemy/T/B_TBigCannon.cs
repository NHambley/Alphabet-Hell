using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_TBigCannon : MonoBehaviour
{

    /// <summary>
    /// Each gun is meant track the player and make sure that their cannons do not rotate too far past their tracking point
    /// if they do stop the firing timer
    /// </summary>

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject bullet;

    // the timer for triggering the big cannon
    float chargeTimer = 2.0f;
    float cTimer = 2.0f;

    // timer for time between quick cannon shots
    float shootTimer = 0.5f;
    float sTimer = 0.5f;
    [SerializeField, Range(20, 50)]
    float rotSpeed;

    bool seePlayer = false;// determines if the timer is subtracted from 
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rotSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        SeePlayer();

        // if they see the player subtract from the timer and rotate them to look towards the player
        if (seePlayer)
        {
            cTimer -= Time.deltaTime;
            Vector3 dir = player.transform.position - transform.position;
            dir = gameObject.transform.InverseTransformDirection(dir);
            float angleBetween = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotSpeed * angleBetween);
        }

        if (cTimer <= 0)
        {
            Fire();
        }
    }

    // check if the cannon is able to actually see the player
    void SeePlayer()
    {
        // get raycast hit info from the transform of the cannon to the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y));

        // if it hits the player decrement the charge timer, if not don't
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            seePlayer = true;
        }
        else
            seePlayer = false;
    }

    // fire a bullet at the player's current position
    void Fire()
    {
        int bulletCount = 0;
        shootTimer -= Time.deltaTime;

        // if the lazer has been going on for the specific time end it
        if(shootTimer <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            shootTimer = sTimer;
            bulletCount++;
        }

        // check if the cannon has fired 5 bullets
        if(bulletCount == 5)
        {
            cTimer = chargeTimer;
            bulletCount = 0;
        }
    }
}
