using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool starting;
    public bool over;

    public int width;
    public int height;
    public int mines;
    public ZoneScript[,] grid;

    public GameObject zone;
    public GameObject gameCamera;
    public GameObject GameOverScreen;
    public GameObject WinScreen;

    // Start is called before the first frame update
    void Start()
    {
        starting = true;
        over = false;

        width = PlayerPrefs.GetInt("width");
        height = PlayerPrefs.GetInt("height");
        mines = PlayerPrefs.GetInt("mines");
        grid = new ZoneScript[width, height];

        // fix camera
        gameCamera.transform.position = new Vector3((width / 2 - .5f), (height / 2 - .5f), gameCamera.transform.position.z);
        gameCamera.GetComponent<Camera>().orthographicSize = height / 2 + 1;

        // generate game
        GenerateZones();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private ZoneScript AddZone(Vector2 position, bool hasMine) {

        int x = (int) position.x;
        int y = (int) position.y;

		GameObject newZoneObject = GameObject.Instantiate(zone, new Vector3(x, y, 0), Quaternion.identity);
		ZoneScript zoneScript = newZoneObject.GetComponent<ZoneScript>(); 
		zoneScript.hasMine = hasMine;

        return zoneScript;

    }

    private void GenerateZones() {

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                
			    grid[x, y] = AddZone(new Vector2(x, y), false);

            }
        }

    }

    public void SetMines(Vector2 startPosition) {

        int minesSet = 0;
        System.Random randomGenerator = new System.Random();

        int startX = (int) startPosition.x;
        int startY = (int) startPosition.y;

        ZoneScript[] adjacentZones = grid[startX, startY].GetAdjacentZones();

        while (minesSet < mines) {

            // get coords of random zone
            int x = randomGenerator.Next(width);
            int y = randomGenerator.Next(height);

            // check zone isn't already a mine
            if (grid[x, y].hasMine) continue;

            // check zone isn't equal to start
            if (x == startX && y == startY) continue;

            // check zone isn't adjacent to start
            bool adjacentToStart = false;

            foreach (ZoneScript zone in adjacentZones)
                if (System.Object.ReferenceEquals(zone, grid[x, y]))
                    adjacentToStart = true;

            if (adjacentToStart) continue;

            // set mine
            grid[x, y].hasMine = true;
            minesSet++;

        }

    }

    public void CheckWin() {

        int count = 0;

        // count revealed squares
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {

                if (grid[i, j].revealed) count++;

            }
		}

        if (count == (width * height - mines)) {

            Win();

		}

    }

    private void Win() {

        Debug.Log("win");

    }

    public void GameOver() {

        // reveal all mines
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {

                ZoneScript zone = grid[i, j];
                if (zone.hasMine) {
                    if (zone.flagged) zone.Flag();
                    zone.ShowMine();
				}

            }
		}

        over = true;
        GameOverScreen.SetActive(true);

    }

}
