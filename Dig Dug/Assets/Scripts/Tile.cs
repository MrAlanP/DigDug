using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Sprite grassEdgeSprite;

	SpriteRenderer spriteRend;
	// Use this for initialization
	void Awake () {
		spriteRend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetEdgeSprite(){
		spriteRend.sprite = grassEdgeSprite;
	}
}
