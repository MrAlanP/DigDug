using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class WinScreen : MonoBehaviour {

	public GameObject screen;
	GameCamera cam;
	public Game game;
	// Use this for initialization
	void Awake () {
		screen.SetActive (false);
		cam = game.gameCam;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowWin(Player winningPlayer){

		//cam.transform.DOLocalMove (new Vector3(winningPlayer.transform.localPosition.x, winningPlayer.transform.localPosition.y, cam.transform.localPosition.z), 3.0f);
		Debug.Log (winningPlayer.transform.localPosition);
		DOTween.To(()=> cam.cameraPosition, x=> cam.cameraPosition = x, 
		           new Vector2(winningPlayer.transform.localPosition.x + 0.25f, winningPlayer.transform.localPosition.y), 
		           3.0f);


		cam.gameObject.GetComponent<Camera>().DOOrthoSize (0.5f, 3.0f).OnComplete(()=>{
			screen.SetActive (true);
		});

		//Get colours

	}

	public void ShowDraw(){

	}
}
