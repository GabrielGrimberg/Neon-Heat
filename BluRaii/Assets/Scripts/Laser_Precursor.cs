using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Precursor : MonoBehaviour {
    Vector3 endPosition;

	// Use this for initialization
	void Start () {
        transform.position = City_Duplicator.cityEnd;

        //endPosition = City_Duplicator.cityStart + new Vector3(Random.Range(-1000, 1000), 0, 0);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        endPosition = new Vector3(City_Duplicator.cityStart.x, City_Duplicator.cityStart.y, player.transform.position.z) + new Vector3(Random.Range(-1000, 1000), 0, 0);
        Invoke("spawnRocket", 1f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime / 0.2f);
	}

    void spawnRocket() {
        Rocket.spawn(City_Duplicator.cityEnd, endPosition);
    }
}
