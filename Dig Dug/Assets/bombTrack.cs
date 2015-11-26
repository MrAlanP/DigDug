using UnityEngine;
using System.Collections;

public class bombTrack : MonoBehaviour 
{
    public GameObject explosion;
    public GameObject player;
    public float speed = 0.50F;
    private float startTime = 0;
    private float journeyLength = 0;
    float detonate = 0;
    bool exploded = false;
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
        //sets bomb timer and animations
        ActiveBomb();
     }
    void ActiveBomb()
    {
        active += Time.deltaTime;
        //makes the bomb throb when thrown
        if (active >= 0.3f)
        {
            throb = !throb;

            if (!throb)
            {
                transform.localScale = new Vector2(1.1f, 1.1f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
            active = 0;
        }
        //makes the bomb "explode"
        detonate += Time.deltaTime;
        if (detonate >= 3)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.localScale = Vector2.Lerp(scale, scale * 4, fracJourney * Time.deltaTime);
            exploded = true;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;

        }
    }
   
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.GetComponent<basicContoller>())
        {
            col.gameObject.GetComponent<basicContoller>().dead = true;
        }
    }
}
