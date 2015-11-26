using UnityEngine;
using System.Collections;

public class bombTrack : MonoBehaviour 
{
    public float speed = 0.50F;
    private float startTime = 0;
    private float journeyLength = 0;
    float detonate = 0;
    bool throb = false;
    float active = 0;
    Vector2 scale;
	// Use this for initialization
	void Start ()
    {
        startTime = Time.time;
        journeyLength = Vector2.Distance(scale, scale*4);
      scale = transform.localScale;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        active += Time.deltaTime;
        if (active >= 0.3f)
        {
            throb = !throb;

            if (!throb)
            {
                transform.localScale = new Vector2(1.1f, 1.1f);//,1.5f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
               
            }
            else
            {
                transform.localScale = new Vector2(1, 1);

                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
               
            } 
            active = 0;
        }

        detonate += Time.deltaTime;
        if (detonate >=2)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.localScale = Vector2.Lerp(scale,scale*4 , fracJourney*Time.deltaTime);
        }
        else
        {
          
        }

	}
}
