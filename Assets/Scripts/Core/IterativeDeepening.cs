namespace RubixCube.Core {

    public class IterativeDeepening {
        
        private MoveStack stack;
        private Cube cube;
        private bool goalFound;
        private Move[] possibleMoves;
        CubeMask solvedState;
        int nodesSearched;
        
        public IterativeDeepening(Cube cube) {
            stack = new MoveStack(0);
            this.cube = cube;
            solvedState = new CubeMask(cube.BitboardsMatchingCenters,
                0b111111111ul,
                0b111111111ul << 9,
                0b111111111ul << 18,
                0b111111111ul << 27,
                0b111111111ul << 36,
                0b111111111ul << 45
            );
            possibleMoves = new Move[] {
                new Move("U"), new Move("U2"), new Move("U'"),
                new Move("F"), new Move("F2"), new Move("F'"),
                new Move("R"), new Move("R2"), new Move("R'"),
                new Move("B"), new Move("B2"), new Move("B'"),
                new Move("L"), new Move("L2"), new Move("L'"),
                new Move("D"), new Move("D2"), new Move("D'")
            };
        }
        
        public void Run(int depth) {
            stack = new MoveStack(depth);
            nodesSearched = 0;
            
            goalFound = false;
            for (int i = 0; i <= depth; i++) {
                if (goalFound)
                    break;
                SearchToDepth(0, i, solvedState);
            }
            
        }
        
        public Move[] Run(CubeMask goalState, int depth, bool debug = false) {
            stack = new MoveStack(depth);
            goalFound = false;
            nodesSearched = 0;
            for (int i = 0; i <= depth; i++) {
                if (goalFound)
                    break;
                SearchToDepth(0, i, goalState);
            }

            return stack.Moves;
        }
        
        public void SearchToDepth(int currentDepth, int maxDepth, CubeMask goalState) {
            nodesSearched++;
            if (cube.MaskMatchesCubeState(goalState)) {
                goalFound = true;
                return;
            }

            if (currentDepth >= maxDepth)
                return;
            
            int lastMoveFaceIndex = stack.PeekLast().rotationIndex;
            int secondLastMoveFaceIndex = stack.PeekSecondLast().rotationIndex;
            
            CubeMask currentState = new CubeMask(cube.GetState());
            foreach (Move move in possibleMoves) {
                // if the last move was moving the same face, we can get there quicker by doing a double rotation
                if (move.rotationIndex == lastMoveFaceIndex)
                    continue;
                // Example: R L R - whereas we could've done: R2 L
                // so don't process this move since we can use 2 moves instead of 3 to get to the same point
                int oppositeFaceIndex = GetMoveOppositeFaceIndex(move);
                if (oppositeFaceIndex != -1 && move.rotationIndex == secondLastMoveFaceIndex && oppositeFaceIndex == lastMoveFaceIndex)
                    continue;
                
                if (goalFound)
                    return;

                cube.Rotate(move);
                stack.Push(move);
                SearchToDepth(currentDepth + 1, maxDepth, goalState);
                cube.SetState(currentState.ColouredFaceletBitboards);
                
                if (!goalFound)
                    stack.Pop();
            }
            
        }
        
        private int GetMoveOppositeFaceIndex(Move move) {
            // only rotations of faces have opposites that don't interact
            return move.rotationIndex switch {
                Move.TopFaceRotationIndex => Move.BottomFaceRotationIndex,
                Move.FrontFaceRotationIndex => Move.BackFaceRotationIndex,
                Move.RightFaceRotationIndex => Move.LeftFaceRotationIndex,
                Move.BackFaceRotationIndex => Move.FrontFaceRotationIndex,
                Move.LeftFaceRotationIndex => Move.RightFaceRotationIndex,
                Move.BottomFaceRotationIndex => Move.TopFaceRotationIndex,
                _ => -1,
            };
        }
    }
}