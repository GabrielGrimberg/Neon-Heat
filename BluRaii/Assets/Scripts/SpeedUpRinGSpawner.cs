using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpRinGSpawner : MonoBehaviour {

	// Use this for initialization
<<<<<<< HEAD
	void Start () {
        Invoke("SpawnRing", 2f);
=======
	void Start () {
        Invoke("SpawnRing", 2f);
>>>>>>> baab324c3e41ca417d50554c385a99dd03d227a5
    }
	
	// Update is called once per frame
	void Update () {
		
	}

<<<<<<< HEAD
    void SpawnRing() {
        SpeedUpRing.Spawn();
        //Invoke("SpawnLaser", Random.Range(0.2f, 0.8f));
        Invoke("SpawnRing", Random.Range(4f, 12f));
=======
    void SpawnRing() {
        SpeedUpRing.Spawn();
        //Invoke("SpawnLaser", Random.Range(0.2f, 0.8f));
        Invoke("SpawnRing", Random.Range(4f, 12f));
>>>>>>> baab324c3e41ca417d50554c385a99dd03d227a5
    }
}
