using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject playersParent;
	public WinScreen winScreen;

	List<Player> players = new List<Player>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			winScreen.ShowWin(players[0]);
		}
	}

	public void SpawnPlayers(List<int> playerIndexes){
		foreach (int index in playerIndexes) {
			GameObject newPlayer = Instantiate(playerPrefab);
			newPlayer.transform.SetParent(playersParent.transform);
			Player player = newPlayer.GetComponent<Player>();
			player.Initialise(index, this);
			players.Add(player);
			player.SetColour(playerColour(index-1));
            
		}
	}

	public void KillPlayer(Player player){
		players.Remove (player);
		Destroy (player.gameObject);

		switch (players.Count) {
		case 0:
			break;
		case 1:
			break;
		}
	}
    Color playerColour(int selector)
    {
        Color colour = new Color();
        if (selector==0)
        {
            colour = Color.blue;
        }
        if (selector==1)
        {
            colour = Color.cyan;
        }
        if (selector==2)
        {
            colour = Color.gray;
        }
        if (selector==3)
        {
            colour = Color.green;
        }
        if (selector==4)
        {
            colour = Color.magenta;
        }
        if (selector==5)
        {
            colour = Color.red;
        }
        if (selector==6)
        {
            colour = Color.yellow;
        }
        if (selector==7)
        {
            colour = Color.black;
        }
        return colour;

    }
}
