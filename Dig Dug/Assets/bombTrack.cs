using UnityEngine;
using System.Collections;

public class bombTrack : MonoBehaviour 
{
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    float detonate = 0;
    Vector2 scale;
	// Use this for initialization
	void Start ()
    {
      scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        detonate += Time.deltaTime;
        Debug.Log(detonate);
        if (detonate >=3)
        {
           // gameObject.transform.localScale = new Vector2(100f,100f);
            Debug.Log(gameObject.transform.localScale);
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.localScale = Vector2.Lerp(scale,scale*10 , fracJourney);
        }

	}
}
