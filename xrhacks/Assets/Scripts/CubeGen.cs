using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGen : MonoBehaviour {

	[SerializeField] private Transform objectTypeToSpawn;
	private IEnumerator coroutine;

	private const int cubeCap = 1000;
	private int cubesGenerated;


	// Use this for initialization
	void Start () {
		cubesGenerated = 0;
		coroutine = WaitAndSpawn(1.5f);
		StartCoroutine(coroutine);
	}

	private IEnumerator WaitAndSpawn(float waitTime){
		while (cubesGenerated < cubeCap)
		{
			yield return new WaitForSeconds(waitTime);

			int randomX = Random.Range(-20, 20);
			int randomY = Random.Range(0, 30);
			int randomZ = 40;

			Vector3 newPos = new Vector3(randomX, randomY, randomZ);

			Transform cube = Instantiate(objectTypeToSpawn, newPos, Quaternion.identity);
			cube.GetComponent<MovingBox>().target = Camera.main.transform;

			cubesGenerated++;
		}
	}
}
