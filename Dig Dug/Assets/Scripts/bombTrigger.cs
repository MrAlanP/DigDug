using UnityEngine;
using System.Collections;

public class bombTrigger : MonoBehaviour {

    TileManager tileManager;
    public GameObject bomb;
    Tile testTile;
	// Use this for initialization
	void Start () {
        tileManager = GameObject.FindGameObjectWithTag("Game").GetComponent<TileManager>();
        testTile = tileManager.GetTile(new IntVector2(5,20));
	}
	
	// Update is called once per frame
	void Update () 
    {
	if (Input.GetKeyDown(KeyCode.B))
    {
        Instantiate(bomb, testTile.transform.position, new Quaternion(0, 0, 0, 0));
    }
	}
}
