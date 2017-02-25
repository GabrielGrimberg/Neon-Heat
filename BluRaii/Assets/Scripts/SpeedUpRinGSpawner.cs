using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpRinGSpawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("SpawnRing", 2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnRing() {
        SpeedUpRing.Spawn();
        //Invoke("SpawnLaser", Random.Range(0.2f, 0.8f));
        Invoke("SpawnRing", Random.Range(4f, 12f));
    }
}
