using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{

    public GameObject tile;

    public GameObject square;
    Camera camera;



    public void Awake()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //spawn a tile where the mouse cursor is
        if (Input.GetMouseButtonDown(0))
        {
            // ray cast from the camera to the mouse position
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            // modify this to allow multiple squares if necessary, or to allow changing which square is being checked
            if(hit.collider == square.GetComponent<BoxCollider2D>()){
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Mathf.Abs(camera.transform.position.z);
                Vector3 tilePos = camera.ScreenToWorldPoint(mousePos);
                tilePos.z = -1f;
                Instantiate(tile, tilePos, Quaternion.identity);
            }   
        }
    }
}
