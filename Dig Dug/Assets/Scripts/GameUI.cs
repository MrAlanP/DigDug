using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameUI : MonoBehaviour {

	public JoinGame Joining;
	public GameObject UI;
	public GameObject Player1;
	public GameObject Player2;
	public GameObject Player3;
	public GameObject Player4;
	public GameObject Player5;
	public GameObject Player6;
	public GameObject Player7;
	public GameObject Player8;
	List<Player> players = new List<Player>();

	// Use this for initialization
	void Start () {
		UI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (Joining.timer == 0) {

			if (Joining.GetPlayers() == 1) {
				Player1.SetActive(true);
			}
			else if(Joining.GetPlayers() == 2){
				Player1.SetActive (true);
				Player2.SetActive (true);
			}
			else if(Joining.GetPlayers() == 3){
				Player1.SetActive (true);
				Player2.SetActive (true);
				Player3.SetActive (true);
			}
			else if(Joining.GetPlayers() == 4){
				Player1.SetActive (true);
				Player2.SetActive (true);
				Player3.SetActive (true);
				Player4.SetActive (true);
			}
			else if(Joining.GetPlayers() == 5){
				Player1.SetActive (true);
				Player2.SetActive (true);
				Player3.SetActive (true);
				Player4.SetActive (true);
				Player5.SetActive (true);
			}
			else if(Joining.GetPlayers() == 6){
				Player1.SetActive (true);
				Player2.SetActive (true);
				Player3.SetActive (true);
				Player4.SetActive (true);
				Player5.SetActive (true);
				Player6.SetActive (true);
			}
			else if(Joining.GetPlayers() == 7){
				Player1.SetActive (true);
				Player2.SetActive (true);
				Player3.SetActive (true);
				Player4.SetActive (true);
				Player5.SetActive (true);
				Player6.SetActive (true);
				Player7.SetActive (true);
			}
			else if(Joining.GetPlayers() == 8){
				Player1.SetActive (true);
				Player2.SetActive (true);
				Player3.SetActive (true);
				Player4.SetActive (true);
				Player5.SetActive (true);
				Player5.SetActive (true);
				Player6.SetActive (true);
				Player7.SetActive (true);
				Player8.SetActive (true);
			}
		}
	}
}
