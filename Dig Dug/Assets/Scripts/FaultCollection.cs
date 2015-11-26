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

}
