using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//does not work rn
public class SubProblemEvaluator : MonoBehaviour
{
    public GameObject workSpace;

    public void EvaluateWorkSpace()
    {
        int oneCount = 0;

        foreach(Transform child in workSpace.transform)
        {
            GameObject childObject = child.gameObject;

            if(childObject.layer == LayerMask.NameToLayer("Tile"))
            {
                if (childObject.CompareTag("PositiveOne"))
                {
                    oneCount++;
                }

                else if (childObject.CompareTag("NegativeOne"))
                {
                    oneCount--;
                }
            }
        }
        Debug.Log(oneCount);
    }
}
