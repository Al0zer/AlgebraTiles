using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInteraction : MonoBehaviour
{

    public float dragDistance = 0.05f;

    public float horizontalDivider;

    public GameObject workspace;
    private Vector2 clickDownPosition;

    private Vector3 tileStartPosition;
    private GameObject targetTile;

    private bool leftSideOfWorkspace = false;

    private bool dragging = false;

    private int signChangesDuringDrag = 0;

    public bool dragToDelete = true;

    public bool allowSignChange = true;

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

                    tileStartPosition = targetTile.transform.position;

                    signChangesDuringDrag = 0;
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

                    if(selectedTiles.Count == 2){

                        if ((selectedTiles[0].tag == "PositiveOne" && selectedTiles[1].tag == "NegativeOne")
                            || (selectedTiles[0].tag == "NegativeOne"&& selectedTiles[1].tag == "PositiveOne")
                            || (selectedTiles[0].tag == "PositiveX" && selectedTiles[1].tag == "NegativeX")
                            || (selectedTiles[0].tag == "NegativeX" && selectedTiles[1].tag == "PositiveX"))
                        {
                            Destroy(selectedTiles[0]);
                            Destroy(selectedTiles[1]);
                            selectedTiles.Clear();
                        }
                    }
                

                //cancel out was not clicked, just switching tile signs
                }else{
                    if(allowSignChange){
                        targetTile.GetComponent<SwitchTileSign>().SwitchSign();
                    }
                }

            }else{
                // cast a ray and see if we hit garbage bin
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit;

                hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("WorkSpace"));

                if(hit.transform == null){
                    if(dragToDelete){
                        Destroy(targetTile);
                    }
                    else{
                        targetTile.transform.position = tileStartPosition;

                        if(signChangesDuringDrag %2 == 1){
                            targetTile.GetComponent<SwitchTileSign>().SwitchSign();
                        }
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
                    signChangesDuringDrag += 1;
                }
                else if (mousePos.x < horizontalDivider && !leftSideOfWorkspace){
                    targetTile = targetTile.GetComponent<SwitchTileSign>().SwitchSign();
                    leftSideOfWorkspace = true;
                    signChangesDuringDrag += 1;
                }
            }
        }
    }

    private float distanceFromButtonDown(){
        return Vector2.Distance(clickDownPosition, new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

   

    public void CancelOut(Toggle change)
    {

        if (change.isOn)
        {
            cancelOut = true;
        }
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
