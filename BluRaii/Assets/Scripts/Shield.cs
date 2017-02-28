using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield {
    public bool onOff = false;
    InvertedColor invertedColor;
    GameObject sphere;

    // Update is called once per frame
    public void Update() {
        if (sphere == null) {
            sphere = GameObject.FindGameObjectWithTag("ShieldSphere");
        }


        if (invertedColor == null) {
            invertedColor = Info.getInvertedColorEffects();
        } else {
            if (Input.GetKey(KeyCode.Space)) {
                onOff = true;
            } else {
                onOff = false;
            }

            invertedColor.onOff = onOff;

            if (onOff) {
                sphere.SetActive(true);
            } else {
                sphere.SetActive(false);
            }
        }
    }
}
