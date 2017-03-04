using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Spawner : MonoBehaviour {
    Player player;

	// Use this for initialization
	void Start () {
        Invoke("SpawnPillar", 3.0f);
        player = Info.getPlayer().GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnPillar() {
        //Invoke("SpawnPillar", Random.Range(0.2f, 0.3f));
        if (player.desertMode) {
            Invoke("SpawnPillar", Random.Range(0.005f, 0.006f));
        } else {
            Invoke("SpawnPillar", Random.Range(0.1f, 0.6f));
        }
        

        if (Random.Range(0, 2) == 1) {
            Pop_Up_Pillar.Spawn();
        } else {
            Pyramidka.Spawn();
        }
        
    }

    public void DeleteObstacles() {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Pillar");

        foreach (GameObject obstacle in obstacles) {
            Destroy(obstacle);
        }
    }
}
