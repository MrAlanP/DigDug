using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
   public GameObject bomb;
   public AudioClip ohNo;
   public AudioClip bump;
   AudioSource source;
   TileManager tileManager;
    float speed = 150;
    public bool falling = false;
    Animator ani;
    public bool dead = false;
    float dying = 0;
    float fade = 0;
    bool sexBomb = false;
    float coolDown = 0;
    bool hasplayed = false;
    bool canMoveL = true;
    bool camMoveR = true;
    bool canMoveU = true;
    bool canMoveD = true;

	int playerIndex;

    ///////////////////////////////////////////////////////////////////////////
    //If false using left side of the controller if true then right side
    /// ///////////////////////////////////////////////////////////////////////
    public bool controllerSide = false;

	// Use this for initialization
	void Awake () 
    {
		playerIndex = 0;
        tileManager = GameObject.FindGameObjectWithTag("Game").GetComponent<TileManager>();
     ani = gameObject.GetComponent<Animator>();
     source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
    void Update()
    {
		if (playerIndex == 0) {
			return;
		}
        ani.SetBool("playerDead", dead);
        if (!dead&&!checkFalling())
        {
            dontFallInTheWaterDummy();
            MovePlayer();
            placeBomb();
           
            
        }
        else if (dead)
        {
            onDeath();
        }
        if (checkFalling())
        {
            ani.SetBool("playerFalling", checkFalling());
            fall();
        }
    }

	public void SetIndex(int index){
		playerIndex = index;
		if (index % 2 == 0) {
			controllerSide = true;
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
        if (!controllerSide)
        {
            ////////////////////////////////////
            // X Axis button input here
            ////////////////////////////////////
            //if (Input.GetAxis("Player1DpadX") != 0)
            //{
                float player1MovementXAxis = 0f;

                //set all inputs straight to 1 so both sides of the controller have the same movement
				if (Input.GetAxis("Player"+playerIndex+"DpadX") < 0)
                {
                    player1MovementXAxis = -1f;
                }
				else if (Input.GetAxis("Player"+playerIndex+"DpadX") > 0)
                {
                    player1MovementXAxis = 1f;
                }
                if (camMoveR)
                {
                    //do movement stuff
                    if (player1MovementXAxis > 0)
                    {

                        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((player1MovementXAxis * speed), 0));
                        ani.SetFloat("XMovement", player1MovementXAxis);
                        // ani.Play("walkR");
                    }
                }
                if (canMoveL)
                {
                    if (player1MovementXAxis < 0)
                    {
                        // ani.Play("walkL");
                        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(player1MovementXAxis * speed, 0));
                        ani.SetFloat("XMovement", player1MovementXAxis);
                    }
                }
                if (player1MovementXAxis == 0)
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
				if (Input.GetAxis("Player"+playerIndex+"DpadY") < 0)
                {
                    player1MovementYAxis = -1f;
                }
				else if (Input.GetAxis("Player"+playerIndex+"DpadY") > 0)
                {
                    player1MovementYAxis = 1f;
                }

			
            //if (canMoveD)
            
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
        else
        {
            ////////////////////////////////////
            // X Axis button input here
            ////////////////////////////////////
           // if (Input.GetButton("Player2ButtonX")  || Input.GetButton("Player2ButtonB"))
            {
                float player2MovementXAxis = 0f;

                //recognize button input
                if (Input.GetButton("Player"+playerIndex+"ButtonX") == true)
                {
                    player2MovementXAxis = -1f;
                }
				else if (Input.GetButton("Player"+playerIndex+"ButtonB") == true)
                {
                    player2MovementXAxis = 1f;
                }
				if (Input.GetButton("Player"+playerIndex+"ButtonX") == true && Input.GetButton("Player"+playerIndex+"ButtonB") == true)
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
				if (Input.GetButton("Player"+playerIndex+"ButtonA") == true)
                {
                    player2MovementYAxis = -1f;
                }
				else if (Input.GetButton("Player"+playerIndex+"ButtonY") == true)
                {
                    player2MovementYAxis = 1f;
                }
				if (Input.GetButton("Player"+playerIndex+"ButtonA") == true && Input.GetButton("Player"+playerIndex+"ButtonY") == true)
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

	    if (!sexBomb)
	    {
			if (Input.GetButtonDown("Player"+playerIndex+"Bumper"))
	        {
	            Instantiate(bomb, new Vector2(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y), gameObject.transform.rotation);
	            sexBomb = true;
	        }
			if (Input.GetButtonUp("Player"+playerIndex+"Bumper"))
	        {
	            sexBomb = false;
	        }
	    }
	    else
	    {
	        coolDown += Time.deltaTime;
	        if (coolDown >= 0.5f)
	        {
	            sexBomb = false;
	            coolDown = 0;
	        }
	    }

    }
    bool checkFalling()
    {
        Tile tileCheck = tileManager.GetClosestTile(gameObject.transform.position);
        Vector2 tilePos = tileCheck.transform.position;
        bool fall = false;
      //  Debug.Log(tileCheck.name.ToString());
        if (!tileCheck.GetComponent<SpriteRenderer>().enabled)
        {
            {
                fall = true;
            }
        }
        else if (Vector2.Distance(tilePos, gameObject.transform.position) > 0.32f)
        {
            fall = true;
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
        gameObject.transform.Rotate(new Vector3(0, 0,1)*Mathf.PI*2);
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

    /// <summary>
    /// this function tells john to fuck off
    /// </summary>
    void dontFallInTheWaterDummy()
    {
        Debug.Log("dont fall in the water dummy");
        Tile closeTile = tileManager.GetClosestTile(transform.position);
        IntVector2 closeTileint = closeTile.tileIndex;
        if (tileManager.adjacentToWaterTiles.Contains(closeTile))
        {
            Debug.Log("Close tie");
            if (!tileManager.GetTile(new IntVector2(closeTileint.x+1, closeTileint.y)).GetComponent<SpriteRenderer>().enabled)
            {
                camMoveR = false;
                Debug.Log("water to yr right");
            }
            else
            {
                camMoveR = true;
            }
           if (!tileManager.GetTile(new IntVector2(closeTileint.x-1, closeTileint.y)).GetComponent<SpriteRenderer>().enabled)
           {
               Debug.Log("water to your left");
               canMoveL = false;
           }
           else
           {
               canMoveL = true;
           }
           if (!tileManager.GetTile(new IntVector2(closeTileint.x, closeTileint.y+1)).GetComponent<SpriteRenderer>().enabled)
           {
               canMoveD = false;
           }
           else
           {
               canMoveD = true;
           }
           if (!tileManager.GetTile(new IntVector2(closeTileint.x, closeTileint.y-1)).GetComponent<SpriteRenderer>().enabled)
           {
               canMoveU = false;
           }
           else
           {
               canMoveU = true;
           }
        }
           
    }
}
