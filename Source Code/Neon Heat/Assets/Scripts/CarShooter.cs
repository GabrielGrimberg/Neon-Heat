using UnityEngine;
using System.Collections;

public class CarShooter : MonoBehaviour {
	public GameObject BulletPOS;
	public GameObject Bullet;
	public float bSpeed; //Bullet speed.
	public GameObject bulletObject;//What we're going to hit

	void Update () {
        /*
		if (Input.GetMouseButtonDown(0)) {//If left mouse button clicked.

			GameObject storeBullet;//Reference for bullet we are going to create.

			//What object to instansiate, from where and what rotation to observe.
			storeBullet = Instantiate(BulletPOS,BulletPOS.transform.position,BulletPOS.transform.rotation) as GameObject;

			//Rotation correction.
			//storeBullet.transform.Rotate(Vector3.forward * -1);


			Rigidbody bullet;
			bullet = storeBullet.GetComponent<Rigidbody>();

			bullet.AddForce(Vector3.back * bSpeed, ForceMode.VelocityChange);

			Destroy(storeBullet, 5.0f); //Delete bullet after X seconds. 5 here.

			print("Working");
		}
		*/
	}

	void OnTriggerEnter(Collider otherObject) {

		if(otherObject.gameObject.tag == "Bullet") {

			print("Bullet is crashing, working");

			Destroy(this.gameObject);
			//Function to destroy other object.
		}
	}
		
}