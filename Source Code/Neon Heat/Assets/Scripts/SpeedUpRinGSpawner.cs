using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpRinGSpawner : MonoBehaviour {
    Player player;

    // Use this for initialization
    void Start () {
        player = Info.getPlayer().GetComponent<Player>();
        Invoke("SpawnRing", 2f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnRing() {
        SpeedUpRing.Spawn();
        if (player.desertMode) {
            Invoke("SpawnRing", Random.Range(0.005f * 4 * 20, 0.006f * 4 * 20));
        } else {
            Invoke("SpawnRing", Random.Range(4, 12));
        }
    }
}
