using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Fault : MonoBehaviour {

	//Whether we can stick a bomb in the fault or not
	public enum FaultType{
		Main,
		Connection
	}

	FaultType faultType;
	SpriteRenderer spriteRend;
	Tile tile;


	// Use this for initialization
	void Awake () {
		faultType = FaultType.Main;
		spriteRend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FadeIn(){
		transform.localScale = Vector3.zero;

		DOTween.To (() => transform.localScale, x => transform.localScale = x, new Vector3 (1, 1, 1), 1);
	}

	//Sets the fault as a connection type
	public void SetAsConnection(){
		faultType = FaultType.Connection;
		spriteRend.enabled = false;
	}

	public void ExplodeFault(){

	}

	public void SetTile(Tile _tile){
		tile = _tile;
		transform.SetParent(tile.transform);
		transform.localPosition = Vector3.zero;
	}


}
