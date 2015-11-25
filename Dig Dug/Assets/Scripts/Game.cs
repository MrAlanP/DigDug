using UnityEngine;
using System.Collections;


public class Game : MonoBehaviour {

	public GameCamera gameCam;
	TileManager tileManager;


	// Use this for initialization
	void Awake () {

		tileManager = GetComponent<TileManager> ();


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
}
