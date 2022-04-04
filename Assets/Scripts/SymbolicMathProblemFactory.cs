using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolicMathProblemGenerator
{
    public static SymbolicMathProblem.SolveTypeProblem generateSolveProblem(){
        int x = Random.Range(-5, 5);
        int a = Random.Range(-5, 5);
        int c = Random.Range(a, a+6);

        if(c > a+2){ // 3, 4, 5
            c -= 2;
        }else{ // 0, 1, 2
            c -= 3;
        }

        int b = Random.Range(0, 7);

        if(b > 3)
        {
            b -= 3;
        }
        else
        {
            b -= 4;
        }

        int d = (a-c) * x + b;

        if(d >= 8){
            b -= d/2;
            d -= d/2;
        }else if(d <= -8)
        {
            b += d / 2;
            d += d / 2;
        }
        
        // randomly swap sides of equation to make it look more diverse
        if(Random.Range(0, 1) == 1){
            return new SymbolicMathProblem.SolveTypeProblem(a, b, c, d);
        }else{
            return new SymbolicMathProblem.SolveTypeProblem(c, d, a, b);
        }
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
