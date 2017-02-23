using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Checks if a key is double pressed and calls the passed callback.
*/

public class DoubleTap {
    KeyCode key;
    float timeToPress;
    float timeLastPressed;
    Action callback;

	public DoubleTap(KeyCode key, float timeToPress, Action callback) {
        this.key = key;
        this.timeToPress = timeToPress;
        this.callback = callback;
    }
	
	// This needs to be called each frame by something else.
	public void Update () {
        if (Input.GetKeyDown(key)) {
            if (timeToPress + timeLastPressed > Time.time) {
                callback.Invoke();
                timeLastPressed = 0;
            } else {
                timeLastPressed = Time.time;
            }
        }

        //If any other key was pressed inbetween, no double tap.
        if(Input.anyKeyDown && !Input.GetKeyDown(key)) {
            timeLastPressed = 0;
        }
    }
}
