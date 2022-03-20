using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolMath : MonoBehaviour
{
    public enum OperationType {
        SOLVE,
        SUBSTITUTE,
        EXPAND,
        FACTOR
    }
    
    public OperationType operation;
    public int left_XSquared;
    public int left_X;
    public int left_ONE;
    public int right_XSquared;
    public int right_X;
    public int right_ONE;

    public int substitute_x_value;
    void Start()
    {
        SymbolicEquation eq;
        switch (operation)
        {
            case OperationType.SOLVE:
                eq = new SymbolicMathProblem.SolveTypeProblem(left_X, left_ONE, right_X, right_ONE);
                Debug.Log("Question: Solve "+eq.ToString());
                Debug.Log("Answer: x="+SymbolicMathProblemSolver.DoSolve((SymbolicMathProblem.SolveTypeProblem)eq));
                break;
            case OperationType.SUBSTITUTE:
                eq = new SymbolicMathProblem.SubstituteTypeProblem(substitute_x_value, left_X, left_ONE);
                Debug.Log("Question: Substitute "+eq.ToString());
                Debug.Log("Answer: "+SymbolicMathProblemSolver.DoSubstitute((SymbolicMathProblem.SubstituteTypeProblem)eq));
                break;
            case OperationType.EXPAND:
                eq = new SymbolicMathProblem.ExpandTypeProblem(left_X, left_ONE, right_X, right_ONE);
                Debug.Log("Question: Expand "+eq.ToString());
                Debug.Log("Answer: "+SymbolicMathProblemSolver.DoExpand((SymbolicMathProblem.ExpandTypeProblem)eq));
                break;
            case OperationType.FACTOR:
                eq = new SymbolicMathProblem.FactorTypeProblem(left_XSquared, left_X, left_ONE);
                Debug.Log("Question: Factor "+eq.ToString());
                Debug.Log("Answer: "+SymbolicMathProblemSolver.DoFactor((SymbolicMathProblem.FactorTypeProblem)eq));
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
