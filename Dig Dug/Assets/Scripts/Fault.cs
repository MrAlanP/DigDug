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

	public Sprite[] connectionSprites = new Sprite[5];

	Vector2[] connectionDirections = new Vector2[4];


	FaultType faultType = FaultType.Connection;
	SpriteRenderer spriteRend;

	Tile tile;

	public IntVector2 tileIndex;

	//Used to determine whether we should collapse
	bool connectsToWater = false;
	bool linksToSelf = false;



	// Use this for initialization
	void Awake () {
		spriteRend = GetComponent<SpriteRenderer> ();
		if (faultType == FaultType.Connection) {
			mainFaultSprite.enabled = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FadeIn(float time){
		mainFaultSprite.color = new Color (1, 1, 1, 0);
		spriteRend.color = new Color (1, 1, 1, 0);
		DOTween.To(()=> spriteRend.color, x=> spriteRend.color = x, new Color(1,1,1,1), time).SetOptions(true);
		DOTween.To(()=> mainFaultSprite.color, x=> mainFaultSprite.color = x, new Color(1,1,1,1), time).SetOptions(true);

	}

	//Sets the fault as a connection type
	public void SetAsMain(){
		if (faultType != FaultType.Main) {
			faultType = FaultType.Main;
			mainFaultSprite.enabled = true;
		}

	}

	public bool CanBeSetMain(){

		if (faultType == FaultType.Main) {
			return false;
		}
		return true;
	}

	public void ExplodeFault(){
		AddConnectionDirection (new Vector2 (1, 0));
		AddConnectionDirection (new Vector2 (0, 1));
		AddConnectionDirection (new Vector2 (-1, 0));
		AddConnectionDirection (new Vector2 (0, -1));
		UpdateSprite ();
		mainFaultSprite.enabled = false;
	}


	public void AddConnectionDirection(Vector2 dir){

		for(int i = 0; i<connectionDirections.Length; i++){
			if(connectionDirections[i]==Vector2.zero){

				connectionDirections[i] = dir;

				break;
			}
			//If we already have this direction, return and don't update sprite
			else if(connectionDirections[i]==dir){
				return;
			}
		}



	}

	public void UpdateSprite(){
		//Add all directions so we can then average to work out rotation
		Vector2 totalDirection = new Vector2 ();
		int directionCount = 0;


		for(int i = 0; i<connectionDirections.Length; i++){
			if(connectionDirections[i]!=Vector2.zero){
				directionCount++;
				totalDirection += connectionDirections[i];
			}
		}

		//No directions means we shouldn't set sprite
		if (directionCount == 0) {
			return;
		}

		//Work out the average direction - for certain connection cases
		Vector2 averageDirection = totalDirection / directionCount;


		int spriteIndex = directionCount-1;
		//Check if sprite should be corner
		if (directionCount == 2) {
			if(totalDirection!=Vector2.zero){
				spriteIndex = 4;
			}
		}


		 spriteRend.sprite = connectionSprites [spriteIndex];

		float rotation = 0;

		//Sort out rotation
		switch (spriteIndex) {
		//t junction
		case 2:{
			rotation = Mathf.Atan2 (averageDirection.x, -averageDirection.y);
			break;
		}
		//corner
		case 4:{
			rotation = Mathf.Atan2 (averageDirection.x, -averageDirection.y);
			rotation -= Mathf.Deg2Rad*45;
			break;
		}
		default:{
			//Use first connection to determine direction (1 or 2 connections)
			rotation = Mathf.Atan2 (connectionDirections[0].x, -connectionDirections[0].y);
			break;
		}
		}

		rotation *= Mathf.Rad2Deg;
		transform.localEulerAngles = new Vector3 (0, 0, rotation);

	}

	public void SetTile(Tile _tile){
		tile = _tile;
		transform.SetParent(tile.transform);
		transform.localPosition = Vector3.zero;
		tileIndex = tile.tileIndex;
	}

	public Tile GetTile(){
		return tile;
	}

	public void SetConnectsToWater(bool setter = true){
		connectsToWater = setter;
	}

	public bool GetConnectsToWater(){

		return connectsToWater;
	}

	public void SetLinksToSelf(bool setter = true){
		linksToSelf = setter;
	}
	
	public bool GetLinksToSelf(){
		return linksToSelf;
	}

	public void Collapse(){
		gameObject.SetActive (false);
		//Destroy (gameObject);
	}

	public bool HasConnection(IntVector2 direction){
		for (int i = 0; i<4; i++) {
			if(connectionDirections[i]!=Vector2.zero){
				if(connectionDirections[i].x==direction.x && connectionDirections[i].y==direction.y){
					return true;
				}
			}
		}

		return false;
	}

	public void PrintConnections(){
		for (int i = 0; i<4; i++) {
			if(connectionDirections[i]!=Vector2.zero){
				Debug.Log(connectionDirections[i]);
			}
		}
	}

	public Vector2[] GetConnections(){
		return connectionDirections;
	}

	public bool CanExplode(){
		return (faultType == FaultType.Main && mainFaultSprite.enabled);
	}



}
