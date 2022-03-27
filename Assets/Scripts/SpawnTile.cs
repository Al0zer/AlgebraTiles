using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{

    public GameObject tile;
    private bool inWorkSpace = false;
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
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(camera.transform.position.z);
            Vector3 tilePos = camera.ScreenToWorldPoint(mousePos);
            tilePos.z = 0f;
            Instantiate(tile, tilePos, Quaternion.identity);
        }
    }
}
