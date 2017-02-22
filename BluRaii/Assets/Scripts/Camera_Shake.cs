using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Camera_Shake : MonoBehaviour {
    Camera mainCam;
    List<Shake> shakes = new List<Shake>();

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        //Check and mark for finished shakes.
        foreach (Shake shake in shakes) {
            if (shake.startTime + shake.duration < Time.time) {
                shake.finished = true;

                //Reset the camera to its original position once the shake is done.
                mainCam.transform.Translate(mainCam.transform.localPosition - shake.originalLocalPosition); //Reset position by subtracting the original difference.
                mainCam.transform.Rotate((mainCam.transform.localRotation * Quaternion.Inverse(shake.originalLocalRotation)).eulerAngles); //Reset rotation by subtracting original difference.
            }
        }

        //Filter out finished shakes
        shakes = (from s in shakes where !s.finished select s).ToList<Shake>();

        //Find the strongest shake and quake it.
        if (shakes.Count > 0) {
            Shake strongestShake = shakes.OrderByDescending(s => s.intensity).First();
            Quake(strongestShake);
        }
    }

    void Quake(Shake shake) {
        /*
            This method will change the position and rotation of the camera randomly based on the intensity of the shake.
        */
        Vector3 randomVector = Vector3.zero;

        if (transform.localPosition.x > shake.originalLocalPosition.x) {
            randomVector.x = Random.Range(-shake.intensity, shake.intensity);
        } else {
            randomVector.x = Random.Range(-shake.intensity, shake.intensity);
        }

        if (transform.localPosition.y > shake.originalLocalPosition.y) {
            randomVector.y = Random.Range(-shake.intensity, shake.intensity);
        } else {
            randomVector.y = Random.Range(-shake.intensity, shake.intensity);
        }

        if (transform.localPosition.z > shake.originalLocalPosition.z) {
            randomVector.z = Random.Range(-shake.intensity, shake.intensity);
        } else {
            randomVector.z = Random.Range(-shake.intensity, shake.intensity);
        }

        mainCam.transform.localPosition = shake.originalLocalPosition + randomVector;


        Vector3 randomRotation = Vector3.zero;

        if (transform.localRotation.x > shake.originalLocalRotation.x) {
            randomRotation.x = Random.Range(-shake.intensity, shake.intensity);
        } else {
            randomRotation.x = Random.Range(-shake.intensity, shake.intensity);
        }

        if (transform.localRotation.y > shake.originalLocalRotation.y) {
            randomRotation.y = Random.Range(-shake.intensity, shake.intensity);
        } else {
            randomRotation.y = Random.Range(-shake.intensity, shake.intensity);
        }

        if (transform.localRotation.z > shake.originalLocalRotation.z) {
            randomRotation.z = Random.Range(-shake.intensity, shake.intensity);
        } else {
            randomRotation.z = Random.Range(-shake.intensity, shake.intensity);
        }

        mainCam.transform.localRotation = shake.originalLocalRotation * Quaternion.Euler(randomRotation);
    } 

    public void AddShake(float intensity, float duration) {
        Shake shake = new Shake();

        shake.originalLocalPosition = mainCam.transform.localPosition;
        shake.originalLocalRotation = mainCam.transform.localRotation;

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

        public Vector3 originalLocalPosition;
        public Quaternion originalLocalRotation;
    }
}
