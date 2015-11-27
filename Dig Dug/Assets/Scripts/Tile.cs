using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Sprite grassEdgeSprite;

	SpriteRenderer spriteRend;

	public IntVector2 tileIndex = new IntVector2();


	Fault fault;
	// Use this for initialization
	void Awake () {
		fault = null;
		spriteRend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetEdgeSprite(){
		spriteRend.sprite = grassEdgeSprite;
	}

	public void AddFault(Fault _fault){
		fault = _fault;
	}

	public bool HasFault(){
		return fault != null;
	}

	public Fault GetFault(){
		return fault;
	}
}
