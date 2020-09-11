using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LVLTool
{
    public static class BinUtils
    {
        private static bool Find(byte[] hexNumber, long location, byte[] data)
        {
            int index = 0;
            while (index < hexNumber.Length && (int)hexNumber[index] == (int)data[location + (long)index])
                ++index;
            return index == hexNumber.Length;
        }

        private static bool Find(byte[] hexNumber, long location, List<byte> data)
        {
            int index = 0;
            while (index < hexNumber.Length && (int)hexNumber[index] == (int)data[(int)(location + index)])
                ++index;
            return index == hexNumber.Length;
        }

        /// <summary>
        /// Searched through 'data' for the givenBytes.
        /// </summary>
        /// <param name="startLocation">Where to start in 'data'</param>
        /// <param name="givenBytes">the stuff to look for</param>
        /// <param name="data">the big data to look through</param>
        /// <returns>location of the givenBytes, -1L if not found.</returns>
        public static long GetLocationOfGivenBytes(long startLocation, byte[] givenBytes, byte[] data, long maxDistance)
        {
            long retVal = -1L;
            long num = (long)(data.Length - givenBytes.Length);
            for (long location = startLocation; location < num; ++location)
            {
                if (Find(givenBytes, location, data))
                {
                    retVal = location;
                    break;
                }
                else if (maxDistance < location - startLocation)
                    break;
            }
            return retVal;
        }

        public static long GetLocationOfGivenBytes(long startLocation, byte[] givenBytes, List<byte> data, long maxDistance)
        {
            long retVal = -1L;
            long num = (long)(data.Count - givenBytes.Length);
            for (long location = startLocation; location < num; ++location)
            {
                if (Find(givenBytes, location, data))
                {
                    retVal = location;
                    break;
                }
                else if (maxDistance < location - startLocation)
                    break;
            }
            return retVal;
        }

        public static string GetByteString(long location, byte[] data, int numBytes)
        {
            char c = '.';
            if (location + numBytes > data.Length)
                return "";
            StringBuilder b = new StringBuilder(numBytes);
            if (location < 0) location = 0;
            
            for (long l = location; l < location + numBytes; l++)
            {
                if (data[l] != 0)
                {
                    c = (char)data[l];
                    b.Append(c);
                }
            }
            return b.ToString();
        }
        // The number/length is stored in reverse least significant --> most significant
        // This function gets the number at the specified location and gives it to you good ;
        public static UInt16 Get2ByteNumberAtLocation(int loc, List<byte> data)
        {
            byte b0, b1;
            b0 = data[loc];
            b1 = data[loc + 1];

            UInt16 retVal = (UInt16)(b0 + (b1 << 8));
            return retVal;
        }

        // The number/length is stored in reverse least significant --> most significant
        // This function gets the number at the specified location and gives it to you good ;
        public static uint GetNumberAtLocation(int loc, List<byte> data)
        {
            byte b0, b1, b2, b3;
            b0 = data[loc];
            b1 = data[loc + 1];
            b2 = data[loc + 2];
            b3 = data[loc + 3];

            uint retVal = (uint)(b0 + (b1 << 8) + (b2 << 16) + (b3 << 24));
            return retVal;
        }

        // The number/length is stored in reverse least significant --> most significant
        // This function gets the number at the specified location and gives it to you good ;
        public static uint GetNumberAtLocation(long loc, byte[] data)
        {
            byte b0, b1, b2, b3;
            b0 = data[loc];
            b1 = data[loc + 1];
            b2 = data[loc + 2];
            b3 = data[loc + 3];

            uint retVal = (uint)( b0 + (b1 << 8) + (b2 << 16) + (b3 << 24));
            return retVal;
        } 

        // The number/length is stored in reverse least significant --> most significant
        // This function sets the number at the specified location 
        public static void WriteNumberAtLocation(long loc, UInt32 num, byte[] data)
        {
            byte[] encodedNumber = EncodeNumber( (int) num);

            data[loc] = encodedNumber[0];
            data[loc + 1] = encodedNumber[1];
            data[loc + 2] = encodedNumber[2];
            data[loc + 3] = encodedNumber[3];
        }

        // The number/length is stored in reverse least significant --> most significant
        // This function sets the number at the specified location
        public static void WriteNumberAtLocation(int loc, UInt32 num, List<byte> data)
        {
            byte[] encodedNumber = EncodeNumber((int)num);

            data[loc] = encodedNumber[0];
            data[loc + 1] = encodedNumber[1];
            data[loc + 2] = encodedNumber[2];
            data[loc + 3] = encodedNumber[3];
        }

        // The number/length is stored in reverse least significant --> most significant
        // This function sets the number at the specified location
        public static void Write2ByteNumberAtLocation(int loc, UInt16 num, List<byte> data)
        {
            byte[] encodedNumber = EncodeNumber((int)num);

            data[loc] = encodedNumber[0];
            data[loc + 1] = encodedNumber[1];
        }

        public static byte[] EncodeNumber(int num)
        {
            byte[] retVal = new byte[4];
            retVal[0] = (byte)(num & 0x000000FF); // last byte
            retVal[1] = (byte)((num & 0x0000FF00) >> 8);
            retVal[2] = (byte)((num & 0x00FF0000) >> 16);
            retVal[3] = (byte)((num & 0xFF000000) >> 24); // first byte
            return retVal;
        }

        public static bool BinCompare(byte[] littleData, byte[] bigData, long location)
        {
            for (long i = 0; i < littleData.Length; i++)
                if (littleData[i] != bigData[i + location])
                    return false;
            return true;
        }
    }
}
