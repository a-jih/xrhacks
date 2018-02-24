using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGen : MonoBehaviour {

	[SerializeField] private Transform objectTypeToSpawn;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		coroutine = WaitAndSpawn(3.0f);
		StartCoroutine(coroutine);
	}

	private IEnumerator WaitAndSpawn(float waitTime){
		while (true)
		{
			yield return new WaitForSeconds(waitTime);

			int randomX = Random.Range(-10, 10);
			int randomY = Random.Range(0, 5);
			int randomZ = 5;

			Vector3 newPos = new Vector3(randomX, randomY, randomZ);

			Transform cube = Instantiate(objectTypeToSpawn, newPos, Quaternion.identity);
			cube.GetComponent<MovingBox>().target = Camera.main.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
