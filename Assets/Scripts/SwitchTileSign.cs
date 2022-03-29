using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTileSign : MonoBehaviour
{

    public GameObject negated;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ray cast from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider == this.GetComponentInParent<BoxCollider2D>())
            {
                GameObject newTile = Instantiate(negated, this.transform.position, this.transform.rotation);
                newTile.transform.parent = this.transform.parent;
                Destroy(this.transform.gameObject);
            }
        }
    }
}
