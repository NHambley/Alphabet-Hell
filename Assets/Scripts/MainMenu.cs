using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {

    // Use this for initialization
    public UnityEngine.UI.Button buttonComponent;
    void Start () {
        //buttonComponent.onClick.AddListener(HandleClick);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            Application.Quit();
            
        }
        if (Input.GetMouseButton(0))
        {
            HandleClick();
        }
    }

   //private void OnMouseUp()
   //{
   //    Debug.Log("gotinhere1");
   //    SceneManager.LoadScene("TestScene");
   //}

    public void HandleClick()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
