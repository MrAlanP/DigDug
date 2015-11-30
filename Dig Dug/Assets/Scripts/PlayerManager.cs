using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject playersParent;
   //public Sprite playerSprite1;
   //public Sprite playerSprite2;
   //public Sprite playerSprite3;
   //public Sprite playerSprite4;
   //public Sprite playerSprite5;
   //public Sprite playerSprite6;
   //public Sprite playerSprite7;
   //public Sprite playerSprite8;
	List<Player> players = new List<Player>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnPlayers(List<int> playerIndexes){
		foreach (int index in playerIndexes) {
			GameObject newPlayer = Instantiate(playerPrefab);
			newPlayer.transform.SetParent(playersParent.transform);
			Player player = newPlayer.GetComponent<Player>();
			player.SetIndex(index);
			players.Add(player);
            newPlayer.GetComponent<SpriteRenderer>().color = playerColour(index-1);
            
		}
	}
    Color playerColour(int selector)
    {
        Color colour = new Color();
        if (selector==0)
        {
            colour = Color.blue;
        }
        if (selector==1)
        {
            colour = Color.cyan;
        }
        if (selector==2)
        {
            colour = Color.gray;
        }
        if (selector==3)
        {
            colour = Color.green;
        }
        if (selector==4)
        {
            colour = Color.magenta;
        }
        if (selector==5)
        {
            colour = Color.red;
        }
        if (selector==6)
        {
            colour = Color.yellow;
        }
        if (selector==7)
        {
            colour = Color.black;
        }
        return colour;

    }
}
