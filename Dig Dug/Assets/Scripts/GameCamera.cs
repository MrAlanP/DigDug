using UnityEngine;
using System.Collections;
using DG.Tweening; //DOTween

public class GameCamera : MonoBehaviour {

	Vector2 cameraOffset = new Vector2();
	Vector2 cameraPosition = new Vector2();
	// Use this for initialization
	void Awake () {
		DOTween.Init ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate(){
		//Allows us to update the camera position while shaking, by just changing offset
		transform.localPosition = new Vector3 (cameraPosition.x + cameraOffset.x, cameraPosition.y + cameraOffset.y, transform.localPosition.z);
	}

	public void SetPosition(Vector2 pos){
		cameraPosition = pos;
		transform.localPosition = new Vector3 (pos.x, pos.y, transform.localPosition.z);
	}

	public void Earthquake(){
		cameraOffset = Vector2.zero;
		DOTween.Shake (() => cameraOffset, x => cameraOffset = x, 1.0f, 0.5f, 100, 10);

	}
}
