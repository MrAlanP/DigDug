using UnityEngine;
using System.Collections;

public class WaterBackground : MonoBehaviour {

	float phaseTime = 0;
	float phaseTarget = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		phaseTime += Time.deltaTime;
		if (phaseTime >= phaseTarget) {
			phaseTime = 0;
			transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 1);
		}
	}
}
