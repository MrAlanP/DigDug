using UnityEngine;
using System.Collections;

public class bombTrigger : MonoBehaviour {


    public GameObject bomb;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	if (Input.GetKeyDown(KeyCode.B))
    {
        Instantiate(bomb, Vector2.zero, new Quaternion(0, 0, 0, 0));
    }
	}
}
