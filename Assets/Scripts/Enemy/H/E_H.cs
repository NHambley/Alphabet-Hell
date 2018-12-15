using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_H : GenericEnemyScript
{
    Vector2 ePosition;
    [SerializeField]
    GameObject bullet;

    float bTimer = 2.0f;
    float timerTrack = 2.0f;

    [Range(2, 10), SerializeField]
    float speed;

    SceneManagerScript sm;
	AudioManager audioManager;

    public override void OnHit(Vector3 onHit)
    {
        Health -= 15;
        audioManager.PlaySound("ting");
    }

    // Use this for initialization
    void Start ()
    {
        Health = 150;
        ePosition = transform.position;
        speed = 2;
        sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
        //velocity = Vector3.zero;
        //acceleration = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
        velocity = Vector3.down * Time.deltaTime;
        //velocity = Vector3.zero;
        //acceleration = Vector3.zero;

        if (Health <= 0)
            Destroy(gameObject);

        timerTrack -= Time.deltaTime;
        // fire a bullet
        if(timerTrack <= 0)
        {
            timerTrack = bTimer;

            GameObject bull = Instantiate(bullet, transform.position, Quaternion.identity);
            int rnum = Random.Range(0, 10);
            
            if (rnum % 2 == 0)
            {
                sm.AddEnemyBullet(bull);
                bull.GetComponent<E_HBullet>().Real = true;
                audioManager.PlaySound("pop2");
            }
            else
                bull.GetComponent<E_HBullet>().Real = false;

        }
	}
}
