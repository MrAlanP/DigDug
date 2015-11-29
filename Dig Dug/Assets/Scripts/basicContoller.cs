using UnityEngine;
using System.Collections;

public class basicContoller : MonoBehaviour
{
   public GameObject bomb;
   public AudioClip ohNo;
   public AudioClip bump;
   AudioSource source;
   TileManager tileManager;
    float speed = 250;
    public bool falling = false;
    Animator ani;
    public bool dead = false;
    float dying = 0;
    float fade = 0;
    bool sexBomb = false;
    float coolDown = 0;
    bool hasplayed = false;

    ///////////////////////////////////////////////////////////////////////////
    //If false using left side of the controller if true then right side
    /// ///////////////////////////////////////////////////////////////////////
    public bool controllerSide = false;

	// Use this for initialization
	void Start () 
    {
        tileManager = GameObject.FindGameObjectWithTag("Game").GetComponent<TileManager>();
     ani = gameObject.GetComponent<Animator>();
     source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
    void Update()
    {
        ani.SetBool("playerDead", dead);
        if (!dead&&!checkFalling())
        {
            MovePlayer();
            placeBomb();
        }
        else if (dead)
        {
            onDeath();
        }
        else
        {
            ani.SetBool("playerFalling", checkFalling());
            fall();
        }
    }

    //kill player object
    void onDeath()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        dying += Time.deltaTime;
        if (dying >= 3)
        {

            fade += Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, fade);
        }
        if (gameObject.GetComponent<SpriteRenderer>().color == Color.clear)
        {
            Destroy(gameObject);
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
                if (Input.GetButtonDown("Player1Bumper"))
                {
                    Instantiate(bomb, new Vector2(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y), gameObject.transform.rotation);
                    sexBomb = true;
                }
                if (Input.GetButtonUp("Player1Bumper"))
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
                if (Input.GetButtonDown("Player2Bumper"))
                {
                    Instantiate(bomb, new Vector2(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y), gameObject.transform.rotation);
                    sexBomb = true;
                }
                if (Input.GetButtonUp("Player2Bumper"))
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
    bool checkFalling()
    {
        Vector2 tilePos = tileManager.GetClosestTile(gameObject.transform.position).transform.position;
        bool fall = false;
        if (Vector2.Distance(tilePos, gameObject.transform.position)>0.32f)
        {
          fall = true;
        }
        else
        {
            fall = false;
        }
        return fall;
    }
    void fall()
    {
        
        float fallToDeath=0;
       if(!hasplayed)
       {
           hasplayed = true;
           source.PlayOneShot(ohNo);
           
       }
        fallToDeath+=Time.deltaTime;
        gameObject.transform.localScale = Vector2.Lerp(gameObject.transform.localScale, Vector2.zero, fallToDeath);
        if (gameObject.transform.localScale.x <= 0.07f)
        {
            Destroy(gameObject);
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col!=null)
        {
            source.PlayOneShot(bump, 1);
        }
    }
}
