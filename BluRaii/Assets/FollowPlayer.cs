using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.localPosition = new Vector3(0, 266, 1023) + player.transform.position;
        transform.localRotation = Quaternion.Euler(new Vector3(-5.13f, -180, 0));

        transform.localPosition = Vector3.Lerp(transform.localPosition, player.transform.position + new Vector3(0, 266, 1023), Mathf.SmoothStep(0.0f, 1.0f, Time.deltaTime / 0.05f));
        //transform.localPosition += new Vector3(0, 266, 1023);
    }
}
