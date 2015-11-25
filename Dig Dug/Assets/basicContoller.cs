using UnityEngine;
using System.Collections;

public class basicContoller : MonoBehaviour
{
    float speed = 250;
    public bool falling = false;
    Animator ani;
	// Use this for initialization
	void Start () 
    {
        ani = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
          
            if (Input.GetAxis("Horizontal") > 0)
            {
                
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((Input.GetAxis("Horizontal") * speed) , 0));
                ani.Play("walkR");
            }
            else
            {
                ani.Play("walkL");
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
            }
        }
        else if (Input.GetAxis("Vertical")!=0)
        {
        
            if (Input.GetAxis("Vertical")>0)
            {
                ani.Play("WalkUp");
                gameObject.GetComponent<Rigidbody2D>().AddForce( new Vector2(0, Input.GetAxis("Vertical") * speed));
            }
            else
            {
                ani.Play("walkDown");
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Input.GetAxis("Vertical") * speed));
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ani.Play("playerIdle");
        }
        if (falling)
        {
            ani.Play("PlayerFall");
        }
	}
}
