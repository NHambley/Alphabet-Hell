using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {

    // Use this for initialization
    public UnityEngine.UI.Button buttonComponent;
    void Start () {
        buttonComponent.onClick.AddListener(HandleClick);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("gotinhere");
            Application.Quit();
            Debug.Log("quit");
        }
            
	}

   //private void OnMouseUp()
   //{
   //    Debug.Log("gotinhere1");
   //    SceneManager.LoadScene("TestScene");
   //}

    public void HandleClick()
    {
        SceneManager.LoadScene("TestScene");
    }
}
