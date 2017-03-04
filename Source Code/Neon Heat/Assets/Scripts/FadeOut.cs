using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (!GameObject.Find("PoliceSirens").GetComponent<AudioSource>().isPlaying) {
                GameObject.Find("PoliceSirens").GetComponent<AudioSource>().Play();
            }
            

            image.color = new Color(255, 255, 255, alpha);

            alpha += Time.deltaTime * 0.5f;
        }

        if (alpha >= 1) {
            PlayerPrefs.SetInt("High Score", Info.getPlayer().GetComponent<Player>().score / 500);
            SceneManager.LoadScene("DeathScene");
        }
    }

    public void StartFadeOut() {
        fadeOut = true;
        startTime = Time.time;
    }
}
