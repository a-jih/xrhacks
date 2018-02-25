using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour {

	public float weaponRange = 50f;

	public Transform gunEnd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 lineOrigin = gunEnd.transform.position;

		Debug.DrawRay(lineOrigin, gunEnd.transform.forward * weaponRange, Color.green);
	}
}
