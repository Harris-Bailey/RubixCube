using Unity.Burst.Intrinsics;
using System;

namespace RubixCube.Core {
    public static class BitboardHelper {
        /// <summary>
        /// Clears the set bit that's the furthest right
        /// </summary>
        /// <returns>The position of the furthest right set bit</returns>
        public static int PopLeastSignificantBit(ref ulong value) {
            // Unity's alternative to System.Numerics.BitOperations.TrailingZeroCount()
            int bitPosition = (int)X86.Bmi1.tzcnt_u64(value);
            // clearing the least significant set bit
            value &= value - 1;
            // returning the 0th index position of the least significant set bit
            return bitPosition;
        }
        
        public static int GetLeastSignificantBit(ulong value) {
            // Unity's alternative to System.Numerics.BitOperations.TrailingZeroCount()
            int bitPosition = (int)X86.Bmi1.tzcnt_u64(value);
            // returning the 0th index position of the least significant set bit
            return bitPosition;
        }
        
        public static int GetBitCount(ulong value) {
            // Unity's alternative to System.Numerics.BitOperations.PopCount()
            int count = X86.Popcnt.popcnt_u64(value);
            return count;
        }
        
        public static string GetBitboardAsString(ulong bitboard) {
            return Convert.ToString((long)bitboard, 2).PadLeft(54, '0');
        }
    }
}