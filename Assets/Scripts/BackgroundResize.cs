using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundResize : MonoBehaviour
{
    SpriteRenderer sr;

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<SpriteRenderer>();
        if (sr == null)
            return;

        // using the width and height of the sprite and the width and height of the camera calculate the scale the backgruond needs to be set to and scale it
        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        double worldScreenHeight = Camera.main.orthographicSize * 2.0;
        double worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3((float)worldScreenWidth / width, (float)worldScreenHeight / height, 1);
    }
}
