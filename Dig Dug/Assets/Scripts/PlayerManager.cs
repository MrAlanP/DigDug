using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject playersParent;
	public WinScreen winScreen;
	TileManager tileManager;

	public List<Player> players = new List<Player>();
	Color[] playerColours = new Color[8]{Color.blue,Color.cyan,Color.gray,Color.green,Color.magenta,Color.red,Color.yellow,Color.black};
	string[] playerNames = new string[8]{"Blue","Cyan","Gray","Green","Magenta","Red","Yellow","Black"};
	IntVector2[] spawnPoints = new IntVector2[8]{new IntVector2(5,30), new IntVector2(64,6), new IntVector2(64,30), new IntVector2(5,6),
												new IntVector2(21,30), new IntVector2(48,6), new IntVector2(48,30), new IntVector2(21,6)};

	// Use this for initialization
	void Awake () {
		tileManager = GetComponent<TileManager> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SpawnPlayers(List<int> playerIndexes){
		int spawnIndex = 0;
		foreach (int index in playerIndexes) {
			GameObject newPlayer = Instantiate(playerPrefab);
			newPlayer.transform.SetParent(playersParent.transform);
			newPlayer.transform.localPosition = tileManager.GetTile(spawnPoints[spawnIndex]).transform.localPosition;
			Player player = newPlayer.GetComponent<Player>();
			player.Initialise(index, this);
			players.Add(player);
			player.SetColour(playerColours[index-1]);
			spawnIndex++;
		}
	}

	public void KillPlayer(Player player){
		players.Remove (player);
		Destroy (player.gameObject);

		if (players.Count <= 1) {
			if(players[0].falling){
				winScreen.ShowDraw();
			}
			else{
				int nameIndex = 0;
				for(int i = 0; i<playerColours.Length; i++){
					if(playerColours[i]==players[0].GetColour()){
						nameIndex = i;
						break;
					}
				}
				players[0].DisableMovement();
				winScreen.ShowWin(players[0], playerNames[nameIndex]);
			}

		}
			

	}
}
