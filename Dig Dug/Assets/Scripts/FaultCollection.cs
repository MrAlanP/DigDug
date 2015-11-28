using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FaultCollection {

	public List<Fault> faults = new List<Fault>();

	public uint key = 0;


	public FaultCollection(){
	}

	public void AddFaults(List<Fault> faultsToAdd){
		for(int i = 0; i<faultsToAdd.Count; i++){
			AddFault(faultsToAdd[i]);
		}
	}

	public void AddFault(Fault fault){
		faults.Add (fault);
		fault.faultCollectionRef = this;
	}


	public void MergeCollection(FaultCollection otherCollection){
		for(int i = 0; i < otherCollection.faults.Count; i++){
			AddFault(otherCollection.faults[i]);
		}
	}


	public List<IntVector2> GetTilesToCollapse(List<Tile> waterTiles){
		List<IntVector2> tilesToCollapse = new List<IntVector2> ();


		//Get water connected indexes
		List<IntVector2> waterConnectionIndexes = new List<IntVector2> ();
		//Land connected indexes
		List<IntVector2> landConnectionIndexes = new List<IntVector2> ();

		for(int i = 0; i<faults.Count; i++){
			if(faults[i].GetConnectsToWater()){
				waterConnectionIndexes.Add(faults[i].tileIndex);
			}
			else if(faults[i].GetLinksToSelf()){
				landConnectionIndexes.Add(faults[i].tileIndex);
			}
		}

		//Make an intvector2 list of water tiles
		List<IntVector2> adjacentWaterTiles = new List<IntVector2> ();
		foreach (Tile tile in waterTiles) {
			adjacentWaterTiles.Add(tile.tileIndex);
		}

		//Make an intvector2 list of faults
		List<IntVector2> faultIndices = new List<IntVector2> ();
		foreach (Fault fault in faults) {
			faultIndices.Add(fault.tileIndex);
		}


		//Collapse
		List<List<IntVector2>> paths = new List<List<IntVector2>> ();
		//Water linked path
		if(waterConnectionIndexes.Count > 1){
			for (int i = 0; i<waterConnectionIndexes.Count-1; i++) {
				List<IntVector2> path1 = GetPath (waterConnectionIndexes [i], waterConnectionIndexes [i+1], faultIndices);
				List<IntVector2> path2 =  GetPath (waterConnectionIndexes [i], waterConnectionIndexes [i+1],adjacentWaterTiles);
				path1.AddRange(path2);
				//Add to list
				paths.Add(path1);
			}
		}
		for (int i = 0; i<landConnectionIndexes.Count; i++) {
			if(faultIndices.Contains(landConnectionIndexes[i])){
				faultIndices.Remove(landConnectionIndexes[i]);
			}
			//Iterate through adjacent starting positions to find how the land connection connects
			for(int j = 0; j<4; j++){
				float startAngle = Mathf.Deg2Rad*(90*(j%4));
				float endAngle = Mathf.Deg2Rad*(90*((j+1)%4));
				IntVector2 start = new IntVector2((int)Mathf.Cos(startAngle),(int)-Mathf.Sin(startAngle)) + landConnectionIndexes[i];
				IntVector2 end = new IntVector2((int)Mathf.Cos(endAngle),(int)-Mathf.Sin(endAngle)) + landConnectionIndexes[i];
				List<IntVector2> path = GetPath(start, end, faultIndices);
				if(path!=null){
					//Add start
					path.Add(landConnectionIndexes[i]);
					paths.Add(path);
				}
			}
		}



		foreach (List<IntVector2> path in paths) {
			for(int i = 0; i<path.Count; i++){
				tilesToCollapse.Add(path[i]);
				for(int j = faults.Count-1; j>=0; j--){
					if(faults[j].tileIndex == path[i]){
						faults.RemoveAt(j);
						break;
					}
				}
			}
		}

		return tilesToCollapse;
	}

	//A* pathfinding for faults
	List<IntVector2> GetPath(IntVector2 start, IntVector2 goal, List<IntVector2> tiles){
		List<PathHeuristic> openList = new List<PathHeuristic> ();
		List<PathHeuristic> closedList = new List<PathHeuristic> ();

		PathHeuristic currentTile = new PathHeuristic(start);

		do {
			//Get the best fault using heuristics
			if(openList.Count>0){
				PathHeuristic lowestFTile = null;
				foreach(PathHeuristic tile in openList){
					if(lowestFTile==null){
						lowestFTile = tile;
					}
					else if(tile.score<lowestFTile.score){
						lowestFTile = tile;
					}
					if(lowestFTile.tileIndex==goal){
						lowestFTile = tile;
						break;
					}
				}
				currentTile = lowestFTile;
			}

			//Put the currentTile in closedList and remove from open
			closedList.Add(currentTile);
			openList.Remove(currentTile);

			//If the current is at the end
			if(currentTile.tileIndex == goal){
				List<IntVector2> pathList = new List<IntVector2>();

				while(currentTile.parent!=null){
					//Add parent to the pathlist
					pathList.Add(currentTile.tileIndex);
					currentTile = currentTile.parent;
				}
				pathList.Add(start);
				pathList.Reverse();
				return pathList;
			}

			//Set adjacent list up
			List<IntVector2> adjacentList = GetAdjacentVectors(currentTile.tileIndex, tiles);


			foreach(IntVector2 adjacent in adjacentList){
				//If adjacent is in closed list, skip
				bool adjacentInClosed = false;
				foreach(PathHeuristic tile in closedList){
					if(tile.tileIndex == adjacent){
						adjacentInClosed = true;
					}
				}

				if(adjacentInClosed){
					continue;
				}

				//If not in open list, add it
				bool adjacentInOpen = false;
				foreach(PathHeuristic tile in openList){
					if(tile.tileIndex == adjacent){
						adjacentInOpen = true;
					}
				}
				//Add to open list
				if(!adjacentInOpen){
					PathHeuristic newTile = new PathHeuristic(adjacent, currentTile);
					//Score
					newTile.SetScore((int)(IntVector2.Distance(adjacent,goal)*10));
					openList.Add(newTile);
					   
				}
			}
		} while(openList.Count>0);

		//Unable to find a path between the start-goal
		return null;
	}

	List<IntVector2> GetAdjacentVectors(IntVector2 tileIndex, List<IntVector2> collections){
		List<IntVector2> adjacents = new List<IntVector2> ();
		
		for(int i = 0; i<collections.Count; i++){
			if(collections[i].x == tileIndex.x){
				if(Mathf.Abs(collections[i].y - tileIndex.y)==1){
					adjacents.Add(collections[i]);
				}
			}
			else if(collections[i].y == tileIndex.y){
				if(Mathf.Abs(collections[i].x - tileIndex.x)==1){
					adjacents.Add(collections[i]);
				}
			}
		}
		return adjacents;
	}


	int GetParentCount(PathHeuristic _tile){

		PathHeuristic tile = _tile;
		int parentCount = 0;
		while (tile.parent!=null) {
			tile = tile.parent;
			parentCount++;
		}
		return parentCount;
	}


	class PathHeuristic{
		public PathHeuristic parent;
		public IntVector2 tileIndex;
		public int score;

		public PathHeuristic(IntVector2 _tileIndex, PathHeuristic _parent = null){
			tileIndex = _tileIndex;
			parent = _parent;
		}

		public void SetScore(int _score){
			score = _score;
		}
	}

}
