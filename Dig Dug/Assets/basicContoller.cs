using UnityEngine;
using System.Collections;

public class basicContoller : MonoBehaviour
{
    float speed = 250;
    public bool falling = false;
    Animator ani;

    //If false using left side of the controller if true then right side
    bool controllerSide = true;

	// Use this for initialization
	void Start () 
    {
        ani = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        MovePlayer();
        if (falling)
        {
            ani.Play("PlayerFall");
        }
	}

    void MovePlayer()
    {
        //Left side of the controller
        if (controllerSide == false)
        {
            if (Input.GetAxis("Player1DpadX") != 0)
            {

                if (Input.GetAxis("Player1DpadX") > 0)
                {

                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((Input.GetAxis("Player1DpadX") * speed), 0));
                    ani.Play("walkR");
                }
                else
                {
                    ani.Play("walkL");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("Player1DpadX") * speed, 0));
                }
            }
            else if (Input.GetAxis("Player1DpadY") != 0)
            {

                if (Input.GetAxis("Player1DpadY") > 0)
                {
                    ani.Play("WalkUp");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Input.GetAxis("Player1DpadY") * speed));
                }
                else
                {
                    ani.Play("walkDown");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Input.GetAxis("Player1DpadY") * speed));
                }
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                ani.Play("playerIdle");
            }
        }

        //Right side of the controller
        else if (controllerSide == true)
        {
            if (Input.GetButton("Player2ButtonX") != false || Input.GetButton("Player2ButtonB") != false)
            {
                float player2MovementXAxis = 0f;
                //recognize button input
                if (Input.GetButton("Player2ButtonX") == true)
                {
                    player2MovementXAxis = -1f;
                }
                else if (Input.GetButton("Player2ButtonB") == true)
                {
                    player2MovementXAxis = 1f;
                }
                if (Input.GetButton("Player2ButtonX") == true && Input.GetButton("Player2ButtonB") == true)
                {
                    player2MovementXAxis = 0f;
                }

                //do movement stuff
                if (player2MovementXAxis > 0)
                {

                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((player2MovementXAxis * speed), 0));
                    ani.Play("walkR");
                }
                else
                {
                    ani.Play("walkL");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(player2MovementXAxis * speed, 0));
                }
            }
            else if (Input.GetButton("Player2ButtonY") != false)
            {
                float player2MovementY = 1.0f;

                if (player2MovementY > 0)
                {
                    ani.Play("WalkUp");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player2MovementY * speed));
                }
                else
                {
                    ani.Play("walkDown");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player2MovementY * speed));
                }
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                ani.Play("playerIdle");
            }
        }
    }
}
