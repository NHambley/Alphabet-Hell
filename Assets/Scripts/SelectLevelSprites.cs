using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectLevelSprites : MonoBehaviour {
    SceneManagerScript sM;
    static int lvl;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "TestScene")
        {
            Destroy(gameObject);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mp.z = 0;
            if (GetComponent<Collider2D>().bounds.Contains(mp))
            {
                handleTouch();
            }
        }
    }
    private void handleTouch()
    {
        Debug.Log("Got here");
        if (this.tag == "Level1")
            
        if (this.tag == "Level2")
            SceneManagerScript.level =2;
        if (this.tag == "Level3")
            SceneManagerScript.level =3;
        if (this.tag == "Level4")

        if (this.tag == "Level5")

        if (this.tag == "Level6")
            SceneManagerScript.level =6;
        if (this.tag == "Level7")
            SceneManagerScript.level =7;
        if (this.tag == "Level8")
            SceneManagerScript.level =8;
        if (this.tag == "Level9")
            SceneManagerScript.level =9;

        Debug.Log(SceneManagerScript.level);
        
        SceneManager.LoadScene("TestScene");
        
    }

    
}
