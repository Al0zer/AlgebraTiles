using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveProblemEvaluator : MonoBehaviour
{

    public float horizontalDivider;
    // the parent of where all the children are
    public GameObject workSpace;

    public SymbolicMathProblem.SolveTypeProblem EvaluateWorkSpace(){
        int leftSideOneCount = 0;
        int leftSideXCount = 0;
        int rightSideOneCount = 0;
        int rightSideXCount = 0;

        foreach(Transform child in workSpace.transform){
            GameObject childObject = child.gameObject;

            if(childObject.layer == LayerMask.NameToLayer("Tile")){
                if(childObject.transform.position.x < horizontalDivider){
                    if(childObject.CompareTag("PositiveOne")){
                        leftSideOneCount++;
                    }else if (childObject.CompareTag("NegativeOne")){
                        leftSideOneCount--;
                    }
                    else if(childObject.CompareTag("PositiveX")){
                        leftSideXCount++;
                    }
                    else if (childObject.CompareTag("NegativeX")){
                        leftSideXCount--;
                    }
                }
                else{
                    if(childObject.CompareTag("PositiveOne")){
                        rightSideOneCount++;
                    }else if (childObject.CompareTag("NegativeOne")){
                        rightSideOneCount--;
                    }
                    else if(childObject.CompareTag("PositiveX")){
                        rightSideXCount++;
                    }
                    else if (childObject.CompareTag("NegativeX")){
                        rightSideXCount--;
                    }
                }
            }
        }

        return new SymbolicMathProblem.SolveTypeProblem(leftSideXCount, leftSideOneCount, rightSideXCount, rightSideOneCount);
    }
}
