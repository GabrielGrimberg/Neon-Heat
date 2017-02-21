using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    Rigidbody rb;
    float horizontalSpeed = 0;
    float maxSpeed = 5000;

    public Text hSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if(rb.velocity.z > -10000) {
            rb.AddRelativeForce(Vector3.forward * -1000);
        }

        if (Input.GetKey(KeyCode.D)) {
            horizontalSpeed = Mathf.Lerp(horizontalSpeed, -maxSpeed, Time.deltaTime / 0.2f);
        }

        if (Input.GetKey(KeyCode.A)) {
            horizontalSpeed = Mathf.Lerp(horizontalSpeed, maxSpeed, Time.deltaTime / 0.2f);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            horizontalSpeed = Mathf.Lerp(horizontalSpeed, 0, Time.deltaTime / 0.1f);
        }


        if (transform.position.x < -3400) {
            transform.position = new Vector3(-3400, transform.position.y, transform.position.z);
            horizontalSpeed = 0;
        }

        if (transform.position.x > -1070) {
            transform.position = new Vector3(-1070, transform.position.y, transform.position.z);
            horizontalSpeed = 0;
        }

        rb.velocity = new Vector3(horizontalSpeed, rb.velocity.y, rb.velocity.z);

        if (hSpeed != null) {
            hSpeed.text = "hSpeed: " + horizontalSpeed.ToString();
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Triggered with " + other.tag);
    }
}
