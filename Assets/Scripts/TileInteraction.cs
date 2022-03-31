using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        
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
                Debug.Log("Hit "+LayerMask.LayerToName(hit.transform.gameObject.layer));
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Tile"))
                {
                    Debug.Log("Hit tile");
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
            if(!dragging)
            {
                // not dragging, just a click
                targetTile.GetComponent<SwitchTileSign>().SwitchSign();
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
}
