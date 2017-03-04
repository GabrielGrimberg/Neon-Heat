using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("High Score").ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
