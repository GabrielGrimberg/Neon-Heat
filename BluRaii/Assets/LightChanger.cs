using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour {
    Light light;
    Color c1 = new Color(255, 0, 255);
    Color c2 = new Color(20, 0, 255);
    bool towards = false;

    // Use this for initialization
    void Start () {
        light = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        if (towards) {
            light.color = Color.Lerp(light.color, c2, Mathf.PingPong(Time.time, 1));
        } else {
            light.color = Color.Lerp(light.color, c1, Mathf.PingPong(Time.time, 1));
        }
		

        if (light.color == c1) {
            towards = !towards;
        }

        if (light.color == c2) {
            towards = !towards;
        }
    }
}
