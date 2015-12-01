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
    float player2MovementYAxis = 0f;

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
       
        if (!dead&&!checkFalling())
        {
            dontFallInTheWaterDummy();
            MovePlayer();
            placeBomb();
           
            
        }
        else if (dead)
        {
            
            ani.SetBool("playerDead", dead);
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
            
                float player1MovementXAxis = 0f;
                if (Input.GetAxis("Player" + playerIndex + "DpadY") == 0)
                {
                    //set all inputs straight to 1 so both sides of the controller have the same movement
                    if (Input.GetAxis("Player" + playerIndex + "DpadX") < 0)
                    {
                        player1MovementXAxis = -1f;
                    }
                    else if (Input.GetAxis("Player" + playerIndex + "DpadX") > 0)
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
                if (canMoveD)
                {//do movement stuff
                    if (player1MovementYAxis > 0)
                    {
                        //ani.Play("WalkUp");
                        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player1MovementYAxis * speed));
                        ani.SetFloat("YMovement", player1MovementYAxis);
                    }
                }
               if (canMoveU)
                {
                    if (player1MovementYAxis < 0)
                    {
                        // ani.Play("walkDown");
                        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player1MovementYAxis * speed));
                        ani.SetFloat("YMovement", player1MovementYAxis);

                    }
                }
                 if (player1MovementYAxis == 0)
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
               // if (!Input.GetButton("Player" + playerIndex + "ButtonY") || !Input.GetButton("Player" + playerIndex + "ButtonA"))
                if (player2MovementYAxis == 0)
                {//recognize button input
                    if (Input.GetButton("Player" + playerIndex + "ButtonX") == true)
                    {
                        player2MovementXAxis = -1f;
                    }
                    else if (Input.GetButton("Player" + playerIndex + "ButtonB") == true)
                    {
                        player2MovementXAxis = 1f;
                    }
                    if (Input.GetButton("Player" + playerIndex + "ButtonX") == true && Input.GetButton("Player" + playerIndex + "ButtonB") == true)
                    {
                        player2MovementXAxis = 0f;
                        //ani.Play("playerIdle");
                    }

                    if (camMoveR)
                    {
                        //do movement stuff
                        if (player2MovementXAxis > 0)
                        {
                            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((player2MovementXAxis * speed), 0));
                            ani.SetFloat("XMovement", player2MovementXAxis);
                            //ani.Play("walkR");
                        }
                    }
                    if (canMoveL)
                    {
                        if (player2MovementXAxis < 0)
                        {
                            //ani.Play("walkL");
                            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(player2MovementXAxis * speed, 0));
                            ani.SetFloat("XMovement", player2MovementXAxis);

                        }
                    }
                }
                    if (player2MovementXAxis == 0)
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
                 player2MovementYAxis = 0f;
                
                //recognize button input
                if (Input.GetButton("Player" + playerIndex + "ButtonA") == true)
                {
                    player2MovementYAxis = -1f;
                }
                else if (Input.GetButton("Player" + playerIndex + "ButtonY") == true)
                {
                    player2MovementYAxis = 1f;
                }
                if (Input.GetButton("Player" + playerIndex + "ButtonA") == true && Input.GetButton("Player" + playerIndex + "ButtonY") == true)
                {
                    player2MovementYAxis = 0f;
                    //ani.Play("playerIdle");
                }
                if (canMoveD)
                {
                    // do movement stuff
                    if (player2MovementYAxis > 0)
                    {
                        // ani.Play("WalkUp");
                        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player2MovementYAxis * speed));
                        ani.SetFloat("YMovement", player2MovementYAxis);

                    }
                }
                if (canMoveU)
                {
                    if (player2MovementYAxis < 0)
                    {
                        //ani.Play("walkDown");
                        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player2MovementYAxis * speed));
                        ani.SetFloat("YMovement", player2MovementYAxis);

                    }
                }
                if (player2MovementYAxis == 0)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    ani.SetFloat("YMovement", player2MovementYAxis);

                }
            }
        }

    }
    Vector2 nearestFault()
    {
        Fault fault;
        Tile tile = tileManager.GetClosestTile(gameObject.transform.position);
        IntVector2 tileCheck = tile.tileIndex;
        //right
        if (tileManager.GetTile(new IntVector2(tileCheck.x + 1, tileCheck.y)).HasFault())
        {
            fault = tileManager.GetTile(new IntVector2(tileCheck.x + 1, tileCheck.y)).GetFault();
            if (fault.CanExplode())
            {
                return tileManager.GetTile(new IntVector2(tileCheck.x + 1, tileCheck.y)).transform.position;
            }
            else
            {
                return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f);
            }
        }
            //left
        else if (tileManager.GetTile(new IntVector2(tileCheck.x - 1, tileCheck.y)).HasFault())
        {
            fault = tileManager.GetTile(new IntVector2(tileCheck.x - 1, tileCheck.y)).GetFault();
            if (fault.CanExplode())
            {
                return tileManager.GetTile(new IntVector2(tileCheck.x - 1, tileCheck.y)).transform.position;
            }
            else
            {
                return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f);
            }
        }
            //down
        else if (tileManager.GetTile(new IntVector2(tileCheck.x, tileCheck.y + 1)).HasFault())
        {
            fault = tileManager.GetTile(new IntVector2(tileCheck.x, tileCheck.y + 1)).GetFault();
            if (fault.CanExplode())
            {
                return tileManager.GetTile(new IntVector2(tileCheck.x, tileCheck.y + 1)).transform.position;
            }
            else
            {
                return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f);
            }
        }
            //up
        else if (tileManager.GetTile(new IntVector2(tileCheck.x, tileCheck.y - 1)).HasFault())
        {
            fault = tileManager.GetTile(new IntVector2(tileCheck.x, tileCheck.y - 1)).GetFault();
            if (fault.CanExplode())
            {
                return tileManager.GetTile(new IntVector2(tileCheck.x, tileCheck.y-1)).transform.position;
            }
            else
            {
                return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f);
            }
        }
        else if (tile.HasFault())
        {
            return tile.transform.position;
        }
        else
        {
            return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f);
        }
    }
    void placeBomb()
    {

	    if (!sexBomb)
	    {
			if (Input.GetButtonDown("Player"+playerIndex+"Bumper"))
	        {
                Instantiate(bomb, nearestFault(), gameObject.transform.rotation);
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

    //edge check
    void dontFallInTheWaterDummy()
    {
        Tile closeTile = tileManager.GetClosestTile(transform.position);
        IntVector2 closeTileint = closeTile.tileIndex;
        if (tileManager.adjacentWaterTiles.Contains(closeTile))
        {
           
            if (!tileManager.GetTile(new IntVector2(closeTileint.x+1, closeTileint.y)).GetComponent<SpriteRenderer>().enabled)
            {
                camMoveR = false;
            }
          
           if (!tileManager.GetTile(new IntVector2(closeTileint.x-1, closeTileint.y)).GetComponent<SpriteRenderer>().enabled)
           {
               canMoveL = false;
           }
         
           if (!tileManager.GetTile(new IntVector2(closeTileint.x, closeTileint.y+1)).GetComponent<SpriteRenderer>().enabled)
           {
               canMoveD = false;
           }
        
           if (!tileManager.GetTile(new IntVector2(closeTileint.x, closeTileint.y-1)).GetComponent<SpriteRenderer>().enabled)
           {
               canMoveU = false;
           }
        
        }
        else
        {
            camMoveR = true;
            canMoveU = true;
            canMoveD = true;
            canMoveL = true;
        }
           
    }
}
