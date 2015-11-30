using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Game : MonoBehaviour {

	public JoinGame joinMenu;
	public GameCamera gameCam;
	TileManager tileManager;
	PlayerManager playerManager;

    public float minTime = 2;
    public float maxTime = 5;

    // current time
    private float time;

    // time to do an earthquake
    private float earthquakeTime;


	// Use this for initialization
	void Awake () {


		tileManager = GetComponent<TileManager> ();
		playerManager = GetComponent<PlayerManager> ();



		//Centre camera on middle of tiles
		Vector2 cameraPos = tileManager.GetCentrePoint ();
		gameCam.SetPosition(new Vector3 (cameraPos.x, cameraPos.y, gameCam.transform.localPosition.z));
	}

	void Start(){
		if (joinMenu != null) {
			joinMenu.gameObject.SetActive (true);
		} else {
			LoadLevel();
		}

        //Initialize the timer for earthquakes
        SetRandomTime();
        time = minTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			Earthquake();
		}
	}

    // Random earthquake stuff follows

    void FixedUpdate()
    {
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
		

		LoadLevel ();

		List<int> players = joinMenu.GetPlayers ();
		playerManager.SpawnPlayers (players);
	}

	void LoadLevel(){
		tileManager.LoadLevel ();
	}
}


