using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour {
    GameObject desertPlane;
    Player player;

    GameObject onPlane;

	// Use this for initialization
	void Start () {
        desertPlane = Resources.Load("desertplane") as GameObject;
        player = Info.getPlayer().GetComponent<Player>();

    }
	
	// Update is called once per frame
	void Update () {
		if(player.desertMode && player.desertPlane != null) {
            if (Vector3.Distance(player.transform.position, player.desertPlane.transform.position) > 1000
                && player.transform.position.z < player.desertPlane.transform.position.z) {
                Vector3 planeDimensions = player.desertPlane.GetComponent<BoxCollider>().size;
                Vector3 planeScale = player.desertPlane.transform.localScale;

                GameObject north = Instantiate(desertPlane);
                north.transform.position = player.desertPlane.transform.position + Vector3.back * planeDimensions.z * planeScale.z;

                GameObject northwest = Instantiate(desertPlane);
                northwest.transform.position = player.desertPlane.transform.position + Vector3.back * planeDimensions.z * planeScale.z + Vector3.right * planeDimensions.x * planeScale.x;

                GameObject northeast = Instantiate(desertPlane);
                northeast.transform.position = player.desertPlane.transform.position + Vector3.back * planeDimensions.z * planeScale.z + Vector3.left * planeDimensions.x * planeScale.x;
            }
        }
	}
}
