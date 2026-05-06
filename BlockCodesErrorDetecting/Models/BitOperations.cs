using System;

namespace BlockCodesErrorDetecting.Models;

public class BitOperations
{
    public static int GetBit(uint value, int position) => (int)(value >> position & 1);
    public static uint SetBit(uint value, int position) => value | (1u << position);
    public static uint ClearBit(uint value, int position) => value & ~(1u << position);
    public static uint ToggleBit(uint value, int position) => value ^ (1u << position);
    public static uint SlideLeft(uint value, int position) => value << position;
    public static uint SlideRight(uint value, int position) => value >> position;

    public static int PopCount(uint value)
    {
        int count = 0;
        for (int i = 0; i < 32; i++)
            count += GetBit(value, i);
        return count;
    }

    public static string BitToString(uint value, int width = 32)
    {
        if (width > 32) width = 32;
        return Convert.ToString(value, 2).PadLeft(width, '0');
    }
}