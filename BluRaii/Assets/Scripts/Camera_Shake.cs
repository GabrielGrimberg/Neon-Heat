using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Camera_Shake : MonoBehaviour{
    const float tiltToPositionRatio = 10;

    GameObject camShake;
    List<Shake> shakes = new List<Shake>();

	// Use this for initialization
	void Start () {
        camShake = GameObject.FindGameObjectWithTag("CameraShake");
        AddShake(1.5f, 2000000); //This is the background shake, but it should be a special case.
    }
	
	// Update is called once per frame
	void Update () {
        //Lerp back to original position but not as aggressively.
        if (shakes.Count > 0) {
            camShake.transform.localPosition = Vector3.Lerp(camShake.transform.localPosition, Vector3.zero, Time.deltaTime * 5);
            camShake.transform.localRotation = Quaternion.Lerp(camShake.transform.localRotation, Quaternion.identity, Time.deltaTime * 5);
        }

        //Check and mark for finished shakes.
        foreach (Shake shake in shakes) {
            if (shake.startTime + shake.duration < Time.time) {
                shake.finished = true;

                //camShake.transform.localPosition = Vector3.zero;
                //camShake.transform.localRotation = Quaternion.identity;
                //Reset the camera to its original position once the shake is done.
                //mainCam.transform.Translate(mainCam.transform.localPosition - shake.originalLocalPosition); //Reset position by subtracting the original difference.
                //mainCam.transform.Translate(shake.positionChanges * -1);
                //This line doesn't always reset the rotation properly.
                //mainCam.transform.Rotate((mainCam.transform.localRotation * Quaternion.Inverse(shake.originalLocalRotation)).eulerAngles); //Reset rotation by subtracting original difference.

                //mainCam.transform.localRotation = mainCam.transform.localRotation * Quaternion.Inverse(shake.rotationChanges);
                //mainCam.transform.localRotation = shake.originalLocalRotation;
                camShake.transform.Translate(shake.positionChanges);
                camShake.transform.localRotation = shake.originalLocalRotation;
            }
        }

        //Filter out finished shakes
        shakes = (from s in shakes where !s.finished select s).ToList<Shake>();

        //Find the strongest shake and quake it.
        if (shakes.Count > 0) {
            Shake strongestShake = shakes.OrderByDescending(s => s.intensity).First();
            Quake(strongestShake);
        }

        //Lerp back aggressively to original position if no shakes.
        if (shakes.Count == 0) {
            camShake.transform.localPosition = Vector3.Lerp(camShake.transform.localPosition, Vector3.zero, 0.15f / Time.deltaTime);
            camShake.transform.localRotation = Quaternion.Lerp(camShake.transform.rotation, Quaternion.identity, 0.15f / Time.deltaTime);
        }
    }

    void Quake(Shake shake) {
        /*
            This method will change the position and rotation of the camera randomly based on the intensity of the shake.
        */

        //Position changes.
        Vector3 randomVector = Vector3.zero;

        int xDirectionPos = (shake.positionChanges.x > 0) ? -1 : 1;
        int yDirectionPos = (shake.positionChanges.y > 0) ? -1 : 1;
        int zDirectionPos = (shake.positionChanges.z > 0) ? -1 : 1;

        randomVector.x = Random.Range(0, shake.intensity) * xDirectionPos;
        randomVector.y = Random.Range(0, shake.intensity) * yDirectionPos;
        randomVector.z = Random.Range(0, shake.intensity) * zDirectionPos;

        camShake.transform.localPosition += randomVector;
        shake.positionChanges += randomVector;

        //Tilt changes
        Vector3 randomRotation = Vector3.zero;

        int xDirectionRot = (shake.rotationChanges.x > 0) ? -1 : 1;
        int yDirectionRot = (shake.rotationChanges.y > 0) ? -1 : 1;
        int zDirectionRot = (shake.rotationChanges.z > 0) ? -1 : 1;

        randomRotation.x = Random.Range(0, shake.intensity) * xDirectionRot;
        randomRotation.y = Random.Range(0, shake.intensity) * yDirectionRot;
        randomRotation.z = Random.Range(0, shake.intensity) * zDirectionRot;
        randomRotation = randomRotation / tiltToPositionRatio;

        camShake.transform.localRotation = camShake.transform.localRotation * Quaternion.Euler(randomRotation);
        shake.rotationChanges = shake.rotationChanges + randomRotation;
    } 

    public void AddShake(float intensity, float duration) {
        Shake shake = new Shake();

        shake.originalLocalPosition = camShake.transform.localPosition;
        shake.originalLocalRotation = camShake.transform.localRotation;

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

        public Vector3 positionChanges;
        public Vector3 rotationChanges;
    }
}
