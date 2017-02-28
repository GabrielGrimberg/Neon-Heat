using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
        //0, 266, 1023
        //transform.localPosition = new Vector3(0, 266, 1023) + player.transform.position;
        //transform.localRotation = Quaternion.Euler(new Vector3(-5.13f, -180, 0));
	}
}
