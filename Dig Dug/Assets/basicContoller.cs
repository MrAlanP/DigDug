using UnityEngine;
using System.Collections;

public class basicContoller : MonoBehaviour
{
    float speed = 250;
    public bool falling = false;
    Animator ani;

    ///////////////////////////////////////////////////////////////////////////
    //If false using left side of the controller if true then right side
    /// ///////////////////////////////////////////////////////////////////////
    public bool controllerSide = true;

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
           // ani.Play("PlayerFall");
        }
	}

    void MovePlayer()
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////
        // Left side of the controller
        /////////////////////////////////////////////////////////////////////////////////////////////////
        if (controllerSide == false)
        {
            ////////////////////////////////////
            // X Axis button input here
            ////////////////////////////////////
            if (Input.GetAxis("Player1DpadX") != 0)
            {
                float player1MovementXAxis = 0f;

                //set all inputs straight to 1 so both sides of the controller have the same movement
                if (Input.GetAxis("Player1DpadX") < 0)
                {
                    player1MovementXAxis = -1f;
                }
                else if (Input.GetAxis("Player1DpadX") > 0)
                {
                    player1MovementXAxis = 1f;
                }

                //do movement stuff
                if (player1MovementXAxis > 0)
                {

                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((player1MovementXAxis * speed), 0));
                   // ani.Play("walkR");
                }
                else
                {
                   // ani.Play("walkL");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(player1MovementXAxis * speed, 0));
                }
            }
            ////////////////////////////////////
            // Y Axis button input here
            ////////////////////////////////////
            else if (Input.GetAxis("Player1DpadY") != 0)
            {
                float player1MovementYAxis = 0f;

                //set all inputs straight to 1 so both sides of the controller have the same movement
                if (Input.GetAxis("Player1DpadY") < 0)
                {
                    player1MovementYAxis = -1f;
                }
                else if (Input.GetAxis("Player1DpadY") > 0)
                {
                    player1MovementYAxis = 1f;
                }

                //do movement stuff
                if (player1MovementYAxis > 0)
                {
                    //ani.Play("WalkUp");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player1MovementYAxis * speed));
                }
                else
                {
                   // ani.Play("walkDown");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player1MovementYAxis * speed));
                }
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //ani.Play("playerIdle");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        // Right side of the controller
        /////////////////////////////////////////////////////////////////////////////////////////////////
        else if (controllerSide == true)
        {
            ////////////////////////////////////
            // X Axis button input here
            ////////////////////////////////////
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
                    //ani.Play("playerIdle");
                }

                //do movement stuff
                if (player2MovementXAxis > 0)
                {

                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((player2MovementXAxis * speed), 0));
                    //ani.Play("walkR");
                }
                else
                {
                    //ani.Play("walkL");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(player2MovementXAxis * speed, 0));
                }
            }
            ////////////////////////////////////
            // Y Axis button input here
            ////////////////////////////////////
            else if (Input.GetButton("Player2ButtonY") != false || Input.GetButton("Player2ButtonA") != false)
            {
                float player2MovementYAxis = 0f;

                //recognize button input
                if (Input.GetButton("Player2ButtonA") == true)
                {
                    player2MovementYAxis = -1f;
                }
                else if (Input.GetButton("Player2ButtonY") == true)
                {
                    player2MovementYAxis = 1f;
                }
                if (Input.GetButton("Player2ButtonA") == true && Input.GetButton("Player2ButtonY") == true)
                {
                    player2MovementYAxis = 0f;
                    //ani.Play("playerIdle");
                }

                // do movement stuff
                if (player2MovementYAxis > 0)
                {
                   // ani.Play("WalkUp");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player2MovementYAxis * speed));
                }
                else
                {
                    //ani.Play("walkDown");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player2MovementYAxis * speed));
                }
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //ani.Play("playerIdle");
            }
        }
    }
}
