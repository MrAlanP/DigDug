using UnityEngine;
using System.Collections;
using DG.Tweening;

public class WinScreen : MonoBehaviour {

	public GameObject screen;
	Camera cam;
	// Use this for initialization
	void Awake () {
		screen.SetActive (false);
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowWin(Player winningPlayer){
		screen.SetActive (true);

		cam.DOOrthoSize (0.5f, 3.0f);
	}

	public void ShowDraw(){

	}
}
