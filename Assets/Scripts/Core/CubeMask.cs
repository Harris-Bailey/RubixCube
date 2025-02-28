namespace RubixCube.Core {
    public class CubeMask {
        public readonly ulong[] ColouredFaceletBitboards;
        
        public CubeMask(ulong[] cubeBitboards) {
            ColouredFaceletBitboards = cubeBitboards;
        }
        
        public CubeMask(
            ulong[] cubeBitboards, 
            ulong topColourBits, 
            ulong frontColourBits, 
            ulong rightColourBits, 
            ulong backColourBits, 
            ulong leftColourBits, 
            ulong bottomColourBits
        ) { 
            ColouredFaceletBitboards = new ulong[6];
            for (int i = 0; i < cubeBitboards.Length; i++) {
                ulong cubeBitboard = cubeBitboards[i];
                
                if ((cubeBitboard & 0b000010000ul) != 0)
                    ColouredFaceletBitboards[i] = topColourBits;
                else if ((cubeBitboard & 0b000010000000000000ul) != 0)
                    ColouredFaceletBitboards[i] = frontColourBits;
                else if ((cubeBitboard & 0b000010000000000000000000000ul) != 0)
                    ColouredFaceletBitboards[i] = rightColourBits;
                else if ((cubeBitboard & 0b000010000000000000000000000000000000ul) != 0)
                    ColouredFaceletBitboards[i] = backColourBits;
                else if ((cubeBitboard & 0b000010000000000000000000000000000000000000000ul) != 0)
                    ColouredFaceletBitboards[i] = leftColourBits;
                else if ((cubeBitboard & 0b000010000000000000000000000000000000000000000000000000ul) != 0)
                    ColouredFaceletBitboards[i] = bottomColourBits;
            }
        }
        
        // is this useful ???? I got no clue whether to keep it or not, and whether it's more intuitive than bits
        // for people who aren't as familiar with bits
        public CubeMask(ulong[] cubeBitboards, int[] onePositions, int[] twoPositions, int[] threePositions, int[] fourPositions, int[] fivePositions, int[] sixPositions) {
            ColouredFaceletBitboards = new ulong[6];
            for (int i = 0; i < cubeBitboards.Length; i++) {
                ulong cubeBitboard = cubeBitboards[i];
                ulong maskBitboard = 0;
                
                if ((cubeBitboard & 0b000010000ul) != 0) {
                    foreach (int position in onePositions)
                        maskBitboard |= 1ul << position;
                }
                else if ((cubeBitboard & 0b000010000000000000ul) != 0) {
                    foreach (int position in twoPositions)
                        maskBitboard |= 1ul << position;
                }
                else if ((cubeBitboard & 0b000010000000000000000000000ul) != 0) {
                    foreach (int position in threePositions)
                        maskBitboard |= 1ul << position;
                }
                else if ((cubeBitboard & 0b000010000000000000000000000000000000ul) != 0) {
                    foreach (int position in fourPositions)
                        maskBitboard |= 1ul << position;
                }
                else if ((cubeBitboard & 0b000010000000000000000000000000000000000000000ul) != 0) {
                    foreach (int position in fivePositions)
                        maskBitboard |= 1ul << position;
                }
                else if ((cubeBitboard & 0b000010000000000000000000000000000000000000000000000000ul) != 0) {
                    foreach (int position in sixPositions)
                        maskBitboard |= 1ul << position;
                }
                
                ColouredFaceletBitboards[i] = maskBitboard;
            }
        }
        
        public CubeMask(ulong[] cubeBitboards, ulong[] mask) {
            ColouredFaceletBitboards = new ulong[6];
            for (int i = 0; i < cubeBitboards.Length; i++) {
                ulong cubeBitboard = cubeBitboards[i];
                
                if ((cubeBitboard & 0b000010000ul) != 0)
                    ColouredFaceletBitboards[i] = mask[0];
                else if ((cubeBitboard & 0b000010000000000000ul) != 0)
                    ColouredFaceletBitboards[i] = mask[1];
                else if ((cubeBitboard & 0b000010000000000000000000000ul) != 0)
                    ColouredFaceletBitboards[i] = mask[2];
                else if ((cubeBitboard & 0b000010000000000000000000000000000000ul) != 0)
                    ColouredFaceletBitboards[i] = mask[3];
                else if ((cubeBitboard & 0b000010000000000000000000000000000000000000000ul) != 0)
                    ColouredFaceletBitboards[i] = mask[4];
                else if ((cubeBitboard & 0b000010000000000000000000000000000000000000000000000000ul) != 0)
                    ColouredFaceletBitboards[i] = mask[5];
            }
        }
        
        public CubeMask Combine(CubeMask mask) {
            ulong[] combinedFaces = new ulong[6];
            for (int i = 0; i < ColouredFaceletBitboards.Length; i++) {
                combinedFaces[i] = ColouredFaceletBitboards[i] | mask.ColouredFaceletBitboards[i];
            }
            return new CubeMask(combinedFaces);
        }
        
        public bool IsSame(CubeMask mask) {
            for (int i = 0; i < ColouredFaceletBitboards.Length; i++) {
                if (mask.ColouredFaceletBitboards[i] != ColouredFaceletBitboards[i])
                    return false;
            }
            return true;
        }
        
        public CubeMask Copy() {
            ulong[] bitboards = new ulong[6];
            for (int i = 0; i < ColouredFaceletBitboards.Length; i++) {
                ulong bitboard = ColouredFaceletBitboards[i];
                bitboards[i] = bitboard;
            }

            return new CubeMask(bitboards);
        }
    }
}