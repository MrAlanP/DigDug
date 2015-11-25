using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Fault : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FadeIn(){
		transform.localScale = Vector3.zero;

		DOTween.To (() => transform.localScale, x => transform.localScale = x, new Vector3 (1, 1, 1), 1);
	}
}
