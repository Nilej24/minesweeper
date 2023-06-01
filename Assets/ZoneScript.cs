using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public bool hasMine = false;
    public bool revealed = false;
    public bool flagged = false;

    private GameManager game;
    private HUDScript HUDManager;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("Game Manager").GetComponent<GameManager>();
        HUDManager = GameObject.Find("Game HUD").GetComponent<HUDScript>();

        ShowFlag(false);
        ChangeAppearance(true, false, false);
    }

    // Update is called once per frame
    void Update()
    {
    }

	private void OnMouseOver()
	{
        if (game && game.over) return;

        if (Input.GetMouseButtonDown(0)) Reveal();
        else if (Input.GetMouseButtonDown(1)) Flag();
	}

    public void Flag() {

        if (game.over || revealed) return;

        if (flagged) {

            flagged = false;
            game.flags++;

        } else if (!flagged) {

            if (game.flags <= 0) return;

            flagged = true;
            game.flags--;

        }

        ShowFlag(flagged);
        HUDManager.UpdateFlagCount();

    }

    private void ShowFlag(bool flagVisible) {

	    transform.Find("Flag").gameObject.SetActive(flagVisible);

    }

    public void Reveal() {

        if (revealed || flagged) return;
        revealed = true;

        if (game.starting) {

            game.SetMines(new Vector2(transform.position.x, transform.position.y));
            game.starting = false;

        }

        if (hasMine) {

            ChangeAppearance(false, false, true);
            game.GameOver();
            return;

        }

		int adjacentMines = CountAdjacentMines();

		if (adjacentMines == 0) {

			ChangeAppearance(false, false, false);

            // make a 'reveal chain' which reveals all attached 'zero adjacent' zones
			foreach(ZoneScript zone in GetAdjacentZones())
				if (!zone.revealed) {
                    if (zone.flagged) zone.Flag(); // unflag zone if flagged
                    zone.Reveal(); // start reveal chain
				}

		} else {

			SetAdjacentMineCount(adjacentMines);
			ChangeAppearance(false, true, false);

		}

        game.CheckWin();

    }

    private void ChangeAppearance(bool grassVisible, bool countVisible, bool mineVisible) {

        transform.Find("Grass").gameObject.SetActive(grassVisible);
		transform.Find("Mine Count").gameObject.SetActive(countVisible);
		transform.Find("Mine").gameObject.SetActive(mineVisible);

    }

    public void ShowMine() {

        ChangeAppearance(false, false, true);

    }

    public ZoneScript[] GetAdjacentZones() {

        var zones = new List<ZoneScript>();

        int x = (int) transform.position.x;
        int y = (int) transform.position.y;

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {

                int zoneCheckX = x - 1 + i;
                int zoneCheckY = y - 1 + j;

                // ignore current zone
                if (zoneCheckX == x && zoneCheckY == y) continue;

                // ignore values outside of the game
                if (
                    zoneCheckX < 0 ||
                    zoneCheckX >= game.width ||
                    zoneCheckY < 0 ||
                    zoneCheckY >= game.height
                ) continue;

                zones.Add(game.grid[zoneCheckX, zoneCheckY]);

            }
        }

        return zones.ToArray();

    }

    private int CountAdjacentMines() {

        int count = 0;
        
        foreach(ZoneScript zone in GetAdjacentZones())
            if (zone.hasMine) count++;

        return count;

    }

    private void SetAdjacentMineCount(int n) {

        TextMeshPro textComponent = transform.Find("Mine Count").gameObject.GetComponent<TextMeshPro>();
        textComponent.text = n.ToString();

    }

}
