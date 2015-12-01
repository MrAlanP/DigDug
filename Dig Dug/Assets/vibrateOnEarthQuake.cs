using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class vibrateOnEarthQuake : MonoBehaviour {

    bool playerIndexSet = false;
    PlayerManager playerManager;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    TileManager tileManager;
    float timer = 0;
    void Start()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
        tileManager = gameObject.GetComponent<TileManager>();
       
    }

    // Update is called once per frame
    void Update()
    {

        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                Debug.Log(playerIndex);
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);


        if (tileManager.quake)
        {
            timer += Time.deltaTime;
            if (timer <= 1)
            {
                for (int i = 0; i < playerManager.players.Count; i++)
                {
                    GamePad.SetVibration((PlayerIndex)i, 0, 1);
                    //if (i % 2 != 0)
                    //    GamePad.SetVibration((PlayerIndex)(i / 2), 0, 1);
                    //else
                    //    GamePad.SetVibration((PlayerIndex)(i / 2), 1, 0);
                }

            }
            else
            {
                for (int i = 0; i < 4; i++)
                    GamePad.SetVibration((PlayerIndex)i, 0, 0);

                tileManager.quake = false;
                timer = 0;
            }
            
        }
       
    }


}

