using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]

public class MenuCountdownLabel : MonoBehaviour {

	public Text Countdown;
	public float TimeLeft = 10;
	public Rect Position = new Rect(0, 300, 200, 100);

	void Start(){
	}

	void Update(){
		TimeLeft -= Time.deltaTime;
	}

	void OnGUI() {
		if (TimeLeft > 0) {
			GUI.Label (new Rect (Position), "Time: " + TimeLeft);
		} 
		else {
			GUI.Label (new Rect (100,100,200,100), "Start!");
		}
	}
}
