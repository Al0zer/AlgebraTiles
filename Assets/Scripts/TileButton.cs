using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    public GameObject spawnTileManager;
    private bool tilesOn = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnTileManager.SetActive(false);
    }

    public void Pressed()
    {
        //if tiles button hasn't already been pressed
        if (!tilesOn)
        {
            spawnTileManager.SetActive(true);
            tilesOn = true;
        }

        //tiles button was already pressed, now we're "turning off" the tiles
        else
        {
            spawnTileManager.SetActive(false);
            tilesOn = false;
        }
    }
}
