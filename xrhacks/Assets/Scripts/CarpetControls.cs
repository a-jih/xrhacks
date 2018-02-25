using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetControls : MonoBehaviour {

	public float speed = 0.0f;
	public float rotSpeed = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		OVRInput.Update();	
		checkInput();
	}	
	void checkInput() {
		
		if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.2) {
			
			//float throttle = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
			Vector3 vel = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);

			speed = vel.z/10; 
			if (speed < 0) {
				speed = 0;
			} else if (speed > 10) {
				speed = 10;
			}
			//moveShip(new Vector3(0.0f, 0.0f, 1.0f), speed); 
		}

		if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.2) {
			rotateShip(OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch), rotSpeed);
		}
		//Vector2 stickRotation = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
		//stickRotateShip(stickRotation, rotSpeed);
		moveShip(transform.forward, speed); 
	}

	void moveShip(Vector3 direction, float magnitude) {
		transform.position += direction * magnitude;
	}

	void rotateShip(Quaternion rotation, float magnitude) {
		Vector3 vel_rotation = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.LTouch);
		float rotX = vel_rotation.x/magnitude;
		float rotY = vel_rotation.y/magnitude;
		float rotZ = vel_rotation.z/magnitude;
		if (Mathf.Abs(vel_rotation.x) < 1) {
			rotX = 0;
		}
		if (Mathf.Abs(vel_rotation.y) < 1) {
			rotY = 0;
		}
		if (Mathf.Abs(vel_rotation.z) < 1) {
			rotZ = 0;
		}
		Matrix4x4 localToWorld = transform.localToWorldMatrix;
		//Quaternion.Euler
		//transform.localRotation = Quaternion.Euler(new Vector3(0.1f, 0, 0)) * transform.localRotation;
		//transform.localRotation = transform.localRotation * Quaternion.AngleAxis(0.1f, transform.up);
		transform.Rotate(new Vector3(rotX, rotY, rotZ));
	}
	void stickRotateShip(Vector2 thumbstick, float speed) {
		Quaternion xRot = new Quaternion(1.0f * thumbstick[1] * speed, 0.0f, 0.0f, 1.0f);
		Quaternion yRot = new Quaternion(0.0f, 1.0f * thumbstick[0] * speed *-1, 0.0f, 1.0f);
		transform.localRotation = transform.localRotation * new Quaternion(thumbstick[1], thumbstick[0], 0.0f, 1.0f);
	}
}
