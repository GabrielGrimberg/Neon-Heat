using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Player class
public class Player : MonoBehaviour {
    const float minimumSpeed = -1500;
    const float maxHorizontalSpeed = 5000;

    Rigidbody rb;
    float currentHorizontalSpeed = 0;
    //float maxHorizontalSpeed = 5000;
    float bonusHorizontalSpeed = 0;

    public Text hSpeed;
    public Text tSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
    //Split update into smaller methods later.
	void Update () {
        //Accelerate the car initally.
        if(rb.velocity.z > -10000) {
            rb.AddRelativeForce(Vector3.forward * -1000);
        }

        //Accelerate car continuously;
        rb.AddRelativeForce(Vector3.forward * -100);

        //Calculate bonusHorizontalSpeed
        //Simplify this later
        float tVel = Mathf.Abs(rb.velocity.z);
        Mathf.Clamp(tVel, 8000, 15000);
        bonusHorizontalSpeed = Helper.Remap(tVel, 8000, 15000, 0, 5000);

        //Input keyboard code.
        if (Input.GetKey(KeyCode.D)) {
            currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, -maxHorizontalSpeed + -bonusHorizontalSpeed, Time.deltaTime / 0.2f);
        }

        if (Input.GetKey(KeyCode.A)) {
            currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, maxHorizontalSpeed + bonusHorizontalSpeed, Time.deltaTime / 0.2f);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, 0, Time.deltaTime / 0.1f);
        }


        if (transform.position.x < -3400) {
            transform.position = new Vector3(-3400, transform.position.y, transform.position.z);
            currentHorizontalSpeed = 0;
        }

        if (transform.position.x > -1070) {
            transform.position = new Vector3(-1070, transform.position.y, transform.position.z);
            currentHorizontalSpeed = 0;
        }

        rb.velocity = new Vector3(currentHorizontalSpeed, rb.velocity.y, rb.velocity.z);

        if (rb.velocity.z > minimumSpeed) { //Remember, the minimum speed is negative.
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, minimumSpeed);
        }

        if (hSpeed != null) {
            hSpeed.text = "hSpeed: " + currentHorizontalSpeed.ToString();
        }

        if (tSpeed != null) {
            tSpeed.text = "Speed: " + rb.velocity.z.ToString();
        }
    }

    void OnTriggerEnter(Collider other) {
        //Debug.Log("Triggered with " + other.tag);

        if (other.tag == "Pillar" || other.tag == "Rocket") {
            rb.AddRelativeForce(Vector3.forward * 2000, ForceMode.VelocityChange);
            Info.getCameraShake().AddShake(20, 0.2f);
        }
    }
}
