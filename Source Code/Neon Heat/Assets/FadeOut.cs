using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeOut : MonoBehaviour {
    bool fadeOut = false;
    Image image;
    float startTime;
    float alpha = 0;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.color = new Color(255, 255, 255, 0);
    }
	
	// Update is called once per frame
	void Update () {
		if(fadeOut) {
            image.color = new Color(255, 255, 255, alpha);

            alpha += Time.deltaTime * 0.5f;
        }
	}

    public void StartFadeOut() {
        fadeOut = true;
        startTime = Time.time;
    }
}
