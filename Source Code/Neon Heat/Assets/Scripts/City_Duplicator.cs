using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Duplicator : MonoBehaviour {
    public GameObject city;
    public GameObject cityCrackLeft;
    public GameObject cityCrackRight;

    public static Vector3 cityStart;
    public static Vector3 cityEnd;

    public GameObject PortalDiskThing1;
    public GameObject PortalDiskThing2;

    public GameObject cityPrefab;
    public GameObject cityRightPrefab;
    public GameObject cityLeftPrefab;
    Vector3 citySize;

    // Use this for initialization
    void Start () {
        cityPrefab = Resources.Load("The City4 (1)") as GameObject;
        cityRightPrefab = Resources.Load("CityCrackLeft (2)") as GameObject;
        cityLeftPrefab = Resources.Load("New City Crack Right") as GameObject;
        SpawnCities();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnCities() {
        cityStart = city.transform.Find("SouthEnd").transform.position;
        Vector3 size = city.GetComponent<BoxCollider>().bounds.size;
        citySize = size;
        GameObject endCity = null;

        float z = size.z;
        for (int i = 0; i < 5; i++) {
            if (Random.Range(0, 3) == 1 && i > 20) {
                if (Random.Range(0, 2) == 1) {
                    cityEnd = Object.Instantiate(cityLeftPrefab, new Vector3(city.transform.position.x, city.transform.position.y, z), city.transform.rotation).transform.position;
                } else {
                    cityEnd = Object.Instantiate(cityRightPrefab, new Vector3(city.transform.position.x, city.transform.position.y, z), city.transform.rotation).transform.position;
                }
            } else {
                endCity = Object.Instantiate(cityPrefab, new Vector3(city.transform.position.x, city.transform.position.y, z), city.transform.rotation);
                cityEnd = endCity.transform.position;
            }

            z -= size.z;
        }

        PortalDiskThing1.transform.position = endCity.transform.Find("SouthEnd").transform.position;
    }

    public Vector3 SpawnCities(Vector3 location) {
        cityStart = location;
        Vector3 size = citySize;
        GameObject endCity = null;

        float z = size.z;
        for (int i = 0; i < 100; i++) {
            if (Random.Range(0, 3) == 1 && i > 20) {
                if (Random.Range(0, 2) == 1) {
                    cityEnd = Object.Instantiate(cityLeftPrefab, new Vector3(location.x, location.y, z), Quaternion.identity).transform.position;
                } else {
                    cityEnd = Object.Instantiate(cityRightPrefab, new Vector3(location.x, location.y, z), Quaternion.identity).transform.position;
                }
            } else {
                endCity = Object.Instantiate(cityPrefab, new Vector3(location.x, location.y, z), Quaternion.identity);
                cityEnd = endCity.transform.position;
            }

            z -= size.z;
        }

        return endCity.transform.Find("SouthEnd").transform.position;
    }

    public void DeleteCities() {
        GameObject[] cities = GameObject.FindGameObjectsWithTag("CityCrack");

        foreach (GameObject city in cities) {
            Destroy(city);
        }
    }
}
