using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSpawnZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Pillar" || other.gameObject.tag == "SpeedUpRing") {
            Destroy(other.gameObject);
        }
    }
}
