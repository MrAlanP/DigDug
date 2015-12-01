using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Game : MonoBehaviour {

	public JoinGame joinMenu;
	public GameCamera gameCam;
	TileManager tileManager;
	PlayerManager playerManager;
    AudioSource source;

    float minTime = 15;
    float maxTime = 25;

    // current time
    private float time = 0;

    // time to do an earthquake
    private float earthquakeTime;

	bool gameActive = false;


	// Use this for initialization
	void Awake () {


		tileManager = GetComponent<TileManager> ();
		playerManager = GetComponent<PlayerManager> ();
        source = gameObject.GetComponent<AudioSource>();


		//Centre camera on middle of tiles
		Vector2 cameraPos = tileManager.GetCentrePoint ();
		gameCam.SetPosition(new Vector3 (cameraPos.x, cameraPos.y, gameCam.transform.localPosition.z));
	}

	void Start(){
        //Initialize the timer for earthquakes
        SetRandomTime();
        time = minTime;
		LoadLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			Earthquake();
		}

		// Random earthquake stuff follows
		if (gameActive) {
            //play music
            playMusic();
			// count up
			time += Time.deltaTime;
			
			// check if its the right time to call an earthquake
			if (time >= earthquakeTime)
			{
				Earthquake();
				SetRandomTime();
				time = 0;
			}
		}
	}

    void playMusic()
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
        
    }


    void SetRandomTime()
    {
        earthquakeTime = Random.Range(minTime, maxTime);
    }

	void Earthquake(){
		tileManager.Earthquake ();
		gameCam.Earthquake ();
	}

	public void StartGame(){
		joinMenu.gameObject.SetActive (false);
		

		gameActive = true;

		List<int> players = joinMenu.GetPlayers ();
		playerManager.SpawnPlayers (players);
	}

	public void EndGame(){
		gameActive = false;

        source.Stop();
	}

	public int GetTotalDestroyedLand(){
		return tileManager.destroyedCount * 5;
	}

	void LoadLevel(){
		StartCoroutine(tileManager.LoadLevel ());
	}
}


