using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverStats : MonoBehaviour {

	public Text endgameText;
	//public GameCubeMain gm;

	// Use this for initialization
	void Start () {
		endgameText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		//gm = GameObject.FindWithTag("Master").GetComponent<GameCubeMain>() as GameCubeMain;
	}
}
