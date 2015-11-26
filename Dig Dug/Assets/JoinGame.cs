using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoinGame : MonoBehaviour {

	public GameObject ReadyTextPlayer1;
	public GameObject ReadyTextPlayer2;
	public List<int> PlayerList = new List<int>();

	// Use this for initialization
	void Start ()
    {
		PlayerList.Add (ReadyTextPlayer1);

	}
	
	// Update is called once per frame
	void Update ()
    {
	




		bool PCReadyP1 = Input.GetKeyDown (KeyCode.Return);
		bool PCReadyP2 = Input.GetKeyDown (KeyCode.KeypadEnter);
		bool JoypadReadyTest = Input.GetKeyDown (KeyCode.JoystickButton4);

		if (PCReadyP1) {
			ReadyTextPlayer1.SetActive (true);
		} 
		else if (JoypadReadyTest) {
			ReadyTextPlayer1.SetActive (true);
		} 
		else if (PCReadyP2) {
			ReadyTextPlayer2.SetActive (true);
		}
	}
}
