using UnityEngine;
using System.Collections;

public class bombTrack : MonoBehaviour 
{
    public GameObject explosion;
    FaultManager faultManager;
    TileManager tileManager;
    Fault localFault;
    Tile localTile;
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
       tileManager = GameObject.FindGameObjectWithTag("Game").GetComponent<TileManager>();
       faultManager = GameObject.FindGameObjectWithTag("Game").GetComponent<FaultManager>();
       localTile = tileManager.GetClosestTile(gameObject.transform.position);
       gameObject.transform.position = localTile.transform.position;

    }
	// Update is called once per frame
	void Update ()
    {
        if (Vector2.Distance(localTile.transform.position, gameObject.transform.position)>1f)
        {
            localTile = tileManager.GetClosestTile(gameObject.transform.position);
            gameObject.transform.position = localTile.transform.position;
        }
       // gameObject.transform.SetParent(localTile.transform);
       
        if (localTile.HasFault())
        {
            crackActiveBomb();
        }
        else
        {
           groundActiveBomb();
        }
    }
    void crackActiveBomb()
    {
        gameObject.transform.SetParent(localTile.transform);
        shrink += Time.deltaTime;
        gameObject.transform.localScale = Vector2.Lerp(startSize, Vector2.zero, shrink);
        localFault = localTile.GetFault();
        if (gameObject.transform.localScale==Vector3.zero)
        {
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            localFault.ExplodeFault();
            Destroy(gameObject);
        }
        



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
