using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
	
	public OVRInput.Controller controller;
	public bool primary;

	public float grabRadius;
	public LayerMask grabMask;

	private GameObject grabbedObject;
	private bool grabbed;

	void GrabObject()
	{
		grabbed = true;

		RaycastHit[] hits;

		hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

		if (hits.Length > 0)
		{
			int closestHit = 0;

			for (int i = 0; i < hits.Length; i++)
			{
				if (hits[i].distance > hits[closestHit].distance) closestHit = i;
			}

			grabbedObject = hits[closestHit].transform.gameObject;
			grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
			grabbedObject.transform.position = transform.position;
			
			float offset = grabbedObject.GetComponent<Transform>().localScale.x/2;
			
			grabbedObject.transform.position += new Vector3(offset, 0, 0);
			grabbedObject.transform.parent = transform;

			if (grabbedObject.GetComponent<MovingBox>()) grabbedObject.GetComponent<MovingBox>().grabbed = true;
		}
	}

	void DropObject()
	{
		grabbed = false;

		if (grabbedObject != null)
		{
			grabbedObject.transform.parent = null;
			grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
			grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
			grabbedObject.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);

			if (grabbedObject.GetComponent<MovingBox>()) grabbedObject.GetComponent<MovingBox>().grabbed = false;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		OVRInput.Update();	
		if (primary)
		{
			if (!grabbed && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.1f) GrabObject();
			if (grabbed && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) < 0.1f) DropObject();
		}
		else
		{
			if (!grabbed && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.1f) GrabObject();
			if (grabbed && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) < 0.1f) DropObject();
		}
	}
}
