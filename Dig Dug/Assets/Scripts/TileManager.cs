using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

	public GameObject tilesParent;
	public GameObject tilePrefab;

	CrackManager crackManager;

	Tile[,] tiles;

	const float TILE_SIZE = 0.32f;

	Vector2 GRID_SIZE = new Vector2(70,35);
	// Use this for initialization
	void Awake () {
		crackManager = GetComponent<CrackManager> ();

		//Create Tiles in grid
		tiles = new Tile[(int)GRID_SIZE.x, (int)GRID_SIZE.y];
		for(int y = 0; y<GRID_SIZE.y; y++){
			for(int x = 0; x<GRID_SIZE.x; x++){
				GameObject newTile = Instantiate(tilePrefab);
				newTile.transform.SetParent(tilesParent.transform);
				newTile.transform.localPosition = new Vector3(TILE_SIZE*x, TILE_SIZE*y, 0);
				newTile.name = "Tile_"+y+"-"+x;
				tiles[x,y] = newTile.GetComponent<Tile>();

				//If the sprite should be edge alt
				if(y==0){
					tiles[x,y].SetEdgeSprite();
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector2 GetCentrePoint(){
		return new Vector2 ((GRID_SIZE.x-1) * 0.5f, (GRID_SIZE.y-1) * 0.5f) * TILE_SIZE;
	}

	public void Earthquake(){

	}
}
