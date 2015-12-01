using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JoinGame : MonoBehaviour {

	public Game game;
	public Text countdownTimer;
	public GameObject[] readyTexts;
	bool[] playerReady = new bool[8];
    AudioSource source;

    bool countdownActive = false;


	public float timer;

	bool gameStarted = false;

	// Use this for initialization
	void Awake ()
    {
        source = gameObject.GetComponent<AudioSource>();
		timer = 5;
		//countdownTimer.gameObject.SetActive (false);
		for(int i = 0; i<playerReady.Length; i++){
			playerReady[i] = false;
		}
	}

	void Start(){
		source.Play ();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (gameStarted) {
			return;
		}
		bool PCReadyP1 = Input.GetKeyDown (KeyCode.Return);
		bool PCReadyP2 = Input.GetKeyDown (KeyCode.KeypadEnter);

		for (int i = 0; i<playerReady.Length; i++) {
			if(!playerReady[i]){
				bool readyButtonDown = Input.GetButton ("Player"+(i+1)+"Bumper");

				if(readyButtonDown){
					playerReady[i] = true;
					readyTexts[i].SetActive(true);
					//Set countdown to start if we have at least 1 player
					if(!countdownActive){
						countdownActive = true;
					}
				}


			}
		}

		UpdateCountdown ();
	}

	public void UpdateCountdown(){
		if(countdownActive){
			timer -= Time.deltaTime;
			countdownTimer.text = "Starting in: "+Mathf.RoundToInt(timer);
            source.volume = timer;
		}

		if(timer<=0){
			gameStarted = true;
			game.StartGame();
			source.Stop();
		}

	}

	public List<int> GetPlayers(){
		List<int> players = new List<int> ();
		for(int i = 0; i<playerReady.Length; i++){
			if(playerReady[i]){
				players.Add(i+1);
			}
		}

		return players;
	}

	public int GetPlayerCount(){
		return GetPlayers ().Count;
	}
}
