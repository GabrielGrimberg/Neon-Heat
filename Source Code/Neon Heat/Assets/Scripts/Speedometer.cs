using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer {
    RectTransform mask;
    Rigidbody playerRb;

    Vector2 originalSize;

    public Speedometer() {
        Start();
    }

	// Use this for initialization
	void Start () {
        mask = GameObject.FindGameObjectWithTag("SpeedometerMask").GetComponent<RectTransform>();
        playerRb = Info.getPlayer().GetComponent<Player>().GetComponent<Rigidbody>();
        originalSize = mask.rect.size;
    }
	
	// Update is called once per frame
	public void Update () {
        float speedometerRatio = Helper.Remap(playerRb.velocity.z, -1500, -30000, 0, 1.0f);

        float noise = 1 - Random.Range(-0.025f, 0.025f);
        mask.sizeDelta = new Vector2(originalSize.x * speedometerRatio * noise, mask.sizeDelta.y);
	}
}
