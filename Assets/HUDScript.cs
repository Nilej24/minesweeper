using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour
{

    public GameManager game;
    public TextMeshProUGUI flagCounter;
    public TextMeshProUGUI timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("TitleScreen");
    }

    public void UpdateFlagCount() {
        flagCounter.text = game.flags.ToString();
    }

    public void UpdateTimeCounter() {
        timeCounter.text = ((int) game.time).ToString();
    }

}
