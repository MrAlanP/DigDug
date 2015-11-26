using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FaultManager : MonoBehaviour {

	List<Fault> faults = new List<Fault>();
	List<Fault> mainFaults = new List<Fault>();
	public GameObject faultPrefab;

	TileManager tileManager;
	// Use this for initialization
	void Awake () {
		tileManager = GetComponent<TileManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			ExplodeFault(mainFaults[Random.Range(0,mainFaults.Count)]);
		}
	}

	public void CreateCracks(Tile[] tilesToAddCracks){
		for (int i = 0; i<tilesToAddCracks.Length; i++) {
			Fault newFault = AddFault(tilesToAddCracks[i]);
			SetFaultAsMain(newFault);
		}
	}

	public void ExplodeFault(Fault fault){
		mainFaults.Remove (fault);
		fault.ExplodeFault();

		Vector2 faultIndex = fault.GetTile ().tileIndex;

		//The number of tiles the explosion expands the fault by
		int explodeRange = 3;


		for(int i = 0; i<explodeRange; i++){
			//Do all 4 directions
			for(int j = 0; j<4; j++){
				float angle = Mathf.Deg2Rad*(90*(j%4));
				Vector2 direction = new Vector2(Mathf.Cos(angle),-Mathf.Sin(angle));

				Vector2 directionOffset = direction * (i+1);
				Tile tile = tileManager.GetTile(faultIndex + directionOffset);

				//Add a fault if one doesn't exist
				Fault newFault;
				if(tile.HasFault()){
					newFault = tile.GetFault();
				}
				else{
					newFault = AddFault(tile);
				}

				//Add entry connection
				newFault.AddConnectionDirection(direction);
				if(i<explodeRange-1){
					//If not at end of explode range, add exit direction too
					newFault.AddConnectionDirection(-direction);
				}
				//Set fault type as insertable
				else{
					SetFaultAsMain(newFault);
				}

				newFault.SetRotationFromDirections();
			}
		}

	}

	void SetFaultAsMain(Fault fault){
		fault.SetAsMain();
		mainFaults.Add (fault);
	}

	Fault AddFault(Tile tile){
		GameObject newFault = Instantiate (faultPrefab);
		Fault fault = newFault.GetComponent<Fault>();
		tile.AddFault(fault);
		fault.SetTile(tile);
		faults.Add(fault);
		return fault;
	}
}
