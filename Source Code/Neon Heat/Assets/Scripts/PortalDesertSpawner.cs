using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDesertSpawner : MonoBehaviour {
    public GameObject entrancePortal;
    public GameObject exitPortal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnterDesert() {
        entrancePortal.transform.position = exitPortal.transform.position + Vector3.back * Random.Range(100000, 200000) + Vector3.right * Random.Range(-10000.0f, 10000.0f);
    }
}
