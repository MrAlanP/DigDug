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
		//PlayerList.Add (ReadyTextPlayer1);
		Debug.Log("This list has " + PlayerList.Count + " numbers");
	}
	
	// Update is called once per frame
	void Update ()
    {
		bool PCReadyP1 = Input.GetKeyDown (KeyCode.Return);
		bool PCReadyP2 = Input.GetKeyDown (KeyCode.KeypadEnter);
		bool JoypadReadyP1 = Input.GetKeyDown (KeyCode.JoystickButton4);
		bool JoypadReadyP2 = Input.GetKeyDown (KeyCode.JoystickButton5);

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
