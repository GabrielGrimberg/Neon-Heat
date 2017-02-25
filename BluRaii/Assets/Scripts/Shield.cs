using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield {
    public bool onOff = false;
    InvertedColor invertedColor;
	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    public void Update() {
        if (invertedColor == null) {
            invertedColor = Info.getInvertedColorEffects();
        } else {
            if (Input.GetKey(KeyCode.Space)) {
                onOff = true;
            } else {
                onOff = false;
            }

            invertedColor.onOff = onOff;
        }
    }
}
