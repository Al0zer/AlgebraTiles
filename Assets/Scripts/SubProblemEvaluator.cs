using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//does not work rn
public class SubProblemEvaluator : MonoBehaviour
{
    public GameObject workSpace;

    public void EvaluateWorkSpace()
    {
        int xVal = 0;
        int leftSideOnes = 0;
        int leftSideX = 0;

        foreach(Transform child in workSpace.transform)
        {
            GameObject childObject = child.gameObject;

            if(childObject.layer == LayerMask.NameToLayer("Tile"))
            {
                if (childObject.CompareTag("PositiveOne"))
                {
                    xVal++;
                }

                else if (childObject.CompareTag("NegativeOne"))
                {
                    xVal--;
                }
            }
        }
        Debug.Log(xVal);
    }
}
