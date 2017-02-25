using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnim : MonoBehaviour {

	float accelData;
	Vector3 currentAngle;
	// Use this for initialization
	void Start () {
		currentAngle = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		float.TryParse (UDPReceive.lastReceivedUDPPacket, out accelData);


		Debug.Log (transform.eulerAngles.z);

		if (accelData > 0.12) {
			currentAngle = new Vector3 (currentAngle.x, currentAngle.y, Mathf.Lerp (currentAngle.z, 12, Time.deltaTime));
		} else if (accelData < -0.12) {
			currentAngle = new Vector3 (currentAngle.x, currentAngle.y,Mathf.Lerp(currentAngle.z, 348, Time.deltaTime));
		} else {
			currentAngle = new Vector3 (currentAngle.x, currentAngle.y,Mathf.Lerp(currentAngle.z, 0, Time.deltaTime));
		}


		transform.eulerAngles = currentAngle;
	}
}
