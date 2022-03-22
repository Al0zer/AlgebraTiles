using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolicMathProblemSolver
{

    
    public static int DoSolve(SymbolicMathProblem.SolveTypeProblem problem)
        {
        // Solves equations of the form: ax + b = cx + d
        if (problem.leftSide[SymbolicEquation.MathSymbol.X_SQUARED] != 0 || problem.rightSide[SymbolicEquation.MathSymbol.X_SQUARED] != 0) {
            throw new SymbolicEquation.InvalidOperationException("Cannot solve equations with x^2");
        }

        // Move all x's to the left side
        // Move all one's to the right side
        problem.MoveToLeftSide(SymbolicEquation.MathSymbol.X, problem.rightSide[SymbolicEquation.MathSymbol.X]);
        problem.MoveToRightSide(SymbolicEquation.MathSymbol.ONE, problem.leftSide[SymbolicEquation.MathSymbol.ONE]);

        // divide both sides by number of x's
        problem.DivideBothSides(SymbolicEquation.MathSymbol.ONE, problem.leftSide[SymbolicEquation.MathSymbol.X]);

        if(problem.leftSide[SymbolicEquation.MathSymbol.X] < 0){
            problem.MultiplyBothSides(SymbolicEquation.MathSymbol.ONE, -1);
        }
        return problem.rightSide[SymbolicEquation.MathSymbol.ONE];
    }

    public static int DoSubstitute(SymbolicMathProblem.SubstituteTypeProblem problem) {
        // substitute x and solve
        if (problem.rightSide[SymbolicEquation.MathSymbol.ONE] != 0 || problem.rightSide[SymbolicEquation.MathSymbol.X] != 0 || problem.rightSide[SymbolicEquation.MathSymbol.X_SQUARED] != 0){
            throw new SymbolicEquation.InvalidOperationException("Equation has values on right side");
        }
        
        return problem.leftSide[SymbolicEquation.MathSymbol.ONE] + problem.leftSide[SymbolicEquation.MathSymbol.X] * problem.xVal + problem.leftSide[SymbolicEquation.MathSymbol.X_SQUARED] * problem.xVal * problem.xVal;
    }

    public static SymbolicMathProblem.FactorTypeProblem DoExpand(SymbolicMathProblem.ExpandTypeProblem problem) {
        // the left side and right side of the equation are values in the parentheses to be expanded
        if(problem.leftSide[SymbolicEquation.MathSymbol.X_SQUARED] != 0 || problem.rightSide[SymbolicEquation.MathSymbol.X_SQUARED] != 0){
            throw new SymbolicEquation.InvalidOperationException("Cannot expand equations with x^2");
        }

        int new_left_x_squared = problem.leftSide[SymbolicEquation.MathSymbol.X] * problem.rightSide[SymbolicEquation.MathSymbol.X];
        int new_left_x = problem.leftSide[SymbolicEquation.MathSymbol.X] * problem.rightSide[SymbolicEquation.MathSymbol.ONE] + problem.leftSide[SymbolicEquation.MathSymbol.ONE] * problem.rightSide[SymbolicEquation.MathSymbol.X];
        int new_left_one = problem.leftSide[SymbolicEquation.MathSymbol.ONE] * problem.rightSide[SymbolicEquation.MathSymbol.ONE];
        return new SymbolicMathProblem.FactorTypeProblem(new_left_x_squared, new_left_x, new_left_one);
    }

    private static int GCD(int a, int b) {
        if (b == 0) {
            return a;
        }
        return GCD(b, a % b);
    }

    public static SymbolicMathProblem.ExpandTypeProblem DoFactor(SymbolicMathProblem.FactorTypeProblem problem) {
        if(problem.leftSide[SymbolicEquation.MathSymbol.X_SQUARED] > 1){
            problem.DivideBothSides(SymbolicEquation.MathSymbol.ONE, GCD(problem.leftSide[SymbolicEquation.MathSymbol.X_SQUARED], GCD(problem.leftSide[SymbolicEquation.MathSymbol.X], problem.leftSide[SymbolicEquation.MathSymbol.ONE])));
        }

        if(problem.leftSide[SymbolicEquation.MathSymbol.X_SQUARED] == 1) {
            // Term has an x^2
            if(problem.leftSide[SymbolicEquation.MathSymbol.ONE] == 0) {
                // ax^2 + bx
                int out_left_x = 1;
                int out_right_x = problem.leftSide[SymbolicEquation.MathSymbol.X_SQUARED];
                int out_right_one = problem.leftSide[SymbolicEquation.MathSymbol.X];
                return new SymbolicMathProblem.ExpandTypeProblem(out_left_x, 0, out_right_x, out_right_one);
            }else{
                for(int i = 1; i<problem.leftSide[SymbolicEquation.MathSymbol.ONE]; i++){
                    if(problem.leftSide[SymbolicEquation.MathSymbol.ONE] % i == 0){ // i and another number multiply to be ones
                        int remainder = problem.leftSide[SymbolicEquation.MathSymbol.ONE] / i;
                        if(i+remainder == problem.leftSide[SymbolicEquation.MathSymbol.X]){
                            int out_left_x = 1;
                            int out_left_one = i;
                            int out_right_x = 1;
                            int out_right_one = remainder;
                            return new SymbolicMathProblem.ExpandTypeProblem(out_left_x, out_left_one, out_right_x, out_right_one);
                        }
                    }
                }
            }
        }else{
            // bx+c
            int gcd = GCD(problem.leftSide[SymbolicEquation.MathSymbol.X], problem.leftSide[SymbolicEquation.MathSymbol.ONE]);
            int out_left_one = gcd;
            int out_right_x = problem.leftSide[SymbolicEquation.MathSymbol.X] / gcd;
            int out_right_one = problem.leftSide[SymbolicEquation.MathSymbol.ONE] / gcd;
            return new SymbolicMathProblem.ExpandTypeProblem(0, out_left_one, out_right_x, out_right_one);
        }
        throw new SymbolicEquation.InvalidOperationException("Cannot factor equation");
    }
}
