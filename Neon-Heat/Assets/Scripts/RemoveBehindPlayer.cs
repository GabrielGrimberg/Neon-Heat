using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBehindPlayer : MonoBehaviour {
    GameObject player;
    float timeToLive = 1f;
    float timeOutOfRange = 0;

	// Use this for initialization
	void Start () {
        player = Info.getPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.z < transform.position.z) {
            if(Vector3.Distance(player.transform.position, transform.position) > 10000) {
                if (timeOutOfRange == 0) {
                    timeOutOfRange = Time.time;
                }

                if (timeOutOfRange + timeToLive < Time.time) {
                    Destroy(gameObject);
                }
            } else {
                timeOutOfRange = 0;
            }
        }
	}
}
