using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FaultManager : MonoBehaviour {
	
	List<Fault> mainFaults = new List<Fault>();
	List<FaultCollection> faultCollections = new List<FaultCollection> ();
	uint faultKey = 0;
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

			//Create a new fault collection for this item
			FaultCollection collection = CreateFaultCollection();
			collection.AddFault(newFault);
		}
	}

	public void ExplodeFault(Fault fault){
		mainFaults.Remove (fault);
		fault.ExplodeFault();

		Vector2 faultIndex = fault.tileIndex;

		//The number of tiles the explosion expands the fault by
		int explodeRange = 10;
		//Skip expanding into direction if we hit water
		bool[] skipDirection = new bool[4]{false,false,false,false};


		for(int i = 0; i<explodeRange; i++){
			//Do all 4 directions
			for(int j = 0; j<4; j++){

				if(skipDirection[j]){
					continue;
				}

				float angle = Mathf.Deg2Rad*(90*(j%4));
				Vector2 direction = new Vector2(Mathf.Cos(angle),-Mathf.Sin(angle));

				Vector2 directionOffset = direction * (i+1);
				Tile tile = tileManager.GetTile(faultIndex + directionOffset);

				//If the tile is in range
				if(tile!=null){
					//Add a fault if one doesn't exist
					Fault newFault;
					if(tile.HasFault()){
						newFault = tile.GetFault();

						//Check if fault collection is same or not
						if(newFault.faultCollectionRef.key != fault.faultCollectionRef.key){
							fault.faultCollectionRef.MergeCollection(newFault.faultCollectionRef);
							faultCollections.Remove(newFault.faultCollectionRef);
						}
						else{
	
						}
					}
					else{
						//Create a new fault
						newFault = AddFault(tile);
						fault.faultCollectionRef.AddFault(newFault);
					}


					//Add entry connection
					newFault.AddConnectionDirection(direction);
					if(i<explodeRange-1){
						//If not at end of explode range, add exit direction too
						newFault.AddConnectionDirection(-direction);
						//Check to see if this connects to ocean
						Tile exitTile = tileManager.GetTile(tile.tileIndex + direction);

						if(exitTile==null){//TODO add water tile check
							skipDirection[j] = true;
							newFault.SetConnectsToWater();
						}
					}
					//Set fault type as insertable
					else{
						SetFaultAsMain(newFault);
					}
					//Update rotation based on connections
					newFault.UpdateSprite();
				}
			}
		}

		//Collapse any water connections
		//fault.faultCollectionRef.HasMultipleWaterConnectors ();

		

	}

	void SetFaultAsMain(Fault fault){
		if (fault.CanBeSetMain ()) {
			fault.SetAsMain ();
			mainFaults.Add (fault);
		} 

	}

	Fault AddFault(Tile tile){
		GameObject newFault = Instantiate (faultPrefab);
		Fault fault = newFault.GetComponent<Fault>();
		tile.AddFault(fault);
		fault.SetTile(tile);
		return fault;
	}

	FaultCollection CreateFaultCollection(){
		FaultCollection newCollection = new FaultCollection ();
		faultCollections.Add (newCollection);
		newCollection.key = faultKey;
		faultKey++;

		return newCollection;
	}
}
