using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {
    Animator anim;
    TileManager tileManager;
    Tile tile;
    AudioSource source;
    public AudioClip loudBang;
    public AudioClip softBang;
    float die = 0;
    bool playedClip = false;
    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("Game").GetComponent<TileManager>();
        source = GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (!playedClip)
        {
            tile = tileManager.GetClosestTile(gameObject.transform.position);
            if (tile.HasFault())
            {
                if (tile.GetFault().IsMain())
                {
                    source.PlayOneShot(softBang, 1);
                    playedClip = true;
                }
                else
                {
                    source.PlayOneShot(loudBang, 1);
                    playedClip = true;
                }
            }
            else
            {
                source.PlayOneShot(loudBang, 1);
                playedClip = true;
            }
           
        }
        die += Time.deltaTime;
        if (die >= 0.6f)
        {
            Destroy(gameObject);
        }
        if (gameObject.GetComponent<CircleCollider2D>().radius <= 0.05f)
        {
            gameObject.GetComponent<CircleCollider2D>().radius += 0.002f;
        }
    
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.GetComponent<Player>())
        {
			col.gameObject.GetComponent<Player>().dead = true;
        }
    }
}
