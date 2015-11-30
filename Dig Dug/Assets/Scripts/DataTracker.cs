using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]

public class DataTracker : MonoBehaviour {

	public Color Background = Color.red;
	public Rect Position = new Rect (0, 0, 100, 100);

	// Use this for initialization
	void Start () {
	
	}

	private void OnGui () {
		GUI.backgroundColor = Background;
		GUI.Box (new Rect (Position), "Player 1");
	}
}
