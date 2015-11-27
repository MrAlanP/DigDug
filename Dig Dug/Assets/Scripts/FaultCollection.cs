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


	public List<Fault> GetFaultsToCollapse(List<Tile> waterTiles){
		List<Fault> faultsToCollapse = new List<Fault> ();


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



		//Collapse
		List<List<IntVector2>> paths = new List<List<IntVector2>> ();
		//Water linked path
		if(waterConnectionIndexes.Count > 1){
			for (int i = 0; i<waterConnectionIndexes.Count-1; i++) {
				paths.Add(GetPath (waterConnectionIndexes [i], waterConnectionIndexes [i+1]));
				paths.Add(GetPath (waterConnectionIndexes [i], waterConnectionIndexes [i+1],waterTiles));
				//Add to list
				Debug.Log(paths[paths.Count-1].Count);
			}
		}
		for (int i = 0; i<landConnectionIndexes.Count; i++) {
			paths.Add(GetPath(landConnectionIndexes[i], landConnectionIndexes[i]));
		}



		foreach (List<IntVector2> path in paths) {
			for(int i = 0; i<path.Count; i++){
				for(int j = faults.Count-1; j>=0; j--){
					if(faults[j].tileIndex == path[i]){
						faultsToCollapse.Add(faults[j]);
						faults.RemoveAt(j);
						break;
					}
				}
			}
		}

		return faultsToCollapse;
	}

	//A* pathfinding for faults
	List<IntVector2> GetPath(IntVector2 start, IntVector2 goal, List<Tile> tiles = null){
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
			List<IntVector2> adjacentList;
			if(tiles!=null){
				adjacentList = GetAdjacentTiles(currentTile.tileIndex, tiles);
			}
			else{
				adjacentList = GetAdjacentFaults(currentTile.tileIndex);
			}

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

	List<IntVector2> GetAdjacentFaults(IntVector2 tileIndex){
		List<IntVector2> adjacents = new List<IntVector2> ();

		for(int i = 0; i<faults.Count; i++){
			if(faults[i].tileIndex.x == tileIndex.x){
				if(Mathf.Abs(faults[i].tileIndex.y - tileIndex.y)==1){
					adjacents.Add(faults[i].tileIndex);
				}
			}
			else if(faults[i].tileIndex.y == tileIndex.y){
				if(Mathf.Abs(faults[i].tileIndex.x - tileIndex.x)==1){
					adjacents.Add(faults[i].tileIndex);
				}
			}
		}
		return adjacents;
	}

	List<IntVector2> GetAdjacentTiles(IntVector2 tileIndex, List<Tile> tiles){
		List<IntVector2> adjacents = new List<IntVector2> ();
		
		for(int i = 0; i<tiles.Count; i++){
			if(tiles[i].tileIndex.x == tileIndex.x){
				if(Mathf.Abs(tiles[i].tileIndex.y - tileIndex.y)==1){
					adjacents.Add(tiles[i].tileIndex);
				}
			}
			else if(tiles[i].tileIndex.y == tileIndex.y){
				if(Mathf.Abs(tiles[i].tileIndex.x - tileIndex.x)==1){
					adjacents.Add(tiles[i].tileIndex);
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
