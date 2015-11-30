using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameUI : MonoBehaviour {

	public JoinGame Joining;
	public Image Player1;
	public Image Player2;
	public Image Player3;
	public Image Player4;
	public Image Player5;
	public Image Player6;
	public Image Player7;
	public Image Player8;
	List<Player> players = new List<Player>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

/*		if (Joining.UpdateCountdown = 0) {

			if (Joining.GetPlayers() = 1) {
				Player1.IsActive (true);
			}
			else if(Joining.GetPlayers = 2){
				Player1.IsActive (true);
				Player2.IsActive (true);
			}
			else if(Joining.GetPlayers = 3){
				Player1.IsActive (true);
				Player2.IsActive (true);
				Player3.IsActive (true);
			}
			else if(Joining.GetPlayers = 4){
				Player1.IsActive (true);
				Player2.IsActive (true);
				Player3.IsActive (true);
				Player4.IsActive (true);
			}
			else if(Joining.GetPlayers = 5){
				Player1.IsActive (true);
				Player2.IsActive (true);
				Player3.IsActive (true);
				Player4.IsActive (true);
				Player5.IsActive (true);
			}
			else if(Joining.GetPlayers = 6){
				Player1.SetActive ();
				Player2.SetActive ();
				Player3.SetActive ();
				Player4.SetActive ();
				Player5.SetActive ();
				Player6.SetActive ();
			}
			else if(Joining.GetPlayers = 7){
				Player1.SetActive ();
				Player2.SetActive ();
				Player3.SetActive ();
				Player4.SetActive ();
				Player5.SetActive ();
				Player6.SetActive ();
				Player7.SetActive ();
			}
			else if(Joining.GetPlayers = 8){
				Player1.SetActive ();
				Player2.SetActive ();
				Player3.SetActive ();
				Player4.SetActive ();
				Player5.SetActive ();
				Player6.SetActive ();
				Player7.SetActive ();
				Player8.SetActive ();
			}
		}*/
	}
}
