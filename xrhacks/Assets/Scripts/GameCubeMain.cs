using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCubeMain : MonoBehaviour {

	public int cubesDestroyed = 0;

	//public bool gameOver = false;
	private IEnumerator coroutine;
	public Text gameOverText;

	public float speed;

	private bool gameStarted = false;

	// Use this for initialization
	void Start () {
		coroutine = GameStart();
		StartCoroutine(coroutine);
	}

	IEnumerator GameStart()
	{
		gameOverText.text = "Don't let them near your magic carpet!\nLeft hand to man the guns, and left index finger to shoot.\nMiddle forward to move forward, middle back to move back.\nRight to rotate.";
		GameObject.Find("CubeGenerator").GetComponent<CubeGen>().enabled = false; 
		yield return new WaitForSeconds(10);
		GameObject.Find("CubeGenerator").GetComponent<CubeGen>().enabled = true; 
		gameOverText.text = "";
		gameStarted = true;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] cubes = GameObject.FindGameObjectsWithTag("Destroyable");
		if(gameStarted == true && cubesDestroyed > 0){
			if(cubesDestroyed == 1){
				gameOverText.text = cubesDestroyed + " cube destroyed!";
			} else {
				gameOverText.text = cubesDestroyed + " cubes destroyed!";
			}
		}

		for (int i = 0; i < cubes.Length; ++i)
		{
			if (Vector3.Distance(cubes[i].GetComponent<Transform>().transform.position, Camera.main.transform.position) <= 2.0f)
			{
				//gameOver = true;

				gameOverText.text = "Game Over. Cubes destroyed: " + cubesDestroyed;
				gameOverText.fontSize = 14;
				Time.timeScale = 0f;
			}
		}

		speed += Time.deltaTime * 0.2f;
	}
}
