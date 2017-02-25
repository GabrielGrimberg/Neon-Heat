using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleExplosion : MonoBehaviour {
    ParticleSystem pSystem;

	// Use this for initialization
	void Start () {
        pSystem = gameObject.GetComponent<ParticleSystem>();

        pSystem.Emit(100);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Explode(Vector3 position) {
        Instantiate(Resources.Load("ObstacleExplosion"), position, Quaternion.Euler(new Vector3(-20, 180, 0)));
    }
}
