using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

	public GameObject tilesParent;
	public GameObject tilePrefab;
	public ParticleSystem collapseParticles;
	public Texture2D level;

	FaultManager faultManager;

	Tile[,] tiles;

	const float TILE_SIZE = 0.32f;

	Vector2 GRID_SIZE = new Vector2(70,35);

	[HideInInspector]
	public List<Tile> waterTiles = new List<Tile>();
	[HideInInspector]
	public List<Tile> adjacentWaterTiles = new List<Tile>();
	// Use this for initialization
	void Awake () {
		GRID_SIZE = new Vector2 (level.width, level.height);
		faultManager = GetComponent<FaultManager> ();
		tiles = new Tile[(int)GRID_SIZE.x, (int)GRID_SIZE.y];
	}

	public void LoadLevel(){
		Color[] pixels = level.GetPixels();
		
		//Reads in an image, doesnt currently do Alans tile edge stuff
		for(int y = 0; y < level.height; y++){
			for(int x = 0; x < level.width; x++){
				
				Color color = pixels[(y*level.width)+x];
				
				GameObject newTile = Instantiate(tilePrefab);
				newTile.transform.SetParent(tilesParent.transform);
				newTile.transform.localPosition = new Vector3(TILE_SIZE*x, TILE_SIZE*y, 0);
				newTile.name = "Tile_"+x+"-"+y;
				tiles[x,y] = newTile.GetComponent<Tile>();
				tiles[x,y].tileIndex = new IntVector2(x,y);
				
				if(color.r > 0.1){
					tiles[x,y].Collapse();
				}

				//If the sprite should be edge alt sprite
//				if(y==0){
//					tiles[x,y].SetEdgeSprite();
//				}
			}
		}
		SetAdjacentWaterTiles ();
		SetWaterTiles ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector2 GetCentrePoint(){
		return new Vector2 ((GRID_SIZE.x-1) * 0.5f, (GRID_SIZE.y-1) * 0.5f) * TILE_SIZE;
	}

	public void Earthquake(){
		int faultCount = 4;
		Tile[] tilesToAddFaults = new Tile [faultCount];



		for (int i = 0; i<faultCount; i++) {
			Vector2 tileIndex = new Vector2(Random.Range(0,(int)GRID_SIZE.x-1), Random.Range(0,(int)GRID_SIZE.y-1));
			tilesToAddFaults[i] = tiles[(int)tileIndex.x, (int)tileIndex.y];
			if(tilesToAddFaults[i].HasCollapsed() || tilesToAddFaults[i].HasFault()){
				tilesToAddFaults[i] = null;
			}

		}
		faultManager.CreateCracks (tilesToAddFaults);

	}

	public Tile GetTile(IntVector2 tileIndex){
		//If out of range
		if (tileIndex.x < 0 || tileIndex.y < 0 || tileIndex.x >= GRID_SIZE.x || tileIndex.y >= GRID_SIZE.y) {
			return null;
		}
		return tiles[tileIndex.x, tileIndex.y];
	}

	public Tile GetClosestTile(Vector2 position){
		position /= TILE_SIZE;
		position = new Vector2 (Mathf.RoundToInt (position.x), Mathf.RoundToInt (position.y));
		position.x = Mathf.Clamp(position.x, 0, GRID_SIZE.x-1);
		position.y = Mathf.Clamp(position.y, 0, GRID_SIZE.y-1);

		return GetTile (new IntVector2((int)position.x,(int)position.y));
	}

	public void CollapseTiles(List<IntVector2> tileIndices){
		foreach (IntVector2 tileIndex in tileIndices) {
			if(!tiles[tileIndex.x, tileIndex.y].HasCollapsed()){
				tiles[tileIndex.x, tileIndex.y].Collapse();
				collapseParticles.transform.localPosition = tiles[tileIndex.x, tileIndex.y].transform.localPosition;
				collapseParticles.Emit(32);
			}

		}
	}

	public void SetWaterTiles(){
		waterTiles.Clear ();
		int test = 0;
		for(int y = 0; y<GRID_SIZE.y; y++){
			for(int x = 0; x<GRID_SIZE.x; x++){
				Tile tile = GetTile(new IntVector2(x,y));
				if(tile.HasCollapsed()){
					test++;
					for(int i = 0; i<4; i++){
						float angle = Mathf.Deg2Rad*(90*(i%4));
						Vector2 direction = new Vector2(Mathf.Cos(angle),-Mathf.Sin(angle));
						Tile adjacent = GetTile(new IntVector2(x+(int)direction.x, y+(int)direction.y));
						if(adjacent!=null){
							if(!adjacent.HasCollapsed()){
								waterTiles.Add(tile);
								break;
							}
						}
					}
					waterTiles.Add(tile);


				}
			}
		}
	}

	public void SetAdjacentWaterTiles(){
		adjacentWaterTiles.Clear ();
		for(int y = 0; y<GRID_SIZE.y; y++){
			for(int x = 0; x<GRID_SIZE.x; x++){
				for(int i = 0; i<4; i++){
					float angle = Mathf.Deg2Rad*(90*(i%4));
					
					Vector2 direction = new Vector2(Mathf.Cos(angle),-Mathf.Sin(angle));
					Tile adjacent = GetTile(new IntVector2(x+(int)direction.x, y+(int)direction.y));
					if(adjacent!=null){
						if(!adjacent.HasCollapsed()){
							continue;
						}
						
					}
					adjacentWaterTiles.Add(GetTile(new IntVector2(x,y)));
				}
			}
		}
	}

  
}
