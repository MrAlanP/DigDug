using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Fault : MonoBehaviour {

	public SpriteRenderer mainFaultSprite;

	//Whether we can stick a bomb in the fault or not
	public enum FaultType{
		Main,
		Connection
	}

	public Sprite[] connectionSprites = new Sprite[4];

	Vector2[] connectionDirections = new Vector2[4];


	FaultType faultType;
	SpriteRenderer spriteRend;

	Tile tile;

	uint faultKey  = 0;


	// Use this for initialization
	void Awake () {
		faultType = FaultType.Connection;
		spriteRend = GetComponent<SpriteRenderer> ();
		mainFaultSprite.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FadeIn(){
		mainFaultSprite.transform.localScale = Vector3.zero;
		DOTween.To (() => mainFaultSprite.transform.localScale, x => mainFaultSprite.transform.localScale = x, new Vector3 (1, 1, 1), 1);
	}

	//Sets the fault as a connection type
	public void SetAsMain(){
		faultType = FaultType.Main;
		mainFaultSprite.enabled = true;
		FadeIn ();
	}

	public void ExplodeFault(){
		faultType = FaultType.Connection;
		AddConnectionDirection (new Vector2 (1, 0));
		AddConnectionDirection (new Vector2 (0, 1));
		AddConnectionDirection (new Vector2 (-1, 0));
		AddConnectionDirection (new Vector2 (0, -1));
	}

	public void AddConnectionDirection(Vector2 dir){

		//Index for the connection - based on connection count
		int spriteIndex = 0;

		for(int i = 0; i<connectionDirections.Length; i++){
			if(connectionDirections[i]==Vector2.zero){

				connectionDirections[i] = dir;
				spriteIndex = i;
				break;
			}
			//If we already have this direction, return and don't update sprite
			else if(connectionDirections[i]==dir){
				return;
			}
		}

		spriteRend.sprite = connectionSprites [spriteIndex];

	}

	public void SetRotationFromDirections(){
		//Add all directions so we can then average to work out rotation
		Vector2 totalDirection = new Vector2 ();
		int directionCount = 0;
		for(int i = 0; i<connectionDirections.Length; i++){
			if(connectionDirections[i]!=Vector2.zero){
				directionCount++;
				totalDirection += connectionDirections[i];
			}
		}
		if (directionCount != 3) {
			float rotation = Mathf.Atan2 (connectionDirections[0].x, -connectionDirections[0].y);
			rotation *= Mathf.Rad2Deg;
			transform.localEulerAngles = new Vector3 (0, 0, rotation);
		}
	}

	public void SetTile(Tile _tile){
		tile = _tile;
		transform.SetParent(tile.transform);
		transform.localPosition = Vector3.zero;
	}

	public Tile GetTile(){
		return tile;
	}


}
