using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolicMathProblemGenerator
{
    public static SymbolicMathProblem.SolveTypeProblem generateSolveProblem(){
        int leftSideOnes = Random.Range(-5, 5);
        int leftSideX = Random.Range(-5, 5);
        int rightSideOnes = Random.Range(-5, 5);
        int rightSideX = Random.Range(-5, 5);
        return new SymbolicMathProblem.SolveTypeProblem(leftSideX, leftSideOnes, rightSideX, rightSideOnes);
    }

    public static SymbolicMathProblem.SubstituteTypeProblem generateSubstituteProblem() {
        int leftSideOnes = Random.Range(-5, 5);
        int leftSideX = Random.Range(-5, 5);
        int xVal = Random.Range(0, 9);
        return new SymbolicMathProblem.SubstituteTypeProblem(xVal, leftSideX, leftSideOnes);
    }

    public static SymbolicMathProblem.ExpandTypeProblem generateExpandProblem() {
        int leftSideOnes = Random.Range(-9, 9);
        int leftSideX = Random.Range(-9, 9);
        int rightSideOnes = Random.Range(-9, 9);
        int rightSideX = Random.Range(-9, 9);
        return new SymbolicMathProblem.ExpandTypeProblem(leftSideX, leftSideOnes, rightSideX, rightSideOnes);
    }

    public static SymbolicMathProblem.FactorTypeProblem generateFactorProblem() {
        SymbolicMathProblem.ExpandTypeProblem base_problem = generateExpandProblem();

        return SymbolicMathProblemSolver.DoExpand(base_problem);
    }
}
