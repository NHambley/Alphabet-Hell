using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_H : GenericBossScript
{
    public GameObject[] firepoints;

    public GameObject basicBullet;
    public GameObject bigBullet;
    SceneManagerScript sm;

    float fpTimer = 1.0f;// left and right clocks
    float track = 1.0f;

    float fpTimer2 = 1.5f;// center, more powerful clock
    float track2 = 1.5f;

	AudioManager audioManager;

    public override void OnHit(Vector3 pos)
    {
        Health -= 15;
        audioManager.PlaySound("ting");
    }

    // Use this for initialization
    void Start ()
    {
        Health = 500;
        sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManagerScript>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlaySound("Hypno");
	}

	// Update is called once per frame
	void Update ()
    {
        // decrement the timers
        track -= Time.deltaTime;
        track2 -= Time.deltaTime;
           
        if(track <= 0)// small clocks
        {
            track = fpTimer;

            GameObject bull1 = Instantiate(basicBullet, firepoints[0].transform.position, Quaternion.identity);
            GameObject bull2 = Instantiate(basicBullet, firepoints[1].transform.position, Quaternion.identity);

            int rnum1 = Random.Range(0, 10);
            int rnum2 = Random.Range(0, 10);

            if (rnum1 % 2 == 0)
            {
                sm.AddEnemyBullet(bull1);
                bull1.GetComponent<E_HBullet>().Real = true;
                audioManager.PlaySound("pop2");
            }
            else
                bull1.GetComponent<E_HBullet>().Real = false;

            if (rnum2 % 2 == 0)
            {
                sm.AddEnemyBullet(bull2);
                bull2.GetComponent<E_HBullet>().Real = true;
                audioManager.PlaySound("pop2");
            }
            else
                bull2.GetComponent<E_HBullet>().Real = false;
        }

        if(track2 <= 0)// big clock
        {
            track2 = fpTimer2;

            GameObject bull = Instantiate(bigBullet, firepoints[2].transform.position, Quaternion.identity);
            int rnum = Random.Range(0, 10);

            if(rnum % 5  != 0)
            {
                sm.AddEnemyBullet(bull);
                bull.GetComponent<E_HBullet>().Real = true;
                audioManager.PlaySound("pop2");
            }
        }
    }
}
