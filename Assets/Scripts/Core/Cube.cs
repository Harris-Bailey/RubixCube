/*
     0,  1,  2
     3,  4,  5
     6,  7,  8
    ----------
     9, 10, 11 | 18, 19, 20 | 27, 28, 29 | 36, 37, 38
    12, 13, 14 | 21, 22, 23 | 30, 31, 32 | 39, 40, 41
    15, 16, 17 | 24, 25, 26 | 33, 34, 35 | 42, 43, 44
    ----------
    45, 46, 47
    48, 49, 50
    51, 52, 53
    
    
     Y,  Y,  Y
     Y,  Y,  Y
     Y,  Y,  Y
    -------
     R,  R,  R |  G,  G,  G |  O,  O,  O |  B,  B,  B
     R,  R,  R |  G,  G,  G |  O,  O,  O |  B,  B,  B
     R,  R,  R |  G,  G,  G |  O,  O,  O |  B,  B,  B
    -------
     W,  W,  W
     W,  W,  W
     W,  W,  W
*/

using System.Text;
namespace RubixCube.Core {

    public class Cube {
        public ulong[] BitboardsMatchingCenters { get; private set; } = {
            0, 0, 0, 0, 0, 0
        };
        public ulong BitsMatchingCenter0 => BitboardsMatchingCenters[0];
        public ulong BitsMatchingCenter1 => BitboardsMatchingCenters[1];
        public ulong BitsMatchingCenter2 => BitboardsMatchingCenters[2];
        public ulong BitsMatchingCenter3 => BitboardsMatchingCenters[3];
        public ulong BitsMatchingCenter4 => BitboardsMatchingCenters[4];
        public ulong BitsMatchingCenter5 => BitboardsMatchingCenters[5];
        
        public Cube() {
            BitboardsMatchingCenters = new ulong[] {
                0b111111111ul,
                0b111111111ul << 9,
                0b111111111ul << 18,
                0b111111111ul << 27,
                0b111111111ul << 36,
                0b111111111ul << 45
            };
        }
        
        public Cube(Move[] scramble) {
            BitboardsMatchingCenters = new ulong[] {
                0b111111111ul,
                0b111111111ul << 9,
                0b111111111ul << 18,
                0b111111111ul << 27,
                0b111111111ul << 36,
                0b111111111ul << 45
            };
            
            foreach (Move move in scramble) {
                Rotate(move);
            }
        }
        
        public Cube(string state) {
            SetCube(state);
        }
        
        public void SetCube(string state) {       
            BitboardsMatchingCenters = new ulong[] { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < state.Length; i++) {
                char faceletNum = state[i];
                switch (faceletNum) {
                    case 'Y':
                        BitboardsMatchingCenters[0] |= 1ul << i;
                        break;
                    case 'R':
                        BitboardsMatchingCenters[1] |= 1ul << i;
                        break;
                    case 'G':
                        BitboardsMatchingCenters[2] |= 1ul << i;
                        break;
                    case 'O':
                        BitboardsMatchingCenters[3] |= 1ul << i;
                        break;
                    case 'B':
                        BitboardsMatchingCenters[4] |= 1ul << i;
                        break;
                    case 'W':
                        BitboardsMatchingCenters[5] |= 1ul << i;
                        break;
                }
            }
        }
        
        public void SetState(ulong[] faceletBitboards) {
            for (int i = 0; i < BitboardsMatchingCenters.Length; i++) {
                ulong newBitboard = faceletBitboards[i];
                BitboardsMatchingCenters[i] = newBitboard;
            }
        }
        
        public void Rotate(Move rotationMove) {
            int rotationIndex = rotationMove.rotationIndex;
            int rotationTypeIndex = rotationMove.rotationTypeIndex;
            
            int startIndex = RotationData.GetRotationBitboardStartIndex(rotationIndex, rotationTypeIndex);
            ulong bitsAffected = RotationData.BitsMovedByRotationMask[rotationIndex];
            // BitboardHelper.PrintBitboard(bitsAffected);
            for (int i = 0; i < BitboardsMatchingCenters.Length; i++) {
                ulong colouredBitboard = BitboardsMatchingCenters[i];
                ulong bitsNeedingRotating = colouredBitboard & bitsAffected;
                ulong rotatedBitboard = 0;
                
                // // no bits to rotate so we can continue to the next coloured bitboard
                // if (bitsNeedingRotating == 0)
                //     continue;
                
                // continuing until there are no more bits to rotate
                while (bitsNeedingRotating != 0) {
                    // getting the bit in the lowest position
                    int bitPos = BitboardHelper.PopLeastSignificantBit(ref bitsNeedingRotating);
                    
                    // fetching the precomputed rotation from the array and adding it to the rotated bitboard
                    rotatedBitboard |= RotationData.RotatedBitPositionsBitboard[startIndex + bitPos];
                }
                
                // remove the rotated bits in their original positions and then add them in their new rotated positions
                BitboardsMatchingCenters[i] = (colouredBitboard & ~bitsAffected) | rotatedBitboard;
            }
        }

        public bool MaskMatchesCubeState(CubeMask cubeMask) {
            for (int i = 0; i < BitboardsMatchingCenters.Length; i++) {
                if ((cubeMask.ColouredFaceletBitboards[i] & BitboardsMatchingCenters[i]) != cubeMask.ColouredFaceletBitboards[i])
                    return false;
            }
            return true;
        }
        
        public ulong[] GetState() {
            ulong[] state = new ulong[6];
            for (int i = 0; i < BitboardsMatchingCenters.Length; i++) {
                ulong bitboard = BitboardsMatchingCenters[i];
                state[i] = bitboard;
            }
            return state;
        }
        
        public override string ToString() {
            // Array of facelet characters
            char[] facelets = new char[9 * 6];

            // Color mapping for each face
            char[] colourMapping = { 'Y', 'R', 'G', 'O', 'B', 'W' };

            // Fill the facelets array with colors from the bitboards
            for (int i = 0; i < BitboardsMatchingCenters.Length; i++) {
                ulong faceletBitboard = BitboardsMatchingCenters[i];
                char colour = colourMapping[i];
                
                while (faceletBitboard != 0) {
                    int index = BitboardHelper.PopLeastSignificantBit(ref faceletBitboard);
                    facelets[index] = colour; // Map the index to the appropriate color
                }
            }

            string topFace = new string(facelets[0..9]);
            string frontFace = new string(facelets[9..18]);
            string rightFace = new string(facelets[18..27]);
            string backFace = new string(facelets[27..36]);
            string leftFace = new string(facelets[36..45]);
            string bottomFace = new string(facelets[45..54]);
            
            // Format and print the cube in the desired layout
            StringBuilder sb = new StringBuilder();

            // Print the top face
            sb.AppendLine($"{topFace[0..3]}");
            sb.AppendLine($"{topFace[3..6]}");
            sb.AppendLine($"{topFace[6..9]}");
            sb.AppendLine();

            // Print the middle rows (left, front, right faces)
            sb.AppendLine($"{frontFace[0..3]} {rightFace[0..3]} {backFace[0..3]} {leftFace[0..3]}");
            sb.AppendLine($"{frontFace[3..6]} {rightFace[3..6]} {backFace[3..6]} {leftFace[3..6]}");
            sb.AppendLine($"{frontFace[6..9]} {rightFace[6..9]} {backFace[6..9]} {leftFace[6..9]}");

            // Print the bottom face
            sb.AppendLine();
            sb.AppendLine($"{bottomFace[0..3]}");
            sb.AppendLine($"{bottomFace[3..6]}");
            sb.AppendLine($"{bottomFace[6..9]}");

            // Print the output
            return sb.ToString();
        }

        public override int GetHashCode() {
            int hash = 17;
            foreach (ulong bitboard in BitboardsMatchingCenters) {
                // Combine the high and low 32 bits of the ulong to avoid losing information.
                hash = hash * 31 + (int)(bitboard & 0xFFFFFFFF);  // Low 32 bits
                hash = hash * 31 + (int)(bitboard >> 32);        // High 32 bits
            }
            return hash;
        }
    }
}