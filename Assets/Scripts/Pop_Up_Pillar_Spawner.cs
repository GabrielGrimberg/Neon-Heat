using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop_Up_Pillar_Spawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("SpawnPillar", 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnPillar() {
        Invoke("SpawnPillar", Random.Range(0.2f, 0.3f));
        Pop_Up_Pillar.Spawn();
    }
}
