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

    public bool PairsEliminated(){
        int leftSideNegativeOneCount = 0;
        int leftSidePositiveOneCount = 0;
        int rightSideNegativeOneCount = 0;
        int rightSidePositiveOneCount = 0;
        int leftSideNegativeXCount = 0;
        int leftSidePositiveXCount = 0;
        int rightSideNegativeXCount = 0;
        int rightSidePositiveXCount = 0;

        foreach(Transform child in workSpace.transform){
            // check which side of workspace each child is on
            if(child.position.x < horizontalDivider){
                // left side
                if(child.gameObject.CompareTag("NegativeOne")){
                    leftSideNegativeOneCount++;
                }else if(child.gameObject.CompareTag("PositiveOne")){
                    leftSidePositiveOneCount++;
                }else if(child.gameObject.CompareTag("NegativeX")){
                    leftSideNegativeXCount++;
                }else if(child.gameObject.CompareTag("PositiveX")){
                    leftSidePositiveXCount++;
                }
            }else{
                // right side
                if(child.gameObject.CompareTag("NegativeOne")){
                    rightSideNegativeOneCount++;
                }else if(child.gameObject.CompareTag("PositiveOne")){
                    rightSidePositiveOneCount++;
                }else if(child.gameObject.CompareTag("NegativeX")){
                    rightSideNegativeXCount++;
                }else if(child.gameObject.CompareTag("PositiveX")){
                    rightSidePositiveXCount++;
                }
            }
        }

        // check if there are tiles of one type on a side, there are not negative of that tile on the same side
        bool leftSideOnesEliminated = (leftSidePositiveOneCount == 0 && leftSideNegativeOneCount >= 0) || (leftSideNegativeOneCount == 0 && leftSidePositiveOneCount >= 0);
        bool leftSideXsEliminated = (leftSidePositiveXCount == 0 && leftSideNegativeXCount >= 0) || (leftSideNegativeXCount == 0 && leftSidePositiveXCount >= 0);
        bool rightSideOnesEliminated = (rightSidePositiveOneCount == 0 && rightSideNegativeOneCount >= 0) || (rightSideNegativeOneCount == 0 && rightSidePositiveOneCount >= 0);
        bool rightSideXsEliminated = (rightSidePositiveXCount == 0 && rightSideNegativeXCount >= 0) || (rightSideNegativeXCount == 0 && rightSidePositiveXCount >= 0);

        return leftSideOnesEliminated && leftSideXsEliminated && rightSideOnesEliminated && rightSideXsEliminated;
    }
}
