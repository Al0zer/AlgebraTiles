using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    public GameObject onesTileManager;
    public GameObject xTileManager;
    private bool tilesOn = false;

    // Start is called before the first frame update
    void Start()
    {
        onesTileManager.SetActive(false);
        xTileManager.SetActive(false);
    }

    //if the ones tile button was pressed
    public void OnesTile()
    {
        //if tiles button hasn't already been pressed
        if (!tilesOn)
        {
            onesTileManager.SetActive(true);
            tilesOn = true;
        }

        //tiles button was already pressed, now we're "turning off" the tiles
        else
        {
            onesTileManager.SetActive(false);
            tilesOn = false;
        }
    }

    //if the x tile button was pressed
    public void XTile()
    {
        //if tiles button hasn't already been pressed
        if (!tilesOn)
        {
            xTileManager.SetActive(true);
            tilesOn = true;
        }

        //tiles button was already pressed, now we're "turning off" the tiles
        else
        {
            xTileManager.SetActive(false);
            tilesOn = false;
        }
    }
}
