using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public Camera gameCam;
	TileManager tileManager;
	CrackManager crackManager;

	// Use this for initialization
	void Awake () {
		tileManager = GetComponent<TileManager> ();
		Vector2 cameraPos = tileManager.GetCentrePoint ();
		Debug.Log (cameraPos);
		gameCam.transform.localPosition = new Vector3 (cameraPos.x, cameraPos.y, gameCam.transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Earthquake(){

	}
}
