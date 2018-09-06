using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Player : MonoBehaviour
{
    public GameObject sceneManager;

    SceneManagerScript managerScript;

    // look at https://docs.unity3d.com/Manual/MobileInput.html for touch controls
    // and https://docs.unity3d.com/Manual/HOWTO-UIMultiResolution.html for resizing the screen depending on mobile device
    int score;
    int health;
    int lives;

    float timer;

    [SerializeField, Range(0,20)]
    float speed = 1f;

    // used to determine the movements of the player's mouse/finger this will be used to determine the direction the player is moved
    Vector2 prevMPos; 
    Vector2 mPosition;
    Vector2 pPosition;// keep track of where the player is

    [SerializeField]
    GameObject bullet;

    float lastBulletTime = -1.0f;

	// Use this for initialization
	void Start ()
    {
        mPosition = Input.mousePosition;
        pPosition = transform.position;
        managerScript = sceneManager.GetComponent<SceneManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		// check input here then call the shoot and move methods depending on whether it's true or not
        if(Input.GetMouseButton(0))
        {
            Move();
            Shoot();
        }
        // lock the y position, just have the player moving side to side for now

        gameObject.transform.position = pPosition;

        Debug.Log(gameObject.transform.position);
    }

    // periodically have the player shoot a projectile using a timer and a cooldown 
    void Shoot()
    {
        if(Time.time - lastBulletTime >= 0.1f || lastBulletTime == -1.0f)
        {
            GameObject newBullet = Instantiate(bullet,gameObject.transform.position,Quaternion.identity);
            //newBullet.transform.position.Set(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            managerScript.AddPlayerBullet(newBullet);
            lastBulletTime = Time.time;
        }
    }

    // move the player in relation to how the player is moving their finger across the screen. Don't match the player and finger positions instead
    // see the direction the finger/mouse is moving in and then apply that movement to the player
    // compare the finger (mouse) position to its position last frame, normalize that vector, multiply by Time.deltaTime, then add it to the player position
    void Move()
    {
        prevMPos = mPosition;
        mPosition = Input.mousePosition;

        Vector2 moveVec = mPosition - prevMPos;
        moveVec.Normalize();

        pPosition += moveVec * Time.deltaTime * speed;

    }
}
