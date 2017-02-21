using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    public Vector3 startPosition;
    public Vector3 endPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime / 0.2f);
        transform.LookAt(endPosition);
        transform.Rotate(Vector3.up * 90);
    }

    public static void spawn(Vector3 startPosition, Vector3 endPosition) {
        GameObject rocket = Instantiate(Resources.Load("rocketA"), startPosition, Quaternion.identity) as GameObject;
        rocket.GetComponent<Rocket>().endPosition = endPosition;
    }
}
