﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Player : MonoBehaviour
{
    public GameObject sceneManager;
    public GameObject particlePrefab;

    SceneManagerScript managerScript;

    // look at https://docs.unity3d.com/Manual/MobileInput.html for touch controls
    // and https://docs.unity3d.com/Manual/HOWTO-UIMultiResolution.html for resizing the screen depending on mobile device
    int score;
    public int health = 50;
    //public float health;
    int lives;

    float timer;

    [SerializeField, Range(0,20)]
    float seekScalar;
    [SerializeField, Range(0, 20)]
    float maxSpeed;

    //float yPos; // keep the y position at the same value all game

    // used to determine the movements of the player's mouse/finger this will be used to determine the direction the player is moved
    Vector2 prevMPos; 
    Vector2 mPosition;
    Vector2 pPosition;// keep track of where the player is
    Vector2 pVelocity = Vector2.zero;
    Vector2 force = Vector2.zero;


    //Variables for touch controls 
    Vector2 fingerPos;
    // end variables for touch controls
    [SerializeField]
    GameObject bullet;

    float lastBulletTime = -1.0f;

    Vector3 spawnPos;

	AudioManager audioManager;
	bool audioShot = false;

	PlayerLives playerLives;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                health = 0;
                Destroy(gameObject);
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		playerLives = GameObject.FindObjectOfType<PlayerLives>().GetComponent<PlayerLives>();

		mPosition = Input.mousePosition;
        pPosition = gameObject.transform.position;
        managerScript = sceneManager.GetComponent<SceneManagerScript>();
        spawnPos = new Vector3(transform.position.x, transform.position.y, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (sceneManager == null)
            GameObject.Find("SceneManager");
        if (managerScript == null)
            managerScript = sceneManager.GetComponent<SceneManagerScript>();

        // check input here then call the shoot and move methods depending on whether it's true or not
        if (Input.GetMouseButton(0))
        {
            mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Move(mPosition);
            if (GetComponent<Renderer>().isVisible && !playerLives.playerHasBeenHit)
            {
                Shoot();
            }
        }
        else
        {
            pVelocity *= 0.95f;
        }

        pPosition += pVelocity;
        force = Vector2.zero;
        gameObject.transform.position = pPosition;

        gameObject.GetComponent<ParticleGenerator>().GenerateParticles((SPRITE)(Random.Range(6, 9)), 3, gameObject.transform.position + new Vector3(0.85f, 0.6f, 0.0f), new Vector3(0.0f, -0.1f, 0.0f), new Vector3(0.5f, 0.5f, 0.5f), 20.0f, 0.1f, 0.5f);
        gameObject.GetComponent<ParticleGenerator>().GenerateParticles((SPRITE)(Random.Range(6, 9)), 3, gameObject.transform.position + new Vector3(-0.85f, 0.6f, 0.0f), new Vector3(0.0f, -0.1f, 0.0f), new Vector3(0.5f, 0.5f, 0.5f), 20.0f, 0.1f, 0.5f);
    }

    // periodically have the player shoot a projectile using a timer and a cooldown 
    void Shoot()
    {
		Vector3 positionOffset = new Vector3(0, 1);
		if (Time.time - lastBulletTime >= 0.1f || lastBulletTime == -1.0f)
        {
			audioManager.Play("shot");
			GameObject newBullet = Instantiate(bullet,transform.position + positionOffset,Quaternion.identity);
            managerScript.AddPlayerBullet(newBullet);
            lastBulletTime = Time.time;
            gameObject.GetComponent<ParticleGenerator>().GenerateParticles((SPRITE)(Random.Range(6, 9)), 5, gameObject.transform.position + positionOffset, new Vector3(0.0f, 0.2f, 0.0f), new Vector3(1.0f,1.0f,1.0f), 20.0f, 0.2f, 0.5f);
        }
    }

    // move the player in relation to how the player is moving their finger across the screen. Don't match the player and finger positions instead
    // see the direction the finger/mouse is moving in and then apply that movement to the player
    // compare the finger (mouse) position to its position last frame, normalize that vector, multiply by Time.deltaTime, then add it to the player position
    void Move(Vector2 mPos)
    {
        //float distanceRatio = Mathf.Abs(mPosition.x - pPosition.x);
        float distanceRatio = Vector2.Distance(mPosition, pPosition);
        if(distanceRatio >= 0.05f)
        {
            if (distanceRatio > 1f)
                distanceRatio = 1f;

            //else if (distanceRatio < 0.3f) distanceRatio = 0.3f;
            Vector2 desiredVelocity = (mPosition - pPosition).normalized * seekScalar;
            force = (desiredVelocity - pVelocity) * Time.deltaTime;
            if (distanceRatio < 1)
                pVelocity *= 0.96f;

            pVelocity += force;
            pVelocity = Vector2.ClampMagnitude(pVelocity, maxSpeed);
        }
        else if(pVelocity.magnitude <= 0.1f)
        {
            pVelocity *= 0.9f;
        }
    }

    public void SetToSpawn() { pPosition = spawnPos; }
}
