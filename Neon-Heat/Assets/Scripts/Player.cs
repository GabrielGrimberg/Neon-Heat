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
	float accelData = 0;

    public Text hSpeed;
    public Text tSpeed;
    public Text tScore;

    DoubleTap aDoubleTap;
    DoubleTap bDoubleTap;
    Shield shield;
    BlurEffect blur;

	public GameObject sendRef;
	UDPSend udpSendRef;

    GameObject[] wheelSmoke;

    Speedometer speedometer;

    bool dropDown = false;

    int score = 0;

    // Use this for initialization
    void Start () {
        shield = new Shield();
        blur = new BlurEffect();
        rb = GetComponent<Rigidbody>();
        speedometer = new Speedometer();

		udpSendRef = sendRef.GetComponent<UDPSend> ();

        aDoubleTap = new DoubleTap(KeyCode.A, 0.2f, DashLeft);
        bDoubleTap = new DoubleTap(KeyCode.D, 0.2f, DashRight);

        wheelSmoke = GameObject.FindGameObjectsWithTag("WheelSmoke");
    }
	
	// Update is called once per frame
    //Split update into smaller methods later.
	void Update () {
        aDoubleTap.Update();
        bDoubleTap.Update();
        shield.Update();
        blur.Update();
        speedometer.Update();

        if (dropDown) {
            DropDown();
            return;
        }

        score += (int)(Mathf.Abs(rb.velocity.z) * Time.deltaTime);

        //Get data from UDPRecieve script for X value of acceleromter on app
        float.TryParse (UDPReceive.lastReceivedUDPPacket, out accelData);
		//Debug.Log(accelData);

        //Accelerate the car initally.
        if(rb.velocity.z > -4000) {
            rb.AddRelativeForce(Vector3.forward * -1000);
        }

        //Accelerate car continuously;
        rb.AddRelativeForce(Vector3.forward * -100);

		//Calculate bonus horizontal speed.
		bonusHorizontalSpeed = CalculateBonusHorizontalSpeed();

        //Lerp back the boost horizontal speed to 0.
        boostHorizontalSpeed = Mathf.Lerp(boostHorizontalSpeed, 0, Time.deltaTime / 0.1f);

        //Input keyboard code.
		if (accelData > 0.12) {
			currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, (accelData*10000) * -1 + -bonusHorizontalSpeed + -boostHorizontalSpeed, Time.deltaTime / 0.2f);
        }

		if (accelData < - 0.12) {
			currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, (accelData*10000) * -1 + bonusHorizontalSpeed + boostHorizontalSpeed, Time.deltaTime / 0.2f);
        }

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
        if (transform.position.x < -3800) {
            transform.position = new Vector3(-3800, transform.position.y, transform.position.z);
            currentHorizontalSpeed = 0;
        }

        if (transform.position.x > -630) {
            transform.position = new Vector3(-630, transform.position.y, transform.position.z);
            currentHorizontalSpeed = 0;
        }

        //tilt
        //Mb turn more the more ur speed is
        Vector3 rotation = transform.localRotation.eulerAngles;
        float maxYTilt = Helper.Remap(rb.velocity.z, minimumSpeed, -16000, 20, 40);
        maxYTilt = Mathf.Clamp(maxYTilt, -40, 40);
        rotation.y = Helper.Remap(currentHorizontalSpeed, (-maxHorizontalSpeed + -bonusHorizontalSpeed + -boostHorizontalSpeed) * -1, -maxHorizontalSpeed + -bonusHorizontalSpeed + -boostHorizontalSpeed, -maxYTilt, maxYTilt);
        rotation.z = Helper.Remap(currentHorizontalSpeed, (-maxHorizontalSpeed + -bonusHorizontalSpeed + -boostHorizontalSpeed) * -1, -maxHorizontalSpeed + -bonusHorizontalSpeed + -boostHorizontalSpeed, -7, 7);
        transform.localRotation = Quaternion.Euler(rotation);

        rb.velocity = new Vector3(currentHorizontalSpeed, rb.velocity.y, rb.velocity.z);

		//Set speed to minimum if less than minimum.
        if (rb.velocity.z > minimumSpeed) { //Remember, the minimum speed is negative.
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, minimumSpeed);
        }

        //Wheel smoke
        if (Mathf.Abs(currentHorizontalSpeed) > 1000) {
            foreach (GameObject wheel in wheelSmoke) {
                wheel.GetComponent<ParticleSystem>().Play();
            }
        } else {
            foreach (GameObject wheel in wheelSmoke) {
                wheel.GetComponent<ParticleSystem>().Stop();
            }
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
			tSpeed.text = (rb.velocity.z / 500.0f).ToString() + "m/s";
		}

        if (tScore != null) {
            tScore.text = "Score: " + (score / 500).ToString() + "m";
        }
	}

    void OnTriggerEnter(Collider other) {
        //Debug.Log("Triggered with " + other.tag);

		udpSendRef.sendString ("crashed");

        if ((other.tag == "Pillar" || other.tag == "Rocket") && !shield.onOff) {
            rb.AddRelativeForce(Vector3.forward * 2000, ForceMode.VelocityChange);
            Info.getCameraShake().AddShake(40, 0.2f);



            ICollidable collidable = other.GetComponent<ICollidable>();
            if (collidable != null) {
                collidable.Collide();
            }

			Info.getDistortImageEffects().Quake();
            ObstacleExplosion.Explode(other.transform.position);
        }

        if (other.tag == "SpeedUpRing") {
            rb.AddRelativeForce(Vector3.forward * -2000, ForceMode.VelocityChange);
            //Info.getCameraShake().AddShake(40, 0.2f);
            //Info.getDistortImageEffects().Quake();
            blur.Quake();
        }

        if (other.tag == "Portal") {
            Info.getFollowPlayer().GoStraight(rb.velocity.z);

            Vector3 teleportPosition = other.GetComponent<Portal>().connectionPortal.transform.position;
            teleportPosition.y = transform.position.y;
            transform.position = teleportPosition;
        }

        if (other.tag == "CityCrack") {
            StartDropDown();
        }
    }

    void DashRight() {
        boostHorizontalSpeed = 15000;
    }

    void DashLeft() {
        boostHorizontalSpeed = 15000;
    }

    void DieExplode() {
        Destroy(gameObject);
    }

    void DropDown() {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime / 0.15f);
        rb.transform.localPosition += Vector3.down * 1000 * Time.deltaTime;
    }

    void StartDropDown() {
        dropDown = true;
        Info.getFollowPlayer().birdsEyeView = true;

        Invoke("DieExplode", 2.0f);
    }
}
