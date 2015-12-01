using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject playersParent;
	public WinScreen winScreen;

	List<Player> players = new List<Player>();
	Color[] playerColours = new Color[8]{Color.blue,Color.cyan,Color.gray,Color.green,Color.magenta,Color.red,Color.yellow,Color.black};
	string[] playerNames = new string[8]{"Blue","Cyan","Gray","Green","Magenta","Red","Yellow","Black"};

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			int nameIndex = 0;
			for(int i = 0; i<playerColours.Length; i++){
				if(playerColours[i]==players[0].GetColour()){
					nameIndex = i;
					break;
				}
			}
			winScreen.ShowWin(players[0], playerNames[nameIndex]);
		}
	}

	public void SpawnPlayers(List<int> playerIndexes){
		foreach (int index in playerIndexes) {
			GameObject newPlayer = Instantiate(playerPrefab);
			newPlayer.transform.SetParent(playersParent.transform);
			Player player = newPlayer.GetComponent<Player>();
			player.Initialise(index, this);
			players.Add(player);
			player.SetColour(playerColours[index-1]);
            
		}
	}

	public void KillPlayer(Player player){
		players.Remove (player);
		Destroy (player.gameObject);

		switch (players.Count) {
		case 0:
			winScreen.ShowDraw();
			break;
		case 1:
			int nameIndex = 0;
			for(int i = 0; i<playerColours.Length; i++){
				if(playerColours[i]==players[0].GetColour()){
					nameIndex = i;
					break;
				}
			}
			winScreen.ShowWin(players[0], playerNames[nameIndex]);
			break;
		}
	}
}
