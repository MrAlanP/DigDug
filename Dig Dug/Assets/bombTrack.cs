using UnityEngine;
using System.Collections;

public class bombTrack : MonoBehaviour 
{
    public GameObject explosion;
   
    bool exploded = false;
    bool throb = false;
    float active = 0;
    float throbCount = 0;
    Vector2 scale;
		
	// Update is called once per frame
	void Update ()
    {
       groundActiveBomb();
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
