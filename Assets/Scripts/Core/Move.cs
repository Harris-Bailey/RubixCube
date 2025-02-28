using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace RubixCube.Core {
    public readonly struct Move {
        private static readonly Random rand = new Random();
        
        public enum Flag {
            FaceRotation,
            SliceRotation,
            WideRotation,
            CubeRotation
        }
        
        public readonly int rotationIndex;
        public readonly int rotationTypeIndex;
        public readonly Flag rotationFlag;
        
        public const int TopFaceRotationIndex = 0;
        public const int FrontFaceRotationIndex = 1;
        public const int RightFaceRotationIndex = 2;
        public const int BackFaceRotationIndex = 3;
        public const int LeftFaceRotationIndex = 4;
        public const int BottomFaceRotationIndex = 5;
        public const int MSliceRotationIndex = 6;
        public const int ESliceRotationIndex = 7;
        public const int SSliceRotationIndex = 8;
        public const int TopWideRotationIndex = 9;
        public const int FrontWideRotationIndex = 10;
        public const int RightWideRotationIndex = 11;
        public const int BackWideRotationIndex = 12;
        public const int LeftWideRotationIndex = 13;
        public const int BottomWideRotationIndex = 14;
        public const int XCubeRotationIndex = 15;
        public const int YCubeRotationIndex = 16;
        public const int ZCubeRotationIndex = 17;
        
        public const int ClockwiseRotation = 0;
        public const int DoubleRotation = 1;
        public const int CounterClockwiseRotation = 2;
        public readonly bool IsNullMove => rotationIndex == -1 && rotationTypeIndex == -1;
        
        public Move(int rotationIndex, int rotationTypeIndex) {
            this.rotationIndex = rotationIndex;
            this.rotationTypeIndex = rotationTypeIndex;
            rotationFlag = GetRotationFlag(rotationIndex);
        }
        
        public Move(string moveInNotation) {
            moveInNotation = moveInNotation.Trim();
            // Console.WriteLine(moveInNotation[0]);
            rotationIndex = moveInNotation[0] switch {
                'U' => TopFaceRotationIndex,
                'F' => FrontFaceRotationIndex,
                'R' => RightFaceRotationIndex,
                'B' => BackFaceRotationIndex,
                'L' => LeftFaceRotationIndex,
                'D' => BottomFaceRotationIndex,
                'M' => MSliceRotationIndex,
                'E' => ESliceRotationIndex,
                'S' => SSliceRotationIndex,
                'u' => TopWideRotationIndex,
                'f' => FrontWideRotationIndex,
                'r' => RightWideRotationIndex,
                'b' => BackWideRotationIndex,
                'l' => LeftWideRotationIndex,
                'd' => BottomWideRotationIndex,
                'x' => XCubeRotationIndex,
                'y' => YCubeRotationIndex,
                'z' => ZCubeRotationIndex,
                _ => throw new ArgumentException($"{moveInNotation} cannot be converted to a valid rotation index"),
            };
            // set it to clockwise by default
            rotationTypeIndex = ClockwiseRotation;
            // if there's an indiciator for double or counter clockwise, set it to the new rotation type
            if (moveInNotation.Length > 1) {
                rotationTypeIndex = moveInNotation[^1] switch {
                    '2' => DoubleRotation,
                    '\'' => CounterClockwiseRotation,
                    _ => throw new ArgumentException($"{moveInNotation} cannot be converted to a valid rotation type"),
                };
            }
            rotationFlag = Flag.FaceRotation;
        }
        
        public readonly Move GetOppositeRotationMove() {
            int oppositeRotationType = GetOppositeRotationType();
            return new Move(rotationIndex, oppositeRotationType);
        }
        
        private readonly int GetOppositeRotationType() {
            // if it's a double move then its undo is the same rotation
            if (rotationTypeIndex == 1)
                return rotationTypeIndex;

            // 0 would turn into 2, 2 would turn into 0
            // 0b11 ^ 0b11 = 0b0
            // 0b0 ^ 0b11 = 0b11
            return rotationTypeIndex ^ 0b10;
        }

        public override readonly string ToString() {
            char faceChar = rotationIndex switch {
                TopFaceRotationIndex => 'U',
                FrontFaceRotationIndex => 'F',
                RightFaceRotationIndex => 'R',
                BackFaceRotationIndex => 'B',
                LeftFaceRotationIndex => 'L',
                BottomFaceRotationIndex => 'D',
                MSliceRotationIndex => 'M',
                ESliceRotationIndex => 'E',
                SSliceRotationIndex => 'S',
                TopWideRotationIndex => 'u',
                FrontWideRotationIndex => 'f',
                RightWideRotationIndex => 'r',
                BackWideRotationIndex => 'b',
                LeftWideRotationIndex => 'l',
                BottomWideRotationIndex => 'd',
                XCubeRotationIndex => 'x',
                YCubeRotationIndex => 'y',
                ZCubeRotationIndex => 'z',
                _ => '?',
            };
            string rotationChar = rotationTypeIndex switch {
                0 => "",
                1 => "2",
                2 => "'",
                _ => "?",
            };
            return $"{faceChar}{rotationChar}";
        }
        
        public static Move NullMove => new Move(-1, -1);
        
        public static Move[] GetMovesFromSequence(string moveSequence) {
            if (moveSequence == string.Empty)
                return Array.Empty<Move>();
                
            string[] splitSequence = moveSequence.Split(' ');
            Move[] moves = new Move[splitSequence.Length];

            for (int i = 0; i < splitSequence.Length; i++) {
                string moveNotation = splitSequence[i];
                moves[i] = new Move(moveNotation);
            }
            return moves;
        }
        
        // todo:
        // combine moves that are in the form as: 1 2 1 where 1 is a face rotation and 2 is the opposite (e.g. R L R -> R2 L)
        // remove all moves that return the cube to a state earlier in the sequence (e.g. "R U U' R'")
        public static Move[] CleanSequence(Move[] sequence) {
            List<Move> cleanedSequence = new List<Move>();
            for (int i = 0; i < sequence.Length; ) {
                int j = i;
                
                int numClockwiseRotations = 0;
                while (sequence[j].rotationIndex == sequence[i].rotationIndex) {
                    numClockwiseRotations += sequence[j].rotationTypeIndex + 1;
                    j++;
                    
                    if (j >= sequence.Length)
                        break;
                }
                
                numClockwiseRotations %= 4;
                if (numClockwiseRotations == 1)
                    cleanedSequence.Add(new Move(sequence[i].rotationIndex, ClockwiseRotation));
                else if (numClockwiseRotations == 2)
                    cleanedSequence.Add(new Move(sequence[i].rotationIndex, DoubleRotation));
                else if (numClockwiseRotations == 3)
                    cleanedSequence.Add(new Move(sequence[i].rotationIndex, CounterClockwiseRotation));
                    
                i = j;
            }
            return cleanedSequence.ToArray();
        }
        
        public static string CleanSequence(string sequence) {
            Move[] sequenceAsMoves = GetMovesFromSequence(sequence);
            Move[] cleanedMoves = CleanSequence(sequenceAsMoves);
            string cleanedSequence = string.Empty;
            
            foreach (Move move in cleanedMoves)
                cleanedSequence += $"{move} ";
            
            return cleanedSequence.TrimEnd(' ');
        }
        
        public static Move[] GetInverse(string moveSequence) {
            Move[] moves = GetMovesFromSequence(moveSequence);
            Move[] invertedSequence = new Move[moves.Length];
            
            for (int i = 0; i < moves.Length; i++) {
                invertedSequence[i] = moves[^(i + 1)].GetOppositeRotationMove();
            }
            
            return invertedSequence;
        }
        
        public static Move[] GetPureRandomMoves(int sequenceLength) {
            Move[] moves = new Move[sequenceLength];
            for (int i = 0; i < sequenceLength; i++) {
                moves[i] = GetRandom();
            }
            return moves;
        }
        
        public static Move GetRandom() {
            return new Move(rand.Next(0, ZCubeRotationIndex + 1), rand.Next(0, CounterClockwiseRotation + 1));
        }
        
        public static Move[] GetInverseMoves(Move[] moves) {
            Move[] invertedSequence = new Move[moves.Length];
            
            for (int i = 0; i < moves.Length; i++) {
                invertedSequence[i] = moves[^(i + 1)].GetOppositeRotationMove();
            }
            
            return invertedSequence;
        }
        
        private static Flag GetRotationFlag(int rotationIndex) {
            return rotationIndex switch {
                >= 15 => Flag.CubeRotation,
                >= 9 => Flag.WideRotation,
                >= 6 => Flag.SliceRotation,
                _ => Flag.FaceRotation,
            };
        }
        
        public static bool operator ==(Move move1, Move move2) {
            return move1.rotationIndex == move2.rotationIndex && move1.rotationTypeIndex == move2.rotationTypeIndex;
        }
        
        public static bool operator !=(Move move1, Move move2) {
            return !(move1 == move2);
        }

        public override bool Equals(object obj) {
            if (obj is not Move move)
                return false;
            if (move.IsNullMove || IsNullMove)
                return false;
            
            return move == this;
        }

        public override int GetHashCode() {
            return HashCode.Combine(rotationIndex, rotationTypeIndex);
        }
    }
}