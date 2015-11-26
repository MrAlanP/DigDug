using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {
    Animator anim;
    float die = 0;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
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

        if (col.gameObject.GetComponent<basicContoller>())
        {
            col.gameObject.GetComponent<basicContoller>().dead = true;
        }
    }
}
