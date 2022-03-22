using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class SymbolicEquation
{
    public class InvalidOperationException : System.Exception
    {
        public InvalidOperationException(string message) : base(message)
        {
        }
    }
    public enum MathSymbol {
        ONE,
        X,
        X_SQUARED
    }

    public Dictionary<MathSymbol, int> leftSide;
    public Dictionary<MathSymbol, int> rightSide;

    protected SymbolicEquation() {
        leftSide = new Dictionary<MathSymbol, int>();
        leftSide[MathSymbol.ONE] = 0;
        leftSide[MathSymbol.X] = 0;
        leftSide[MathSymbol.X_SQUARED] = 0;
        rightSide = new Dictionary<MathSymbol, int>();
        rightSide[MathSymbol.ONE] = 0;
        rightSide[MathSymbol.X] = 0;
        rightSide[MathSymbol.X_SQUARED] = 0;
    }

    protected SymbolicEquation(int left_XSquared, int left_X, int left_ONE, int right_XSquared, int right_X, int right_ONE) {
        leftSide = new Dictionary<MathSymbol, int>();
        leftSide[MathSymbol.ONE] = left_ONE;
        leftSide[MathSymbol.X] = left_X;
        leftSide[MathSymbol.X_SQUARED] = left_XSquared;
        rightSide = new Dictionary<MathSymbol, int>();
        rightSide[MathSymbol.ONE] = right_ONE;
        rightSide[MathSymbol.X] = right_X;
        rightSide[MathSymbol.X_SQUARED] = right_XSquared;
    }

    public void AddToBothSides(MathSymbol symbol, int amount) {
        this.rightSide[symbol] += amount;
        this.leftSide[symbol] += amount;
    }

    public void MultiplyBothSides(MathSymbol symbol, int amount){
        switch(symbol){
            case MathSymbol.X_SQUARED:
                if(this.leftSide[MathSymbol.X] != 0 || this.rightSide[MathSymbol.X] != 0){
                    throw new InvalidOperationException("Cannot multiply X by X squared (cubics unsupported");
                }
                if(this.leftSide[MathSymbol.X_SQUARED] != 0 || this.rightSide[MathSymbol.X_SQUARED] != 0){
                    throw new InvalidOperationException("Cannot multiply X squared by X squared (quartics unsupported");
                }
                this.leftSide[MathSymbol.X_SQUARED] = this.leftSide[MathSymbol.ONE] * amount;
                this.rightSide[MathSymbol.X_SQUARED] = this.rightSide[MathSymbol.ONE] * amount;

                this.leftSide[MathSymbol.ONE] = 0;
                this.rightSide[MathSymbol.ONE] = 0;
                break;
            case MathSymbol.X:
                if(this.leftSide[MathSymbol.X_SQUARED] != 0 || this.rightSide[MathSymbol.X_SQUARED] != 0){
                    throw new InvalidOperationException("Attempt to multiply X squared by X (cubics unsupported");
                }

                this.leftSide[MathSymbol.X_SQUARED] = this.leftSide[MathSymbol.X] * amount;
                this.rightSide[MathSymbol.X_SQUARED] = this.rightSide[MathSymbol.X] * amount;

                this.leftSide[MathSymbol.X] = this.leftSide[MathSymbol.ONE] * amount;
                this.rightSide[MathSymbol.X] = this.rightSide[MathSymbol.ONE] * amount;

                break;
            case MathSymbol.ONE:
                this.leftSide[MathSymbol.X_SQUARED] *= amount;
                this.rightSide[MathSymbol.X_SQUARED] *= amount;
                this.leftSide[MathSymbol.X] *= amount;
                this.rightSide[MathSymbol.X] *= amount;
                this.leftSide[MathSymbol.ONE] *= amount;
                this.rightSide[MathSymbol.ONE] *= amount;
                break;
            default:
                throw new InvalidOperationException("Attempt to multiply by unsupported symbol");

        }
    }

    public void DivideBothSides(MathSymbol symbol, int amount){
        switch(symbol){
            case MathSymbol.X_SQUARED:
                if(this.leftSide[MathSymbol.X] != 0 || this.rightSide[MathSymbol.X] != 0){
                    throw new InvalidOperationException("Attempt to divide X by X squared (fractions unsupported");
                }
                if(this.leftSide[MathSymbol.ONE] != 0 || this.rightSide[MathSymbol.ONE] != 0){
                    throw new InvalidOperationException("Attempt to divide 1 by X squared (fractions unsupported");
                }

                this.leftSide[MathSymbol.ONE] = this.leftSide[MathSymbol.X_SQUARED] / amount;
                this.rightSide[MathSymbol.ONE] = this.rightSide[MathSymbol.X_SQUARED] / amount;

                this.leftSide[MathSymbol.X_SQUARED] = 0;
                this.rightSide[MathSymbol.X_SQUARED] = 0;
                break;
            case MathSymbol.X:
                if(this.leftSide[MathSymbol.ONE] != 0 || this.rightSide[MathSymbol.ONE] != 0){
                    throw new InvalidOperationException("Attempt to divide non-X term by X (fractions unsupported");
                }

                this.leftSide[MathSymbol.ONE] = this.leftSide[MathSymbol.X] / amount;
                this.rightSide[MathSymbol.ONE] = this.rightSide[MathSymbol.X] / amount;

                this.leftSide[MathSymbol.X] = this.leftSide[MathSymbol.X_SQUARED] / amount;
                this.rightSide[MathSymbol.X] = this.rightSide[MathSymbol.X_SQUARED] / amount;

                this.leftSide[MathSymbol.X_SQUARED] = 0;
                this.rightSide[MathSymbol.X_SQUARED] = 0;

                break;
            case MathSymbol.ONE:
                this.leftSide[MathSymbol.X_SQUARED] /= amount;
                this.rightSide[MathSymbol.X_SQUARED] /= amount;
                this.leftSide[MathSymbol.X] /= amount;
                this.rightSide[MathSymbol.X] /= amount;
                this.leftSide[MathSymbol.ONE] /= amount;
                this.rightSide[MathSymbol.ONE] /= amount;
                break;
            default:
                throw new InvalidOperationException("Attempt to divide by unsupported symbol");
        }
    }

    public void MoveToLeftSide(MathSymbol symbol, int amount) {
        amount = Math.Abs(amount);
        if(Math.Abs(this.rightSide[symbol]) < amount){
            throw new InvalidOperationException("Not enough of " + symbol + " on the right side");
        }

        if(this.rightSide[symbol] > 0){
            this.AddToBothSides(symbol, -amount);
        }
        else{
            this.AddToBothSides(symbol, amount);
        }
    }

    public void MoveToRightSide(MathSymbol symbol, int amount) {
        amount = Math.Abs(amount);
        if(Math.Abs(this.leftSide[symbol]) < amount){
            throw new InvalidOperationException("Not enough of " + symbol + " on the left side");
        }

        if(this.leftSide[symbol] > 0){
            this.AddToBothSides(symbol, -amount);
        }
        else{
            this.AddToBothSides(symbol, amount);
        }
    }

    public static String equationSideToString(Dictionary<MathSymbol, int> equationSide) {
        String out_string = "";
        if(equationSide[MathSymbol.X_SQUARED] != 0){
            if(Math.Abs(equationSide[MathSymbol.X_SQUARED]) > 1){
                out_string += equationSide[MathSymbol.X_SQUARED] + "x^2";
            }
            else{
                out_string += "x^2";
            }
        }
        if(equationSide[MathSymbol.X] != 0){
            if(equationSide[MathSymbol.X_SQUARED] != 0){
                out_string += " + ";
            }
            if(Math.Abs(equationSide[MathSymbol.X]) > 1){
                out_string += equationSide[MathSymbol.X] + "x";
            }
            else{
                out_string += "x";
            }
        }
        if(equationSide[MathSymbol.ONE] != 0){
            if(equationSide[MathSymbol.X_SQUARED] != 0 || equationSide[MathSymbol.X] != 0){
                out_string += " + ";
            }
            out_string += equationSide[MathSymbol.ONE];
        }else if(equationSide[MathSymbol.X_SQUARED] == 0 && equationSide[MathSymbol.X] == 0){
            out_string += "0";
        }
        return out_string;
    }
    
    override public String ToString() {
        String out_string = "";
        out_string += equationSideToString(this.leftSide);
        out_string += " = ";
        out_string += equationSideToString(this.rightSide);
        return out_string;
    }
}
