using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTileSign : MonoBehaviour
{

    public GameObject negated;
    

    public GameObject SwitchSign(){
        GameObject newTile = Instantiate(negated, this.transform.position, this.transform.rotation);
        newTile.transform.parent = this.transform.parent;
        Destroy(this.transform.gameObject);
        return newTile;
    }
}
