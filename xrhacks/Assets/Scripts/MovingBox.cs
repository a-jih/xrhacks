﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour {

	[SerializeField] public Transform target;
	[SerializeField] private float speed;

	private Vector3 direction;

	public bool grabbed;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		direction = Vector3.Normalize(target.position - transform.position);		
	}

	// Update is called once per frame
	void Update () {
		if (!grabbed)
		{
			direction = Vector3.Normalize(target.position - transform.position);		
			float step = speed * Time.deltaTime;
			transform.position += direction * step;
		}
	}
}
