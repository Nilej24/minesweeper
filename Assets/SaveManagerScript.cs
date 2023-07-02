using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveManagerScript : MonoBehaviour
{
    private int currentChar = 1;
    public TextMeshProUGUI char1, char2, char3;

    public TextMeshProUGUI difficulty, time;

    // Start is called before the first frame update
    void Start()
    {
        // show game difficulty on screen
        switch (PlayerPrefs.GetInt("difficulty")) {

            case 1:
                difficulty.text = "Easy";
                break;
            case 2:
                difficulty.text = "Medium";
                break;
            case 3:
                difficulty.text = "Hard";
                break;

        }

        // show player's time on screen
        time.text = PlayerPrefs.GetInt("time").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // input letters with the keyboard buttons
    public void InputLetter(string letter) {

        // cancel function if name is complete
        if (currentChar > 3) return;

        // update current onscreen letter
        switch (currentChar) {

            case 1:
                char1.text = letter;
                break;
            case 2:
                char2.text = letter;
                break;
            case 3:
                char3.text = letter;
                break;

        }

        // move to next letter
        currentChar++;

    }

    // delete the previous letter
    public void InputBackspace() {

        // cancel function if name is empty
        if (currentChar <= 1) return;

        // delete prev onscreen letter
        switch (currentChar) {

            case 2:
                char1.text = "_";
                break;
            case 3:
                char2.text = "_";
                break;
            case 4:
                char3.text = "_";
                break;

        }

        // move to prev letter
        currentChar--;
        
    }

}
