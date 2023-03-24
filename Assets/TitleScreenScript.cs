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
        Start(10, 8, 10);
    }

    public void MediumStart() {
        Start(18, 14, 40);
    }

    public void HardStart() {
        Start(24, 20, 99);
    }

    private void Start(int width, int height, int mines) {
        PlayerPrefs.SetInt("width", width);
        PlayerPrefs.SetInt("height", height);
        PlayerPrefs.SetInt("mines", mines);

        SceneManager.LoadScene("GameScreen");
    }

}
