using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class FaultManager : MonoBehaviour {
	
	List<Fault> mainFaults = new List<Fault>();
	List<Fault> faults = new List<Fault> ();
	List<Fault> faultsToCheck = new List<Fault> ();
	
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
		//Collapse any tiles we should
		for (int i = 0; i<faultsToCheck.Count; i++) {
			
			List<IntVector2> tilesToCollapse = GetTilesToCollapse (faultsToCheck[i]);
			CollapseTiles (tilesToCollapse);

			faultsToCheck.RemoveAt(i);
		}
	}

	public void CreateCracks(Tile[] tilesToAddCracks){
		for (int i = 0; i<tilesToAddCracks.Length; i++) {
			if(tilesToAddCracks[i]!=null){
				Fault newFault = AddFaultToTile(tilesToAddCracks[i]);
				SetFaultAsMain(newFault);
			}


		}
	}

	public void ExplodeFault(Fault fault){
		mainFaults.Remove (fault);
		fault.ExplodeFault();



		IntVector2 faultIndex = fault.tileIndex;

		//The number of tiles the explosion expands the fault by
		int explodeRange = 3;
		//How long do we see faults fade in for
		float faultExpandTime = 0.3f;

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
						//Fault has connected to itself in a link, should explode
						if(i>0){
							int timer = 0;
							DOTween.To(()=>timer,x=>timer=x,0,faultExpandTime).OnComplete(()=>{
								faultsToCheck.Add(newFault);
							});

						}
					}
					else{
						//Create a new fault
						newFault = AddFaultToTile(tile);
						newFault.FadeIn(faultExpandTime);
					}

					faults.Add(newFault);

					//Add entry connection
					newFault.AddConnectionDirection(direction);
					if(i<explodeRange-1){
						//If not at end of explode range, add exit direction too
						newFault.AddConnectionDirection(-direction);
						//Check to see if this connects to ocean
						Tile exitTile = tileManager.GetTile(tile.tileIndex + direction);

						if(exitTile==null || exitTile.HasCollapsed()){
							stopExplodingInDirection = true;
							int timer = 0;
							DOTween.To(()=>timer,x=>timer=x,0,faultExpandTime).OnComplete(()=>{
								faultsToCheck.Add(newFault);
							});
						}
					}
					//Set fault type as insertable
					else{
						SetFaultAsMain(newFault);

					}
					//Update rotation based on connections
					if(!tile.HasCollapsed()){
						newFault.UpdateSprite();

					}

				}

				if(stopExplodingInDirection){
					break;
				}
			}
		}







	}


	void CollapseTiles(List<IntVector2> tilesToCollapse){

		if (tilesToCollapse.Count > 0) {
			foreach(IntVector2 tileIndex in tilesToCollapse){

				for(int i = mainFaults.Count-1; i>=0; i--){
					if(mainFaults[i].tileIndex == tileIndex){
						mainFaults.RemoveAt(i);
						break;
					}
				}
				for(int i = faults.Count-1; i>=0; i--){
					if(faults[i].tileIndex == tileIndex){
						faults.RemoveAt(i);
						break;
					}
				}
			}
			tileManager.CollapseTiles(tilesToCollapse);

			//Update water tiles
			tileManager.SetWaterTiles ();
			tileManager.SetAdjacentWaterTiles();
		}
	}

	void PrintPath(List<IntVector2> path){
		for (int i = 0; i<path.Count; i++) {
			Debug.Log(path[i].ToString());
		}
	}

	public List<IntVector2> GetTilesToCollapse(Fault faultToCheck){
		List<IntVector2> tilesToCollapse = new List<IntVector2> ();
		List<IntVector2> path = null;


		
		//List of nodes
		List<IntVector2> nodes = new List<IntVector2> ();
		for (int i = 0; i<tileManager.waterTiles.Count; i++) {
			nodes.Add(tileManager.waterTiles[i].tileIndex);
		}
		for (int i = 0; i<faults.Count; i++) {
			nodes.Add(faults[i].tileIndex);
		}

		IntVector2 faultIndex = faultToCheck.tileIndex;

		while (nodes.Contains(faultIndex)) {
			nodes.Remove(faultIndex);
		}

		for (int i = 0; i<2 || path!=null; i++) {
			//Should use an array here
			List<IntVector2> startToEnd = new List<IntVector2>();
			float angle = Mathf.Deg2Rad*(90*i);
			startToEnd.Add(faultIndex + new IntVector2((int)Mathf.Cos(angle),(int)-Mathf.Sin(angle)));
			startToEnd.Add(faultIndex - new IntVector2((int)Mathf.Cos(angle),(int)-Mathf.Sin(angle)));
			if(AreNodesValid(startToEnd)){
				path = Pathfinding.GetPath(startToEnd[0], startToEnd[1], nodes);
				if(path!=null){
					//Add start
					path.Add(faultIndex);
				}
			}

			if(path!=null){
				break;
			}
			//Right angles
			for(int j = 0; j<2; j++){
				startToEnd.Clear();
				float startAngle = Mathf.Deg2Rad*((90*(j%4))+(i*180));
				float endAngle = Mathf.Deg2Rad*((90*((j+1)%4))+i*180);

				startToEnd.Add(new IntVector2((int)Mathf.Cos(startAngle),(int)-Mathf.Sin(startAngle)) + faultIndex);
				startToEnd.Add(new IntVector2((int)Mathf.Cos(endAngle),(int)-Mathf.Sin(endAngle)) + faultIndex);
				if(AreNodesValid(startToEnd)){
					path = Pathfinding.GetPath(startToEnd[0], startToEnd[1], nodes);
					if(path!=null){
						//Add start
						path.Add(faultIndex);
						break;
					}
				}
			}
		}
		
		if(path!=null){
			List<IntVector2> containedTiles = Pathfinding.GetContainedSquares(path);
			path.AddRange(containedTiles);
			
			//Add all tiles
			for(int i = 0; i<path.Count; i++){
				tilesToCollapse.Add(path[i]);
			}
		}

		return tilesToCollapse;
	}

	bool AreNodesValid(List<IntVector2> nodes){

		for(int i = 0; i<nodes.Count; i++){
			Tile tile = tileManager.GetTile(nodes[i]);
			if(tile==null){
				return false;
			}
			//Not a fault or water
			if(!tile.HasCollapsed() && !tile.HasFault()){
				return false;
			}
		}

		return true;
	}
	
	void SetFaultAsMain(Fault fault){
		if (fault.CanBeSetMain ()) {
			fault.SetAsMain ();
			mainFaults.Add (fault);
		} 

	}

	Fault AddFaultToTile(Tile tile){
		GameObject newFault = Instantiate (faultPrefab);
		Fault fault = newFault.GetComponent<Fault>();
		tile.AddFault(fault);
		fault.SetTile(tile);
		faults.Add (fault);
		return fault;
	}

}
