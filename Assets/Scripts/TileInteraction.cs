using System.Collections.Generic;
using UnityEngine;

public class TileInteraction : MonoBehaviour
{

    public float dragDistance = 0.05f;

    public float horizontalDivider;

    public GameObject garbageBin;
    private Vector2 clickDownPosition;
    private GameObject targetTile;

    private bool leftSideOfWorkspace = false;

    private bool dragging = false;

    private List<GameObject> selectedTiles;
    private bool cancelOut = false;
    // Start is called before the first frame update
    void Start()
    {
        selectedTiles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clickDownPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;

            hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit.transform != null){
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Tile"))
                {
                    targetTile = hit.transform.gameObject;

                    leftSideOfWorkspace = targetTile.transform.position.x < horizontalDivider;
                }
            }
            else{
                targetTile = null;
            }
        }
        if(Input.GetMouseButtonUp(0) && targetTile != null)
        {
            // not dragging, just a click
            if(!dragging)
            {
                //canceling out tiles
                if (cancelOut)
                {
                    selectedTiles.Add(targetTile);

                    if(selectedTiles.Count > 2){
                        selectedTiles.RemoveAt(0);
                    }

                    if ((selectedTiles[0].tag == "PositiveOne" && selectedTiles[1].tag == "NegativeOne")
                        || (selectedTiles[0].tag == "NegativeOne"&& selectedTiles[1].tag == "PositiveOne")
                        || (selectedTiles[0].tag == "PositiveX" && selectedTiles[1].tag == "NegativeX")
                        || (selectedTiles[0].tag == "NegativeX" && selectedTiles[1].tag == "PositiveX"))
                    {
                        Destroy(selectedTiles[0]);
                        Destroy(selectedTiles[1]);
                        selectedTiles.Clear();
                    }
                

                //cancel out was not clicked, just switching tile signs
                }else{
                    targetTile.GetComponent<SwitchTileSign>().SwitchSign();
                }

            }else{
                // cast a ray and see if we hit garbage bin
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit;

                hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("UI"));

                if(hit.transform != null){
                    if(hit.transform.gameObject == garbageBin)
                    {
                        Destroy(targetTile);
                    }
                }

                TileSnapping.trySnap(targetTile);
            }
            targetTile = null;
            dragging = false;
        }

        if(targetTile != null)
        {
            // make tile follow mouse
            if(this.distanceFromButtonDown() > dragDistance)
            {
                dragging = true;
            }

            if(dragging){
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetTile.transform.position = new Vector3(mousePos.x, mousePos.y, targetTile.transform.position.z);

                if(mousePos.x > horizontalDivider && leftSideOfWorkspace){
                    targetTile = targetTile.GetComponent<SwitchTileSign>().SwitchSign();
                    leftSideOfWorkspace = false;
                }
                else if (mousePos.x < horizontalDivider && !leftSideOfWorkspace){
                    targetTile = targetTile.GetComponent<SwitchTileSign>().SwitchSign();
                    leftSideOfWorkspace = true;
                }
            }
        }
    }

    private float distanceFromButtonDown(){
        return Vector2.Distance(clickDownPosition, new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

   

    public void CancelOut()
    {
        //if cancel out button hasn't already been pressed
        if (!cancelOut)
        {
            cancelOut = true;
        }

        //cancel out was already pressed, now turning it off
        else
        {
            selectedTiles.Clear();
            cancelOut = false;
        }
    }

    public void ResetCancelOut()
    {
        cancelOut = false;
        selectedTiles = new List<GameObject>();
    }
}
