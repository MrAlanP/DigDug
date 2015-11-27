using UnityEngine;
using System.Collections;

public class bombTrack : MonoBehaviour 
{
    public GameObject explosion;
    public TileManager tileManager;
    bool exploded = false;
    bool throb = false;
    float active = 0;
    float throbCount = 0;
    Vector2 scale;
    float shrink;
    Vector2 startSize;
    Vector2 bombPos;
    void Start()
    {
       startSize = gameObject.transform.localScale;

    }
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(tileManager.GetCentrePoint().ToString());
       // Tile localTile = tileManager.GetTile(new Vector2(1,1));
		Tile closestTile = tileManager.GetClosestTile (new Vector2 (1.5f, 1.5f));


        Debug.Log(tileManager.GetCentrePoint().ToString());
        Tile localTile = tileManager.GetClosestTile(gameObject.transform.position);
        gameObject.transform.SetParent(localTile.transform);

             
        //get tile with nearest position, set bomb pos to tile pos. IF tile has crack (crackActiveBomb()) ELSE (groundActiveBomb())

        bombPos = new Vector2(closestTile.transform.position.x, closestTile.transform.position.y);   
        
      
      
       // gameObject.transform.SetParent(localTile.transform);
        //if (localTile.HasFault())
        //{
        //    crackActiveBomb();
        //}
        //else
        //{
           groundActiveBomb();
        //}
    }
    void crackActiveBomb()
    {
        shrink += Time.deltaTime;
        gameObject.transform.localScale = Vector2.Lerp(startSize, Vector2.zero, shrink);


    }
    void groundActiveBomb()
    {
        active += Time.deltaTime;
        //makes the bomb throb when dropped
        if (active >= 0.3f)
        {
            throb = !throb;

            if (!throb)
            {
                transform.localScale = new Vector2(1.1f, 1.1f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                throbCount++;
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
            active = 0;
            
        }
        //creates explosion
        if (throbCount==6&&!exploded)
        {
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            exploded = true;
        }

        if (throbCount==7)
        {
            Destroy(gameObject);
        }
    }
}
