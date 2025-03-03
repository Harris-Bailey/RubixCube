using UnityEngine;
using RubixCube.Core;

public class CubeInputHandler {

    public Move GetMoveFromInput() {
        if (!Input.anyKeyDown)
            return Move.NullMove;
        
        string moveSuffix = "";
        
        if (Input.GetKeyDown(KeyCode.Tab)) {
            // add a 2 so it rotates twice in the core
            moveSuffix = "2";
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift)) {
            // add an apostrophe so it rotates counter clockwise in the core
            moveSuffix = "'";
        }
        
        bool wideMoveActive = Input.GetKeyDown(KeyCode.LeftAlt);
        
        if (Input.GetKeyDown(KeyCode.U)) {
            string faceLetter = wideMoveActive ? "u" : "U";
            return new Move($"{faceLetter}{moveSuffix}");
        }
        else if (Input.GetKeyDown(KeyCode.F)) {
            string faceLetter = wideMoveActive ? "f" : "F";
            return new Move($"{faceLetter}{moveSuffix}");
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            string faceLetter = wideMoveActive ? "r" : "R";
            return new Move($"{faceLetter}{moveSuffix}");
        }
        else if (Input.GetKeyDown(KeyCode.B)) {
            string faceLetter = wideMoveActive ? "b" : "B";
            return new Move($"{faceLetter}{moveSuffix}");
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            string faceLetter = wideMoveActive ? "l" : "L";
            return new Move($"{faceLetter}{moveSuffix}");
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            string faceLetter = wideMoveActive ? "d" : "D";
            return new Move($"{faceLetter}{moveSuffix}");
        }
        
        else if (Input.GetKeyDown(KeyCode.M)) {
            return new Move($"M{moveSuffix}");
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            return new Move($"E{moveSuffix}");
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            return new Move($"S{moveSuffix}");
        }
        
        else if (Input.GetKeyDown(KeyCode.X)) {
            return new Move($"x{moveSuffix}");
        }
        else if (Input.GetKeyDown(KeyCode.Y)) {
            return new Move($"y{moveSuffix}");
        }
        else if (Input.GetKeyDown(KeyCode.Z)) {
            return new Move($"z{moveSuffix}");
        }
        
        return Move.NullMove;
    }
}