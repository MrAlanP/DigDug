using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoinGame : MonoBehaviour {

	public GameObject ReadyTextPlayer1;
	public GameObject ReadyTextPlayer2;
	public GameObject ReadyTextPlayer3;
	public GameObject ReadyTextPlayer4;
	public GameObject ReadyTextPlayer5;
	public GameObject ReadyTextPlayer6;
	public GameObject ReadyTextPlayer7;
	public GameObject ReadyTextPlayer8;
	public List<int> PlayerList = new List<int>();

	// Use this for initialization
	void Start ()
    {
		//PlayerList.Add (ReadyTextPlayer1);
		Debug.Log("This list has " + PlayerList.Count + " numbers");
	}
	
	// Update is called once per frame
	void Update ()
    {
		bool PCReadyP1 = Input.GetKeyDown (KeyCode.Return);
		bool PCReadyP2 = Input.GetKeyDown (KeyCode.KeypadEnter);
		bool JoypadReadyP1 = Input.GetKeyDown (KeyCode.Joystick1Button4);
		bool JoypadReadyP2 = Input.GetKeyDown (KeyCode.Joystick1Button5);
		bool JoypadReadyP3 = Input.GetKeyDown (KeyCode.Joystick2Button4);
		bool JoypadReadyP4 = Input.GetKeyDown (KeyCode.Joystick2Button5);
		bool JoypadReadyP5 = Input.GetKeyDown (KeyCode.Joystick3Button4);
		bool JoypadReadyP6 = Input.GetKeyDown (KeyCode.Joystick3Button5);
		bool JoypadReadyP7 = Input.GetKeyDown (KeyCode.Joystick4Button4);
		bool JoypadReadyP8 = Input.GetKeyDown (KeyCode.Joystick4Button5);

		if (PCReadyP1) {
			ReadyTextPlayer1.SetActive (true);
			PlayerList.Add(1);
			Debug.Log("This list has " + PlayerList.Count + " numbers");
		} 
		else if (PCReadyP2) {
			ReadyTextPlayer2.SetActive (true);
			PlayerList.Add(2);
			Debug.Log("This list has " + PlayerList.Count + " numbers");
		}

		if (JoypadReadyP1) {
			ReadyTextPlayer1.SetActive (true);
			PlayerList.Add (1);
			Debug.Log ("This list has " + PlayerList.Count + " numbers");
		} 
		else if (JoypadReadyP2) {
			ReadyTextPlayer1.SetActive (true);
			PlayerList.Add (2);
			Debug.Log ("This list has " + PlayerList.Count + " numbers");
		}
	}
}
