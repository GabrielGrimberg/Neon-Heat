using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour {
    Camera mainCam;
    List<Shake> shakes = new List<Shake>();

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        foreach(Shake shake in shakes) {
            if(shake.startTime + shake.duration > Time.time) {
                Quake(shake);
            } else {
                if(!shake.finished) {
                    mainCam.transform.position = shake.originalPosition;
                }

                shake.finished = true;
            }
        }
    }

    void Quake(Shake shake) {
        Vector3 randomVector = Vector3.zero;

        if (transform.localPosition.x > shake.originalPosition.x) {
            randomVector.x = Random.Range(-shake.intensity, shake.intensity);
        } else {
            randomVector.x = Random.Range(-shake.intensity, shake.intensity);
        }

        if (transform.localPosition.y > shake.originalPosition.y) {
            randomVector.y = Random.Range(-shake.intensity, shake.intensity);
        } else {
            randomVector.y = Random.Range(-shake.intensity, shake.intensity);
        }

        if (transform.localPosition.z > shake.originalPosition.z) {
            randomVector.z = Random.Range(-shake.intensity, shake.intensity);
        } else {
            randomVector.z = Random.Range(-shake.intensity, shake.intensity);
        }

        mainCam.transform.localPosition = shake.originalPosition + randomVector;
    } 

    public void AddShake(float intensity, float duration) {
        Shake shake = new Shake();

        shake.originalPosition = mainCam.transform.localPosition;
        shake.intensity = intensity;
        shake.duration = duration;
        shake.startTime = Time.time;

        shakes.Add(shake);
    }

    class Shake {
        public float intensity;
        public float startTime;
        public float duration;
        public bool finished = false;

        public Vector3 originalPosition;
    }
}
