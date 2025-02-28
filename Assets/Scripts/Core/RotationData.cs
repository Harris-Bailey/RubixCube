using System;

namespace RubixCube.Core {
    public static class RotationData {
        private static readonly int[] Map = {
            #region Indexes for top face rotation
            
            // top facelet rotations
            2,  5,  8,
            1, -1,  7,
            0,  3,  6,
            
            // front adjacent facelet rotations
            36, 37, 38,
            -1, -1, -1,
            -1, -1, -1,
            
            // right adjacent facelet rotations
            9, 10, 11,
            -1, -1, -1,
            -1, -1, -1,
            
            // back adjacent facelet rotations
            18, 19, 20,
            -1, -1, -1,
            -1, -1, -1,
            
            // left adjacent facelet rotations
            27, 28, 29,
            -1, -1, -1,
            -1, -1, -1,
            
            // bottom adjacent facelet rotations
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            #endregion
            #region Indexes for front face rotation
            
            // top adjacent facelets rotations
            -1, -1, -1,
            -1, -1, -1,
            18, 21, 24,
            
            // front facelet rotations
            11, 14, 17,
            10, -1, 16,
            9, 12, 15,
            
            // right adjacent facelet rotations
            47, -1, -1,
            46, -1, -1,
            45, -1, -1,
            
            // back adjacent facelet rotations
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // left adjacent facelet rotations
            -1, -1,  8,
            -1, -1,  7,
            -1, -1,  6,
            
            // bottom adjacent facelet rotations
            38, 41, 44,
            -1, -1, -1,
            -1, -1, -1,
            
            #endregion
            #region Indexes for right face rotation
            
            // top adjacent facelets rotations
            -1, -1, 33,
            -1, -1, 30,
            -1, -1, 27,
            
            // front adjacent facelet rotations
            -1, -1,  2,
            -1, -1,  5,
            -1, -1,  8,
            
            // right facelet rotations
            20, 23, 26,
            19, -1, 25,
            18, 21, 24,
            
            // back adjacent facelet rotations
            53, -1, -1,
            50, -1, -1,
            47, -1, -1,
            
            // left adjacent facelet rotations
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // bottom adjacent facelet rotations
            -1, -1, 11,
            -1, -1, 14,
            -1, -1, 17,
            
            #endregion
            #region Indexes for back face rotation
            
            // top adjacent facelets rotations
            42, 39, 36,
            -1, -1, -1,
            -1, -1, -1,
            
            // front adjacent facelet rotations
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // right adjacent facelet rotations
            -1, -1,  0,
            -1, -1,  1,
            -1, -1,  2,
            
            // back facelet rotations
            29, 32, 35,
            28, -1, 34,
            27, 30, 33,
            
            // left adjacent facelet rotations
            51, -1, -1,
            52, -1, -1,
            53, -1, -1,
            
            // bottom adjacent facelet rotations
            -1, -1, -1,
            -1, -1, -1,
            26, 23, 20,
            
            #endregion
            #region Indexes for left face rotation
            
            // top adjacent facelets rotations
            9, -1, -1,
            12, -1, -1,
            15, -1, -1,
            
            // front adjacent facelet rotations
            45, -1, -1,
            48, -1, -1,
            51, -1, -1,
            
            // right adjacent facelet rotations
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // back adjacent facelet rotations
            -1, -1,  6,
            -1, -1,  3,
            -1, -1,  0,
            
            // left facelet rotations
            38, 41, 44,
            37, -1, 43,
            36, 39, 42,
            
            // bottom adjacent facelet rotations
            35, -1, -1,
            32, -1, -1,
            29, -1, -1,
            
            #endregion
            #region Indexes for bottom face rotation
            
            // top adjacent facelets rotations
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // front adjacent facelet rotations
            -1, -1, -1,
            -1, -1, -1,
            24, 25, 26,
            
            // right adjacent facelet rotations
            -1, -1, -1,
            -1, -1, -1,
            33, 34, 35,
            
            // back adjacent facelet rotations
            -1, -1, -1,
            -1, -1, -1,
            42, 43, 44,
            
            // left adjacent facelet rotations
            -1, -1, -1,
            -1, -1, -1,
            15, 16, 17,
            
            // bottom facelet rotations
            47, 50, 53,
            46, -1, 52,
            45, 48, 51,
            
            #endregion
            
            #region Indexes for M slice rotation
            
            // top facelet rotated positions
            -1, 10, -1,
            -1, 13, -1,
            -1, 16, -1,
            
            // front facelet rotated positions
            -1, 46, -1,
            -1, 49, -1,
            -1, 52, -1,
            
            // right facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // back facelet rotated positions
            -1,  7, -1,
            -1,  4, -1,
            -1,  1, -1,
            
            // left facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // bottom facelet rotated positions
            -1, 34, -1,
            -1, 31, -1,
            -1, 28, -1,
            
            #endregion
            #region Indexes for E slice rotation
            
            // top facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // front facelet rotated positions
            -1, -1, -1,
            21, 22, 23,
            -1, -1, -1,
            
            // right facelet rotated positions
            -1, -1, -1,
            30, 31, 32,
            -1, -1, -1,
            
            // back facelet rotated positions
            -1, -1, -1,
            39, 40, 41,
            -1, -1, -1,
            
            // left facelet rotated positions
            -1, -1, -1,
            12, 13, 14,
            -1, -1, -1,
            
            // bottom facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            #endregion
            #region Indexes for S slice rotation
            
            // top facelet rotated positions
            -1, -1, -1,
            19, 22, 25,
            -1, -1, -1,
            
            // front facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // right facelet rotated positions
            -1, 50, -1,
            -1, 49, -1,
            -1, 48, -1,
            
            // back facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // left facelet rotated positions
            -1,  5, -1,
            -1,  4, -1,
            -1,  3, -1,
            
            // bottom facelet rotated positions
            -1, -1, -1,
            37, 40, 43,
            -1, -1, -1,
            
            #endregion
            
            #region Indexes for top wide rotation
            
            // top facelet rotated positions
            2,  5,  8,
            1, -1,  7,
            0,  3,  6,
            
            // front facelet rotated rotations
            36, 37, 38,
            39, 40, 41,
            -1, -1, -1,
            
            // right facelet rotated rotations
            9, 10, 11,
            12, 13, 14,
            -1, -1, -1,
            
            // back facelet rotated rotations
            18, 19, 20,
            21, 22, 23,
            -1, -1, -1,
            
            // left facelet rotated rotations
            27, 28, 29,
            30, 31, 32,
            -1, -1, -1,
            
            // bottom facelet rotated rotations
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            #endregion
            #region Indexes for front wide rotation
            
            // top facelet rotated positions
            -1, -1, -1,
            19, 22, 25,
            18, 21, 24,
            
            // front facelet rotated positions
            11, 14, 17,
            10, -1, 16,
            9, 12, 15,
            
            // right facelet rotated positions
            47, 50, -1,
            46, 49, -1,
            45, 48, -1,
            
            // back facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // left facelet rotated positions
            -1,  5,  8,
            -1,  4,  7,
            -1,  3,  6,
            
            // bottom facelet rotated positions
            38, 41, 44,
            37, 40, 43,
            -1, -1, -1,
            
            #endregion
            #region Indexes for right wide rotation
            
            // top facelet rotated positions
            -1, 34, 33,
            -1, 31, 30,
            -1, 28, 27,
            
            // front facelet rotated positions
            -1,  1,  2,
            -1,  4,  5,
            -1,  7,  8,
            
            // right facelet rotated positions
            20, 23, 26,
            19, -1, 25,
            18, 21, 24,
            
            // back facelet rotated positions
            53, 52, -1,
            50, 49, -1,
            47, 46, -1,
            
            // left facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // bottom facelet rotated positions
            -1, 10, 11,
            -1, 13, 14,
            -1, 16, 17,
            
            #endregion
            #region Indexes for back wide rotation
            
            // top facelets rotated positions
            42, 39, 36,
            43, 40, 37,
            -1, -1, -1,
            
            // front facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // right facelet rotated positions
            -1,  3,  0,
            -1,  4,  1,
            -1,  5,  2,
            
            // back facelet rotated positions
            29, 32, 35,
            28, -1, 34,
            27, 30, 33,
            
            // left facelet rotated positions
            51, 48, -1,
            52, 49, -1,
            53, 50, -1,
            
            // bottom facelet rotated positions
            0,  0,  0,
            25, 22, 19,
            26, 23, 20,
            
            #endregion
            #region Indexes for left wide rotation
            
            // top facelet rotated positions
            9, 10, -1,
            12, 13, -1,
            15, 16, -1,
            
            // front facelet rotated positions
            45, 46, -1,
            48, 49, -1,
            51, 52, -1,
            
            // right facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // back facelet rotated positions
            -1,  7,  6,
            -1,  4,  3,
            -1,  1,  0,
            
            // left facelet rotated positions
            38, 41, 44,
            37, -1, 43,
            36, 39, 42,
            
            // bottom facelet rotated positions
            35, 34, -1,
            32, 31, -1,
            29, 28, -1,
            
            #endregion
            #region Indexes for bottom wide rotation
            
            // top facelet rotated positions
            -1, -1, -1,
            -1, -1, -1,
            -1, -1, -1,
            
            // front facelet rotated positions
            -1, -1, -1,
            21, 22, 23,
            24, 25, 26,
            
            // right facelet rotated positions
            -1, -1, -1,
            30, 31, 32,
            33, 34, 35,
            
            // back facelet rotated positions
            -1, -1, -1,
            39, 40, 41,
            42, 43, 44,
            
            // left facelet rotated positions
            -1, -1, -1,
            12, 13, 14,
            15, 16, 17,
            
            // bottom facelet rotated positions
            47, 50, 53,
            46, -1, 52,
            45, 48, 51,
            
            #endregion
            
            #region Indexes for X cube rotation
            
            // top facelet rotated positions
            35, 34, 33,
            32, 31, 30,
            29, 28, 27,
            
            // front facelet rotated positions
            0,  1,  2,
            3,  4,  5,
            6,  7,  8,
            
            // right facelet rotated positions
            20, 23, 26,
            19, -1, 25,
            18, 21, 24,
            
            // back facelet rotated positions
            53, 52, 51,
            50, 49, 48,
            47, 46, 45,
            
            // left facelet rotated positions
            42, 39, 36,
            43, -1, 37,
            44, 41, 38,
            
            // bottom facelet rotated positions
            9, 10, 11,
            12, 13, 14,
            15, 16, 17,
            
            #endregion
            #region Indexes for Y cube rotation
            
            // top facelet rotated positions
            2,  5,  8,
            1, -1,  7,
            0,  3,  6,
            
            // front facelet rotated positions
            36, 37, 38,
            39, 40, 41,
            42, 43, 44,
            
            // right facelet rotated positions
            9, 10, 11,
            12, 13, 14,
            15, 16, 17,
            
            // back facelet rotated positions
            18, 19, 20,
            21, 22, 23,
            24, 25, 26,
            
            // left facelet rotated positions
            27, 28, 29,
            30, 31, 32,
            33, 34, 35,
            
            // bottom facelet rotated positions
            51, 48, 45,
            52, -1, 46,
            53, 50, 47,
            
            #endregion
            #region Indexes for Z cube rotation
            
            // top facelet rotated positions
            20, 23, 26,
            19, 22, 25,
            18, 21, 24,
            
            // front facelet rotated positions
            11, 14, 17,
            10, -1, 16,
            9, 12, 15,
            
            // right facelet rotated positions
            47, 50, 53,
            46, 49, 52,
            45, 48, 51,
            
            // back facelet rotated positions
            33, 30, 27,
            34, -1, 28,
            35, 32, 29,
            
            // left facelet rotated positions
            2,  5,  8,
            1,  4,  7,
            0,  3,  6,
            
            // bottom facelet rotated positions
            38, 41, 44,
            37, 40, 43,
            36, 39, 42,
            
            #endregion
        };
        
        // there's 54 facelets in each rotation
        private static readonly int numWaysToRotate = Map.Length / 54;
        private static readonly int numFaces = 6;
        
        /// <summary>
        /// The bitboards that show where a specific bit will end up given a certain rotation
        /// </summary>
        public static readonly ulong[] RotatedBitPositionsBitboard;
        
        /// <summary>
        /// Contains a bitboard for the bits that have moved position due to the rotation. <br/>
        /// This does not include bits that will rotate in a 3D space but haven't actually moved position.
        /// </summary>
        public static readonly ulong[] BitsMovedByRotationMask;
        
        public const ulong TopCenterFaceletMask    = 0b000010000ul;
        public const ulong FrontCenterFaceletMask  = 0b000010000ul << 9;
        public const ulong RightCenterFaceletMask  = 0b000010000ul << 18;
        public const ulong BackCenterFaceletMask   = 0b000010000ul << 27;
        public const ulong LeftCenterFaceletMask   = 0b000010000ul << 36;
        public const ulong BottomCenterFaceletMask = 0b000010000ul << 45;
        
        static RotationData() {
            int faceletCount = 54;
            int numRotationTypes = 3;
            
            BitsMovedByRotationMask = new ulong[numWaysToRotate];
            for (int rotationIndex = 0; rotationIndex < numWaysToRotate; rotationIndex++) {
                ulong bitsAffectedByRotationMask = 0;
                for (int bitIndex = 0; bitIndex < faceletCount; bitIndex++) {
                    // calculates the index of the value from the map
                    int mapIndex = rotationIndex * faceletCount + bitIndex;
                    // uses the map to get the index on the cube that the bit rotates to
                    int bitRotatedIndex = Map[mapIndex];
                    // skip if the bit's position doesn't change, denoted by a -1
                    if (bitRotatedIndex == -1)
                        continue;
                    // otherwise it does move so assign the rotated index to the bits affected
                    bitsAffectedByRotationMask |= 1ul << bitRotatedIndex;
                }
                BitsMovedByRotationMask[rotationIndex] = bitsAffectedByRotationMask;
            }
            
            // there'll be one bitboard per bit for however many facelets, number of ways to rotate, and the number of rotation types
            RotatedBitPositionsBitboard = new ulong[numWaysToRotate * faceletCount * numRotationTypes];
            // go over every way to rotate
            for (int rotationIndex = 0; rotationIndex < numWaysToRotate; rotationIndex++) {
                // go over every bit
                for (int bitIndex = 0; bitIndex < faceletCount; bitIndex++) {
                    // map the bit to the rotated bit index
                    int mappedRotationIndex = Map[rotationIndex * faceletCount + bitIndex];
                    // go over every way to rotate
                    for (int rotationTypeIndex = 0; rotationTypeIndex < numRotationTypes; rotationTypeIndex++) {
                        // skip if there's no change to the bit's position once rotated - denoted by a -1
                        if (mappedRotationIndex == -1)
                            break;
                        // assign the rotated index to a specific index in the array as a bitboard
                        RotatedBitPositionsBitboard[GetRotationBitboardStartIndex(rotationIndex, rotationTypeIndex) + bitIndex] = 1ul << mappedRotationIndex;
                        // reassign the mapped rotation index based on this new position to get the next rotation type
                        // single move becomes double move, double move becomes counter clockwise move
                        mappedRotationIndex = Map[rotationIndex * faceletCount + mappedRotationIndex];
                    }     
                }   
            }
        }
        
        public static int GetRotationBitboardStartIndex(int rotationIndex, int rotationTypeIndex) {
            int faceletCount = 54;
            
            // these offsets allow us to omit 3 dimensional arrays and instead use 1 dimensional arrays
            int rotationTypeOffset = faceletCount * numWaysToRotate * rotationTypeIndex;
            int rotationOffset = faceletCount * rotationIndex;
            
            return rotationTypeOffset + rotationOffset;
        }
    }
}