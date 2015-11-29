using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Game : MonoBehaviour {

	public JoinGame joinMenu;
	public GameCamera gameCam;
	TileManager tileManager;
	PlayerManager playerManager;


	// Use this for initialization
	void Awake () {
		joinMenu.gameObject.SetActive (true);
		tileManager = GetComponent<TileManager> ();
		playerManager = GetComponent<PlayerManager> ();

		//Centre camera on middle of tiles
		Vector2 cameraPos = tileManager.GetCentrePoint ();
		gameCam.SetPosition(new Vector3 (cameraPos.x, cameraPos.y, gameCam.transform.localPosition.z));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			Earthquake();
		}
	}

	void Earthquake(){
		tileManager.Earthquake ();
		gameCam.Earthquake ();
	}

	public void StartGame(){
		joinMenu.gameObject.SetActive (false);



		LoadLevel ();

		List<int> players = joinMenu.GetPlayers ();
		playerManager.SpawnPlayers (players);
	}

	void LoadLevel(){
		tileManager.LoadLevel ();
	}
}


