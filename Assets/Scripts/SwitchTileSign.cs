using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTileSign : MonoBehaviour
{
    private bool isNegative = false;
    public GameObject tile;

    public Sprite posTile;
    public Sprite negTile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ray cast from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider == tile.GetComponent<BoxCollider2D>())
            {
                if (!isNegative)
                {
                    this.GetComponent<SpriteRenderer>().sprite = negTile;
                    isNegative = true;
                }

                else
                {
                    this.GetComponent<SpriteRenderer>().sprite = posTile;
                    isNegative = false;
                }
            }
        }
    }
}
