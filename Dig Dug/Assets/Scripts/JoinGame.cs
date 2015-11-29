using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JoinGame : MonoBehaviour {

	public Text countdownTimer;
	public GameObject[] readyTexts;
	bool[] playerReady = new bool[8];


	float timer;

	// Use this for initialization
	void Awake ()
    {
		timer = 10;
		countdownTimer.gameObject.SetActive (false);
		for(int i = 0; i<playerReady.Length; i++){
			playerReady[i] = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
		bool PCReadyP1 = Input.GetKeyDown (KeyCode.Return);
		bool PCReadyP2 = Input.GetKeyDown (KeyCode.KeypadEnter);

		for (int i = 0; i<playerReady.Length; i++) {
			if(!playerReady[i]){
				bool readyButtonDown = Input.GetButton ("Player"+(i+1)+"Bumper");

				if(readyButtonDown){
					//Debug.Log(Input.GetAxis ("Player"+(i+1)+"Bumper"));
					playerReady[i] = true;
					readyTexts[i].SetActive(true);
					//Set countdown to start if we have at least 1 player
					if(!countdownTimer.gameObject.activeSelf){
						countdownTimer.gameObject.SetActive (true);
					}
				}


			}
		}

		UpdateCountdown ();
	}

	void UpdateCountdown(){
		if(countdownTimer.gameObject.activeSelf){
			timer -= Time.deltaTime;
			countdownTimer.text = "Starting in: "+Mathf.RoundToInt(timer);
		}

	}


}
