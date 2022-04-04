using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileCreationManager : MonoBehaviour
{
    public GameObject onesTile;
    public GameObject xTile;

    public GameObject allowedArea;
    private bool onesTileOn = false;
    private bool xTileOn = false;
    private Camera viewCamera;

    // Start is called before the first frame update
    void Start()
    {
        viewCamera = Camera.main;
    }

    //if the ones tile button was pressed
    public void OnesTile(Toggle activated)
    {
        //if tiles button hasn't already been pressed
        onesTileOn = activated.isOn;
        xTileOn = false;
    }

    public bool OnesTileOn()
    {
        return onesTileOn;
    }

    //if the x tile button was pressed
    public void XTile(Toggle activated)
    {
        xTileOn = activated.isOn;
        onesTileOn = false;
    }

    public bool XTileOn()
    {
        return xTileOn;
    }

    //remove all tiles from the workspace
    public void ClearButton()
    {
        foreach (Transform obj in allowedArea.transform)
        {
            Destroy(obj.gameObject);
        }
    }
    public void DisableTileCreation(){
        onesTileOn = false;
        xTileOn = false;
    }

    void Update()
    {
        //spawn a tile where the mouse cursor is
        if (Input.GetMouseButtonDown(0) &&(onesTileOn || xTileOn))
        {
            // ray cast from the camera to the mouse position
            Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            // modify this to allow multiple squares if necessary, or to allow changing which workspace is being checked
            if (hit.collider == allowedArea.GetComponent<BoxCollider2D>())
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(viewCamera.transform.position.z);
                Vector3 tilePos = viewCamera.ScreenToWorldPoint(mousePos);
                tilePos.z = -1.5f;

                if (onesTileOn)
                {
                    GameObject newTile = Instantiate(onesTile, tilePos, Quaternion.identity);
                    newTile.transform.parent = hit.collider.transform;
                    TileSnapping.trySnap(newTile);
                }
                else if (xTileOn)
                {
                    GameObject newTile = Instantiate(xTile, tilePos, Quaternion.identity);
                    newTile.transform.parent = hit.collider.transform;
                    TileSnapping.trySnap(newTile);
                }
            }  
        }
    }
}
