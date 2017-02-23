using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//Player class
public class Player : MonoBehaviour {
    const float minimumSpeed = -1500;
    const float maxHorizontalSpeed = 5000;

    Rigidbody rb;

    //Put all this horizontal speed in its own class.
    float currentHorizontalSpeed = 0;
    //float maxHorizontalSpeed = 5000;
    float bonusHorizontalSpeed = 0;
    float boostHorizontalSpeed = 0;

    public Text hSpeed;
    public Text tSpeed;

    DoubleTap aDoubleTap;
    DoubleTap bDoubleTap;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        aDoubleTap = new DoubleTap(KeyCode.A, 0.2f, DashLeft);
        bDoubleTap = new DoubleTap(KeyCode.D, 0.2f, DashRight);
    }
	
	// Update is called once per frame
    //Split update into smaller methods later.
	void Update () {
        aDoubleTap.Update();
        bDoubleTap.Update();

        //Accelerate the car initally.
        if(rb.velocity.z > -10000) {
            rb.AddRelativeForce(Vector3.forward * -1000);
        }

        //Accelerate car continuously;
        rb.AddRelativeForce(Vector3.forward * -100);

		//Calculate bonus horizontal speed.
		bonusHorizontalSpeed = CalculateBonusHorizontalSpeed();

        //Lerp back the boost horizontal speed to 0.
        boostHorizontalSpeed = Mathf.Lerp(boostHorizontalSpeed, 0, Time.deltaTime / 0.1f);

        //Input keyboard code.
        if (Input.GetKey(KeyCode.D)) {
            currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, -maxHorizontalSpeed + -bonusHorizontalSpeed + -boostHorizontalSpeed, Time.deltaTime / 0.2f);
        }

        if (Input.GetKey(KeyCode.A)) {
            currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, maxHorizontalSpeed + bonusHorizontalSpeed + boostHorizontalSpeed, Time.deltaTime / 0.2f);
        }

		//If none of the buttons are pressed, lerp to 0 on horizontal speed.
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, 0, Time.deltaTime / 0.1f);
        }


		//Check lane boundaries
        if (transform.position.x < -3400) {
            transform.position = new Vector3(-3400, transform.position.y, transform.position.z);
            currentHorizontalSpeed = 0;
        }

        if (transform.position.x > -1070) {
            transform.position = new Vector3(-1070, transform.position.y, transform.position.z);
            currentHorizontalSpeed = 0;
        }

        rb.velocity = new Vector3(currentHorizontalSpeed, rb.velocity.y, rb.velocity.z);

		//Set speed to minimum if less than minimum.
        if (rb.velocity.z > minimumSpeed) { //Remember, the minimum speed is negative.
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, minimumSpeed);
        }

		_UpdateText();
    }

	float CalculateBonusHorizontalSpeed() { //Change this to take in 2 ranges and a multiplier later.
		float tVel = Mathf.Abs(rb.velocity.z);
		Mathf.Clamp(tVel, 8000, 15000);
		return Helper.Remap(tVel, 8000, 15000, 0, 5000);
	}

	void _UpdateText() {
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


            ICollidable collidable = other.GetComponent<ICollidable>();
            if (collidable != null) {
                collidable.Collide();
            }

            Info.getDistortImageEffects().offsetColor = 0.2f;
        }
    }

    void DashRight() {
        boostHorizontalSpeed = 15000;
    }

    void DashLeft() {
        boostHorizontalSpeed = 15000;
    }
}
