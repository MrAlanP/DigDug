using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class WinScreen : MonoBehaviour {

	public GameObject screen;
	GameCamera cam;
	public Game game;

	public Text playerWinText;
	public Text destroyedText;
	// Use this for initialization
	void Awake () {
		screen.SetActive (false);
		cam = game.gameCam;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowWin(Player winningPlayer, string name = ""){

		game.EndGame ();

		if (winningPlayer != null) {
			DOTween.To (() => cam.cameraPosition, x => cam.cameraPosition = x, 
			           new Vector2 (winningPlayer.transform.localPosition.x + 0.25f, winningPlayer.transform.localPosition.y), 
			           3.0f);
			
			
			cam.gameObject.GetComponent<Camera> ().DOOrthoSize (0.5f, 3.0f).OnComplete (() => {
				screen.SetActive (true);
			});

			playerWinText.text = name + " Wins";
			playerWinText.color = winningPlayer.GetColour ();
		} else {
			playerWinText.text = "Nobody Won!";
		}



		destroyedText.text = game.GetTotalDestroyedLand ().ToString();



	}

	public void ShowDraw(){
		ShowWin (null);
	}
}
