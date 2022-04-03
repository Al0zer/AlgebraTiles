using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSnapping
{
     public static void trySnap(GameObject tile){
        // cast a ray from each corner of the block. If it intersects another, snap it to the other block
                float extension = 0.01f;
                float width = tile.GetComponent<BoxCollider2D>().size.x * tile.transform.lossyScale.x;
                float height = tile.GetComponent<BoxCollider2D>().size.y * tile.transform.lossyScale.y;
                for(int i = 0; i < 4; i++)
                {
                    Vector2 center = tile.transform.position;
                    
                    switch(i)
                    {
                        case 0:
                            // bottom right
                            center.x += width/2 + extension;
                            center.y += height/2 + extension;
                            break;
                        case 1:
                            // top right
                            center.x += width/2 + extension;
                            center.y += -height/2 - extension;
                            break;
                        case 2:
                            // bottom left
                            center.x += -width/2 - extension;
                            center.y += height/2 + extension;
                            break;
                        case 3:
                            // top left
                            center.x += -width/2 - extension;
                            center.y += -height/2 - extension;
                            break;
                    }

                    Ray cornerRay = new Ray(center, new Vector3(0,0,1));
                    Debug.DrawRay(cornerRay.origin, cornerRay.direction, Color.red, 10);
                    RaycastHit2D cornerHit;

                    cornerHit = Physics2D.Raycast(cornerRay.origin, cornerRay.direction, Mathf.Infinity, LayerMask.GetMask("Tile"));

                    if(cornerHit.transform != null)
                    {
                        if(cornerHit.transform.gameObject != tile)
                        {
                            if(tile.transform.position.x > cornerHit.transform.position.x)
                            {
                                // target tile is to the right of the hit tile

                                // check if tile is within 33% of the top or bottom of the other tile
                                if(cornerHit.transform.position.y > tile.transform.position.y + height/3)
                                {
                                    // below
                                    TileSnapping.snapBottom(cornerHit.transform.gameObject, tile);
                                }
                                else if(cornerHit.transform.position.y < tile.transform.position.y - height/3)
                                {
                                    // above
                                    TileSnapping.snapTop(cornerHit.transform.gameObject, tile);
                                }else{
                                    // within threshold
                                    TileSnapping.snapRight(cornerHit.transform.gameObject, tile);
                                }
                            }
                            else
                            {
                                // target tile is to the left of the hit tile

                                // check if tile is within 33% of the top or bottom of the other tile
                                if (cornerHit.transform.position.y > tile.transform.position.y + height / 3)
                                {
                                    // below
                                    TileSnapping.snapBottom(cornerHit.transform.gameObject, tile);
                                }
                                else if (cornerHit.transform.position.y < tile.transform.position.y - height / 3)
                                {
                                    // above
                                    TileSnapping.snapTop(cornerHit.transform.gameObject, tile);
                                }
                                else
                                {
                                    // within threshold
                                    TileSnapping.snapLeft(cornerHit.transform.gameObject, tile);
                                }
                            }
                            break;
                        }
                    }
                }
    }

    private static void snapRight(GameObject baseTile, GameObject snapTile){
        // snap to right side of tile
        Vector3 newPosition = baseTile.transform.position;
        newPosition.x += baseTile.transform.GetComponent<BoxCollider2D>().size.x/2 * baseTile.transform.lossyScale.x + snapTile.GetComponent<BoxCollider2D>().size.x/2 * snapTile.transform.lossyScale.x;
        snapTile.transform.position = newPosition;
    }

    private static void snapLeft(GameObject baseTile, GameObject snapTile){
        // snap to left side of tile
        Vector3 newPosition = baseTile.transform.position;
        newPosition.x -= baseTile.transform.GetComponent<BoxCollider2D>().size.x/2 * baseTile.transform.lossyScale.x + snapTile.GetComponent<BoxCollider2D>().size.x/2 * snapTile.transform.lossyScale.x;
        snapTile.transform.position = newPosition;
    }

    private static void snapTop(GameObject baseTile, GameObject snapTile){
        // snap to top of tile
        Vector3 newPosition = baseTile.transform.position;
        newPosition.y += baseTile.transform.GetComponent<BoxCollider2D>().size.y/2 * baseTile.transform.lossyScale.y + snapTile.GetComponent<BoxCollider2D>().size.y/2 * snapTile.transform.lossyScale.y;
        snapTile.transform.position = newPosition;
    }

    private static void snapBottom(GameObject baseTile, GameObject snapTile){
        // snap to bottom of tile
        Vector3 newPosition = baseTile.transform.position;
        newPosition.y -= baseTile.transform.GetComponent<BoxCollider2D>().size.y/2 * baseTile.transform.lossyScale.y + snapTile.GetComponent<BoxCollider2D>().size.y/2 * snapTile.transform.lossyScale.y;
        snapTile.transform.position = newPosition;
    }
}
