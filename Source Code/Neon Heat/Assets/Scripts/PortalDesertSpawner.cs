using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalDesertSpawner : MonoBehaviour {
    public GameObject entrancePortal;
    public GameObject exitPortal;
    public Vector3 newEntracePortalPosition;
    //public Vector3 newExitPortalPosition = Vector3.zero;
    public Vector3 newExitPortalPosition = new Vector3(-2212, -128, 5277);
    Player player;
    GameObject playerGO;
    LineRenderer line;
    public Text tPortalDistance;
    bool cityToDesert = true;


    // Use this for initialization
    void Start () {
        line = gameObject.GetComponent<LineRenderer>();
        player = Info.getPlayer().GetComponent<Player>();
        playerGO = Info.getPlayer();
        newEntracePortalPosition = entrancePortal.transform.position;
        Invoke("UpdateEntrancePosition", 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (player.desertMode) {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newEntracePortalPosition);
        }

        tPortalDistance.text = "Distance To Portal: " + Vector3.Distance(playerGO.transform.position, newEntracePortalPosition);
    }

    public void EnterDesert() {
        if (cityToDesert) {
            newEntracePortalPosition = exitPortal.transform.position + Vector3.back * Random.Range(100000 * 5, 200000 * 5) + Vector3.right * Random.Range(-100000.0f / 2, 100000.0f / 2);
            Info.getCityDuplicator().DeleteCities();
            Info.getPlayer().GetComponent<Obstacle_Spawner>().DeleteObstacles();
        }
        //newExitPortalPosition = Vector3.zero;
        newExitPortalPosition = new Vector3(-2212, -128, 5277);
        cityToDesert = !cityToDesert;

        Invoke("ChangeEntrancePortalPosition", 2.0f);
        Invoke("ChangeExitPortalPosition", 2.0f);
    }

    public void EnterCity() {
        newEntracePortalPosition = Info.getCityDuplicator().SpawnCities(Vector3.zero);
        newExitPortalPosition = new Vector3(-76741, 0, 6393);
        /*
        GameObject temp = entrancePortal;
        entrancePortal = exitPortal;
        exitPortal = temp;
        */

        cityToDesert = !cityToDesert;
        Invoke("ChangeEntrancePortalPosition", 2.0f);
        Invoke("ChangeExitPortalPosition", 2.0f);

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Pillar");

        foreach (GameObject obstacle in obstacles) {
            Destroy(obstacle);
        }
    }

    void ChangeEntrancePortalPosition() {
        entrancePortal.transform.position = newEntracePortalPosition;
    }

    void ChangeExitPortalPosition() {
        exitPortal.transform.position = newExitPortalPosition;
    }

    void UpdateEntrancePosition() {
        newEntracePortalPosition = entrancePortal.transform.position;
    }
}
