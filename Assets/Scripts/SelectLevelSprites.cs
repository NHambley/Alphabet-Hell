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

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mp.z = 0;
            Debug.Log(mp);
            foreach (Collider2D level in FindObjectsOfType<Collider2D>())
            {
                if (level.bounds.Contains(mp))
                {
                    handleTouch(level.gameObject);
                }

            }
        }
    }
    private void handleTouch(GameObject level)
    {
        Debug.Log(level.tag);
        if (level.tag == "Level1")
            SceneManagerScript.level = 1;
        if (level.tag == "Level2")
            SceneManagerScript.level = 2;
        if (level.tag == "Level3")
            SceneManagerScript.level = 3;
        if (level.tag == "Level4")
            SceneManagerScript.level = 4;
        if (level.tag == "Level5")
            SceneManagerScript.level = 5;
        if (level.tag == "Level6")
            SceneManagerScript.level = 6;
        if (level.tag == "Level7")
            SceneManagerScript.level = 7;
        if (level.tag == "Level8")
            SceneManagerScript.level = 8;
        if (level.tag == "Level9")
            SceneManagerScript.level = 9;
        if (level.tag == "Level10")
            SceneManagerScript.level = 10;


        Debug.Log(SceneManagerScript.level);
        
        SceneManager.LoadScene("TestScene");
        
    }

    
}
