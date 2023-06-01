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
    public int flags;
    public ZoneScript[,] grid;

    public GameObject zone;
    public GameObject gameCamera;
    public GameObject WinScreen;
    public HUDScript HUDManager;

    public float UISize;

    // Start is called before the first frame update
    void Start()
    {
        starting = true;
        over = false;

        width = PlayerPrefs.GetInt("width");
        height = PlayerPrefs.GetInt("height");
        mines = PlayerPrefs.GetInt("mines");
        flags = mines;
        grid = new ZoneScript[width, height];

        // fix camera
        gameCamera.GetComponent<Camera>().orthographicSize = (height / 2) + (UISize * height);
        gameCamera.transform.position = new Vector3((width / 2 - .5f), (height / 2 - .5f) + (UISize * 0.5f * height), gameCamera.transform.position.z);

        // set HUD values
        HUDManager.UpdateFlagCount();

        // generate game
        GenerateZones();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // creates a new 'zone' gameObject and returns it
    // used by generateZones
    private ZoneScript AddZone(Vector2 position, bool hasMine) {

        int x = (int) position.x;
        int y = (int) position.y;

		GameObject newZoneObject = GameObject.Instantiate(zone, new Vector3(x, y, 0), Quaternion.identity);
		ZoneScript zoneScript = newZoneObject.GetComponent<ZoneScript>(); 
		zoneScript.hasMine = hasMine;

        return zoneScript;

    }

    // generates the field - made up of 'zones' which are saved in grid
    // called at game start
    private void GenerateZones() {

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                
			    grid[x, y] = AddZone(new Vector2(x, y), false);

            }
        }

    }

    // generates the field's mines based on a starting position
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

    // check if the game is won
    // called when a zone is clicked
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

    // called when game is won
    private void Win() {

        Debug.Log("win");

    }

    // called when game is lost
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

    }

}
