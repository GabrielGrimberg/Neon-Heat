using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLights : MonoBehaviour {
    public float rotationsPerSecond = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, (rotationsPerSecond * 360.0f) * Time.deltaTime, 0));
	}
}
