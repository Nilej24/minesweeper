using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EasyStart() {
		PlayerPrefs.SetInt("difficulty", 1);
        Start(10, 8, 10);
    }

    public void MediumStart() {
		PlayerPrefs.SetInt("difficulty", 2);
        Start(18, 14, 40);
    }

    public void HardStart() {
		PlayerPrefs.SetInt("difficulty", 3);
        Start(24, 20, 99);
    }

    private void Start(int width, int height, int mines) {
        PlayerPrefs.SetInt("width", width);
        PlayerPrefs.SetInt("height", height);
        PlayerPrefs.SetInt("mines", mines);

        PlayerPrefs.SetInt("score", 999);

        SceneManager.LoadScene("GameScreen");
    }

}
