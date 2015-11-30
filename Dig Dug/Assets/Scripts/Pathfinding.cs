using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Pathfinding {

	//A* pathfinding for faults
	public static List<IntVector2> GetPath(IntVector2 start, IntVector2 goal, List<IntVector2> nodes){
		List<PathHeuristic> openList = new List<PathHeuristic> ();
		List<PathHeuristic> closedList = new List<PathHeuristic> ();

		PathHeuristic currentTile = new PathHeuristic(start);

		float startFuncTime = Time.realtimeSinceStartup;

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
			List<IntVector2> adjacentList = GetAdjacentNodes(currentTile.tileIndex, nodes);


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

		Debug.Log (Time.realtimeSinceStartup - startFuncTime);
		Debug.Log (closedList.Count);

		//Unable to find a path between the start-goal
		return null;
	}

	static List<IntVector2> GetAdjacentNodes(IntVector2 tileIndex, List<IntVector2> collections){
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

	static int GetParentCount(PathHeuristic _tile){

		PathHeuristic tile = _tile;
		int parentCount = 0;
		while (tile.parent!=null) {
			tile = tile.parent;
			parentCount++;
		}
		return parentCount;
	}
	
	//Pass in a list of a shapes coords for its circumference, get back a list of squares inside the shape
	public static List<IntVector2> GetContainedSquares(List<IntVector2> shapeCirc){
		List<IntVector2> containedSquares = new List<IntVector2> ();
		//Order list by height
		shapeCirc = shapeCirc.OrderBy (w => w.y).ToList ();

		int startIndex = 0;
		//For each row
		for (int i = shapeCirc[0].y; i<=shapeCirc[shapeCirc.Count-1].y; i++) {
			//Row List
			List<int> row = new List<int>();
			for(int j = startIndex; j<shapeCirc.Count; j++){
				if(shapeCirc[j].y > i){
					startIndex = j;
					break;
				}
				else{
					row.Add(shapeCirc[j].x);
				}
			}
			row = row.OrderBy(w => w).ToList();
			//Now using this row, work out which squares should be marked as start edges, end edges and which to ignore
			List<int> startEdges = new List<int>();
			List<int> endEdges = new List<int>();
			for(int j = 0; j<row.Count; j++){
				//If not at last box
				if(j<row.Count-1){
					//If the box doesn't have another to the right
					if(!row.Contains(row[j]+1)){
						startEdges.Add(row[j]);
					}
				}
				//If not first box
				if(j>0){
					//If the box doesn't have another to the left
					if(!row.Contains(row[j]-1)){
						endEdges.Add(row[j]);
					}
				}
			}
			if(startEdges.Count>0){
				//Add all points between start and end edges
				for(int j= 0; j<endEdges.Count; j++){
					if(j<startEdges.Count){
						for(int k = startEdges[j]+1; k<endEdges[j]; k++){
							containedSquares.Add(new IntVector2(k, i));
						}
					}

				}
			}

		}


		return containedSquares;
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
