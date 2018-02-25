using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

	public float fireRate = .25f;
	public float weaponRange = 100f;

	public Transform gunEnd;

	private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

	private LineRenderer laserLine;

	private float nextFire;

	private IEnumerator ShotEffect()
	{
		laserLine.enabled = true;

		yield return shotDuration;

		laserLine.enabled = false;
	}

	// Use this for initialization
	void Start () {
		laserLine = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.2 && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			StartCoroutine(ShotEffect());

			Vector3 rayOrigin = gunEnd.transform.position;

			RaycastHit hit;

			laserLine.SetPosition(0, rayOrigin);

			if (Physics.Raycast(rayOrigin, gunEnd.transform.forward, out hit, weaponRange))
			{
				laserLine.SetPosition(1, hit.point);

				GameObject[] cubes = GameObject.FindGameObjectsWithTag("Destroyable");

				for (int i = 0; i < cubes.Length; ++i)
				{
					if (cubes[i] == hit.collider.gameObject)
					{
						Debug.Log("cube is hit.collider ob");
						GameObject.Find("Master").GetComponent<GameCubeMain>().cubesDestroyed += 1;
						Destroy(hit.collider.gameObject);
					}
				}
			}
			else
			{
				laserLine.SetPosition(1, rayOrigin + (gunEnd.transform.forward * weaponRange));
			}
		}
	}
}
