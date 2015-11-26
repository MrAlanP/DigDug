using UnityEngine;
using System.Collections;

public class basicContoller : MonoBehaviour
{
   public GameObject bomb;
    float speed = 250;
    public bool falling = false;
    Animator ani;
    public bool dead = false;
    bool sexBomb = false;
    float coolDown = 0;

    ///////////////////////////////////////////////////////////////////////////
    //If false using left side of the controller if true then right side
    /// ///////////////////////////////////////////////////////////////////////
    public bool controllerSide = true;

	// Use this for initialization
	void Start () 
    {
       // bomb = GameObject.FindGameObjectWithTag("Bomb");
        ani = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
    void Update()
    {
        MovePlayer();
        placeBomb();
        ani.SetBool("playerFalling", falling);
        ani.SetBool("playerDead", dead);
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
            //if (Input.GetAxis("Player1DpadX") != 0)
            //{
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
					ani.SetFloat("XMovement", player1MovementXAxis);
                   // ani.Play("walkR");
                }
                else if(player1MovementXAxis < 0)
                {
                   // ani.Play("walkL");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(player1MovementXAxis * speed, 0));
					ani.SetFloat("XMovement", player1MovementXAxis);
                }
				else if(player1MovementXAxis == 0)
				{
					ani.SetFloat("XMovement", player1MovementXAxis);
				}
            //}
            ////////////////////////////////////
            // Y Axis button input here
            ////////////////////////////////////
            //else if (Input.GetAxis("Player1DpadY") != 0)
           // {
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
					ani.SetFloat("YMovement", player1MovementYAxis);
                }
                else if(player1MovementYAxis < 0)
                {
                   // ani.Play("walkDown");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player1MovementYAxis * speed));
					ani.SetFloat("YMovement", player1MovementYAxis);
				
                }
            	else if(player1MovementYAxis == 0)
           		{
                	gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					ani.SetFloat("YMovement", player1MovementYAxis);
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
           // if (Input.GetButton("Player2ButtonX")  || Input.GetButton("Player2ButtonB"))
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
					ani.SetFloat("XMovement", player2MovementXAxis);
                    //ani.Play("walkR");
                }
				else if (player2MovementXAxis < 0)
                {
                    //ani.Play("walkL");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(player2MovementXAxis * speed, 0));
					ani.SetFloat("XMovement", player2MovementXAxis);
					
                }
				else if(player2MovementXAxis == 0)
				{
					gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					ani.SetFloat("XMovement", player2MovementXAxis);
					//ani.Play("playerIdle");
				}
            }
            ////////////////////////////////////
            // Y Axis button input here
            ////////////////////////////////////
         //   else if (Input.GetButton("Player2ButtonY") != false || Input.GetButton("Player2ButtonA") != false)
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
					ani.SetFloat("YMovement", player2MovementYAxis);
					
                }
                else if (player2MovementYAxis < 0)
                {
                    //ani.Play("walkDown");
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player2MovementYAxis * speed));
					ani.SetFloat("YMovement", player2MovementYAxis);
					
                }
				else if (player2MovementYAxis == 0)
				{
					gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					ani.SetFloat("YMovement", player2MovementYAxis);
					
				}
            }
        }

    }
    void placeBomb()
    {
        if (!controllerSide)
        {
            if (!sexBomb)
            {
                if (Input.GetButtonDown("Player1BumperLeft"))
                {
                    Instantiate(bomb, new Vector2(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y), gameObject.transform.rotation);
                    sexBomb = true;
                }
                if (Input.GetButtonUp("Player1BumperLeft"))
                {
                    sexBomb = false;
                }
            }
            else
            {
                coolDown += Time.deltaTime;
                if (coolDown >= 2)
                {
                    sexBomb = false;
                    coolDown = 0;
                }
            }
        }
        else
        {
            if (!sexBomb)
            {
                if (Input.GetButtonDown("Player2BumperRight"))
                {
                    Instantiate(bomb, new Vector2(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y), gameObject.transform.rotation);
                    sexBomb = true;
                }
                if (Input.GetButtonUp("Player2BumperRight"))
                {
                    sexBomb = false;
                }
            }
            else
            {
                coolDown += Time.deltaTime;
                if (coolDown >= 2)
                {
                    sexBomb = false;
                    coolDown = 0;
                }
            }
        }
    }
}
