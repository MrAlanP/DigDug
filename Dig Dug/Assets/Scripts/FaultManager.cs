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

		IntVector2 faultIndex = fault.tileIndex;

		//The number of tiles the explosion expands the fault by
		int explodeRange = 5;

		//Do all 4 directions
		for(int j = 0; j<4; j++){
			//Repeat for range
			for(int i = 0; i<explodeRange; i++){
				float angle = Mathf.Deg2Rad*(90*(j%4));

				Vector2 direction = new Vector2(Mathf.Cos(angle),-Mathf.Sin(angle));

				Vector2 directionOffset = direction * (i+1);
				//Debug.Log(directionOffset);
				Tile tile = tileManager.GetTile(faultIndex + directionOffset);

				bool stopExplodingInDirection = tile==null;

				//If the tile is in range
				if(!stopExplodingInDirection){
					//Add a fault if one doesn't exist
					Fault newFault;
					if(tile.HasFault()){
						newFault = tile.GetFault();
						stopExplodingInDirection = true;
						//Check if fault collection is same or not
						if(newFault.faultCollectionRef.key != fault.faultCollectionRef.key){
							fault.faultCollectionRef.MergeCollection(newFault.faultCollectionRef);
							faultCollections.Remove(newFault.faultCollectionRef);
						}
						//Not explode point connection
						else if(i>0){
							Debug.Log("Collapse Land");
							fault.SetLinksToSelf();

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


						if(exitTile==null){
							stopExplodingInDirection = true;
							newFault.SetConnectsToWater();
						}
						else if(exitTile.HasCollapsed()){
							stopExplodingInDirection = true;
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
				else{
					fault.SetConnectsToWater();
				}

				if(stopExplodingInDirection){
					break;
				}
			}
		}

		tileManager.SetAdjacentWaterTiles ();
		//Collapse any water connections
		List<IntVector2> tilesToCollapse = fault.faultCollectionRef.GetTilesToCollapse (tileManager.adjacentToWaterTiles);
		if (tilesToCollapse.Count > 0) {
			foreach(IntVector2 tileIndex in tilesToCollapse){
				for(int i = mainFaults.Count-1; i>=0; i--){
					if(mainFaults[i].tileIndex == tileIndex){
						mainFaults.RemoveAt(i);
						break;
					}
				}
			}
			faultCollections.Remove(fault.faultCollectionRef);

			tileManager.CollapseTiles(tilesToCollapse);
		}

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
