using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolicMathProblem
{
    public class SolveTypeProblem: SymbolicEquation{
        public SolveTypeProblem(int left_X, int left_ONE, int right_X, int right_ONE) : base(0, left_X, left_ONE, 0, right_X, right_ONE)
        {
        }
    }

    public class SubstituteTypeProblem: SymbolicEquation{
        public int xVal;
        public SubstituteTypeProblem(int xVal, int left_X, int left_ONE) : base(0, left_X, left_ONE, 0, 0, 0)
        {
            this.xVal = xVal;
        }

        public override string ToString()
        {
            return SymbolicEquation.equationSideToString(leftSide) + "; x = " + xVal;
        }
    }

    public class ExpandTypeProblem: SymbolicEquation{
        public ExpandTypeProblem(int left_X, int left_ONE, int right_X, int right_ONE) : base(0, left_X, left_ONE, 0, right_X, right_ONE)
        {
        }

        public override string ToString()
        {
            return "(" + SymbolicEquation.equationSideToString(leftSide) + ")(" + SymbolicEquation.equationSideToString(rightSide)+")";
        }
    }

    public class FactorTypeProblem: SymbolicEquation{
        public FactorTypeProblem(int left_X_Squared, int left_X, int left_ONE) : base(left_X_Squared, left_X, left_ONE, 0, 0, 0)
        {
        }

        public override string ToString()
        {
            return SymbolicEquation.equationSideToString(this.leftSide);
        }
    }
}
