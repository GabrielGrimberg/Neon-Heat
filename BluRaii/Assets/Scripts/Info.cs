using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    This script contains static accessors to various objects.

    Most of the global scripts are on the DeLorean right now.
*/

public class Info {
    public static Camera_Shake getCameraShake() {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Camera_Shake>();
    }

    public static DistortImageEffect getDistortImageEffects() {
        return Camera.main.GetComponent<DistortImageEffect>();
    }

    public static GameObject getPlayer() {
        return GameObject.FindGameObjectWithTag("Player");
    }

    public static InvertedColor getInvertedColorEffects() {
        return Camera.main.GetComponent<InvertedColor>();
    }

    public static UnityStandardAssets.ImageEffects.MotionBlur getBlurEffects() {
        return Camera.main.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
    }

    public static FollowPlayer getFollowPlayer() {
        return GameObject.FindGameObjectWithTag("PlayerCameraAnchor").GetComponent<FollowPlayer>();
    }
}
