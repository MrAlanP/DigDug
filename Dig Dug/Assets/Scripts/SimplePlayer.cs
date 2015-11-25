using UnityEngine;
using System.Collections;


public class SimplePlayer : MonoBehaviour {

    public float moveX = 0;
    public float moveY = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveX = Input.GetAxis("Player1DpadX");
        moveY = Input.GetAxis("Player1DpadY");
        Debug.Log(moveX);

        Vector2 moveDir = new Vector2(moveY, moveX);
        float speed = 1f;
        transform.Translate(speed * moveDir * Time.deltaTime);
	}
}
