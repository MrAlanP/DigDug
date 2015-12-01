using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashText : MonoBehaviour {

    float counter = 0;
    float blinkSpeed = 0.2f;
	bool isRed = true;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update ()
    {
        
        counter+= Time.deltaTime;

		if (counter >= blinkSpeed) {
			isRed = !isRed;
			if (isRed)
			{

				text.color = Color.yellow;
			}
			else
			{
				text.color = Color.red;
			}
			counter = 0;
		}

        
	}
}
