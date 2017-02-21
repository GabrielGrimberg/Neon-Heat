using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Spawner : MonoBehaviour {
    public GameObject trailPrecursor;

	// Use this for initialization
	void Start () {
        Invoke("SpawnLaser", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnLaser() {
        GameObject t = Instantiate(trailPrecursor, City_Duplicator.cityEnd, Quaternion.identity);
        Invoke("SpawnLaser", Random.Range(0.2f, 0.8f));
    }
}
