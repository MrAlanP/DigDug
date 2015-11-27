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


	public List<Fault> GetFaultsToCollapse(){
		List<Fault> faultsToCollapse = new List<Fault> ();


		//Get water connected indexes
		List<IntVector2> waterConnectionIndexes = new List<IntVector2> ();

		for(int i = 0; i<faults.Count; i++){
			if(faults[i].GetConnectsToWater()){
				waterConnectionIndexes.Add(faults[i].tileIndex);
			}
		}
		
		if(waterConnectionIndexes.Count < 2){
			return faultsToCollapse;
		}

		//Collapse
		for (int i = 0; i<waterConnectionIndexes.Count-1; i++) {
			List<IntVector2> path = GetPath (waterConnectionIndexes [i], waterConnectionIndexes [i+1]);
			Debug.Log("Collapse Land - Water Connection");
		}

		return faultsToCollapse;

	}

	//A* pathfinding for faults
	List<IntVector2> GetPath(IntVector2 start, IntVector2 goal){
		List<FaultHeuristic> openList = new List<FaultHeuristic> ();
		List<FaultHeuristic> closedList = new List<FaultHeuristic> ();

		FaultHeuristic currentFault = new FaultHeuristic(start);

		do {
			//Get the best fault using heuristics
			if(openList.Count>0){
				FaultHeuristic lowestFFault = null;
				foreach(FaultHeuristic fault in openList){
					if(lowestFFault==null){
						lowestFFault = fault;
					}
					else if(fault.score<lowestFFault.score){
						lowestFFault = fault;
					}
					if(lowestFFault.tileIndex==goal){
						lowestFFault = fault;
						break;
					}
				}
				currentFault = lowestFFault;
			}

			//Put the currentTile in closedList and remove from open
			closedList.Add(currentFault);
			openList.Remove(currentFault);

			//If the current is at the end
			if(currentFault.tileIndex == goal){
				List<IntVector2> pathList = new List<IntVector2>();

				while(currentFault.parent!=null){
					//Add parent to the pathlist
					pathList.Add(currentFault.tileIndex);
					currentFault = currentFault.parent;
				}
				pathList.Add(start);
				pathList.Reverse();
				return pathList;
			}

			//Set adjacent list up
			List<IntVector2> adjacentList = GetAdjacentFaults(currentFault.tileIndex);
			foreach(IntVector2 adjacent in adjacentList){
				//If adjacent is in closed list, skip
				bool adjacentInClosed = false;
				foreach(FaultHeuristic fault in closedList){
					if(fault.tileIndex == adjacent){
						adjacentInClosed = true;
					}
				}
				if(adjacentInClosed){
					continue;
				}

				//If not in open list, add it
				bool adjacentInOpen = false;
				foreach(FaultHeuristic fault in openList){
					if(fault.tileIndex == adjacent){
						adjacentInOpen = true;
					}
				}
				//Add to open list
				if(!adjacentInOpen){
					FaultHeuristic newFault = new FaultHeuristic(adjacent, currentFault);
					//Score
					newFault.SetScore((int)(IntVector2.Distance(adjacent,goal)*10));
					openList.Add(newFault);
					   
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


	class FaultHeuristic{
		public FaultHeuristic parent;
		public IntVector2 tileIndex;
		public int score;

		public FaultHeuristic(IntVector2 _tileIndex, FaultHeuristic _parent = null){
			tileIndex = _tileIndex;
			parent = _parent;
		}

		public void SetScore(int _score){
			score = _score;
		}
	}

}
