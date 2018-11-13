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
        // Check if there is a touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Check if finger is over a UI element 
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                HandleClick();   
                Debug.Log("UI is touched");
                //so when the user touched the UI(buttons) call your UI methods 
            }
            else
            {
                Debug.Log("UI is not touched");
                //so here call the methods you call when your other in-game objects are touched 
            }
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
