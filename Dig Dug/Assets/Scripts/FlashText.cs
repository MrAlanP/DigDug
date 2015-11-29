using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashText : MonoBehaviour {

    int counter = 0;
    int blinkSpeed = 10;
    bool blinkR;
    bool blinkY;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (counter == 5)
        {
            blinkR = false;
            blinkY = true;
        }
        if (counter == blinkSpeed)
        {
            blinkY = false;
            blinkR = true;
        
            counter = 0;
        }
        
        counter++;

        if (blinkY == true)
        {
            text.color = Color.yellow;
        }
        if (blinkR == true)
        {
            text.color = Color.red;
        }
	}
}
