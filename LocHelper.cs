using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Loc files are not terribly complex.
 * They are ucfb fiels with file type, name and body.
 * Each of the strings are layed out as:
 *      string_id_hash (4 bytes)
 *      2 byte number ( total size of the info needed fof the string [string, pointer, size])
 *      string_content (2 byte characters)
 *      termination sequence of 32-bit '0' (ie 00000000)
 */
namespace LVLTool
{
    /// <summary>
    /// class for localization operations.
    /// </summary>
    public class LocHelper
    {
        private  byte[] BodyBytes = { 0x42, 0x4f, 0x44, 0x59 }; 
        private uint BodyStart
        {
            get
            {
                uint retVal = (uint)BinUtils.GetLocationOfGivenBytes(24, BodyBytes, mData, 20);
                retVal += 8;
                return retVal;
            }
        }

        public byte[] GetData()
        {
            // this is where we trim the fat of all the added bytes from 'SetString()'
            int logicalEnd = (int)(BodySizeLoc + BodyEnd);
            for (int i = mData.Count-1; i > logicalEnd; i--)
                mData.RemoveAt(i);
            MakeUcfb();
            byte[] retVal = mData.ToArray();
            return retVal;
        }

        /// <summary>
        /// The entire data of the loc resource file 
        /// </summary>
        private List<byte> mData;

        private int mBodySizeLoc = -1;
        private int BodySizeLoc
        {
            get
            {
                if (mBodySizeLoc == -1)
                {
                    byte[] bodyStr = new byte[] { 0x42, 0x4f, 0x44, 0x59 };
                    long loc = BinUtils.GetLocationOfGivenBytes(0x10, bodyStr, mData, 0x30);
                    mBodySizeLoc = (int)(loc + 4);
                }
                return mBodySizeLoc;
            }
        }

        private uint BodyEnd
        {
            get
            {
                uint bodyLen = BinUtils.GetNumberAtLocation(BodySizeLoc, mData);
                uint retVal = (uint)(BodySizeLoc + bodyLen);
                return retVal;
            }
            /*set
            {
                /*int bodyLenLoc = BodySizeLoc;
                byte[] bodyLen = BinUtils.EncodeNumber((int)value);
                mData[bodyLenLoc  ] = bodyLen[0];
                mData[bodyLenLoc+1] = bodyLen[1];
                mData[bodyLenLoc+2] = bodyLen[2];
                mData[bodyLenLoc+3] = bodyLen[3];* /

                BinUtils.WriteNumberAtLocation(BodySizeLoc, value, mData);
            }*/
        }

        private void AddBodySize(int size)
        {
            uint bodyEnd = (uint)(BodyEnd + size);

            BinUtils.WriteNumberAtLocation(BodySizeLoc, bodyEnd, mData);
        }

        public LocHelper(byte[] data)
        {
            mData = new List<byte>( data.Length + 1024);
            mData.AddRange(data);
        }

        private void MakeUcfb()
        {
            int sz = mData.Count - 8;
            BinUtils.WriteNumberAtLocation(4, (uint)sz, mData);
            int rem = mData.Count % 4;
            switch (rem)
            {
                case 0: // add 3
                    mData.Add(0);
                    mData.Add(0);
                    mData.Add(0);
                    break;
                case 1: // add 2
                    mData.Add(0);
                    mData.Add(0);
                    break;
                case 2: // add 1
                    mData.Add(0);
                    break;
            }
        }

        /// <summary>
        /// Add string to the end of the loc file
        /// </summary>
        /// <param name="stringId"></param>
        /// <param name="content"></param>
        public void AddString(string stringId, string content)
        {
            uint hash = HashHelper.HashString(stringId);
            int size = 4 + 6 + (2 * content.Length);
            int neededSize = (int)(BodyEnd + size);
            
            while (neededSize > mData.Count)
                mData.Add(0);

            int stringDataStart =(int) BodyEnd + 1;
            // lay down string; hash, size, string data
            BinUtils.WriteNumberAtLocation((int)(stringDataStart), hash, mData);
            BinUtils.Write2ByteNumberAtLocation(stringDataStart + 5, (UInt16) size, mData);
            //byte[] sz = BinUtils.EncodeNumber(size);
            //mData[stringDataStart + 5] = sz[0];
            //mData[stringDataStart + 6] = sz[1];
            int data_index = stringDataStart + 7;
            for (int i = 0; i < content.Length; i++)
            {
                BinUtils.Write2ByteNumberAtLocation(data_index, content[i], mData);
                //mData[data_index    ] = (byte) content[i];
                //mData[data_index + 1] = (byte)(content[i] << 8);
                data_index += 2;
            }
            BinUtils.WriteNumberAtLocation(data_index, 0, mData);
            // update body data
            AddBodySize(size);
        }

        /// <summary>
        /// String size seem to be always be a multiple of 4. With 4-6 'zero bytes' at the end
        /// </summary>
        /// <param name="stringId">the id of the string to modify</param>
        /// <param name="newValue">the new value</param>
        /// <returns>True if the string was modified</returns>
        public bool SetString(string stringId, string newValue)
        {
            bool retVal = false;
            uint hashId = GetHashId(stringId);
            StringBuilder sb = new StringBuilder(20);
            int stringLoc = GetString(hashId, sb);
            string prevValue = sb.ToString();
            
            if (stringLoc > 0 && prevValue != newValue)
            {
                UInt32 oldSz = BinUtils.Get2ByteNumberAtLocation(stringLoc + 4, mData);
                UInt16 sz = (UInt16)((newValue.Length * 2) + 2 + 4 + 4); // 2:size info 4:hashId 4 min zero bytes
                if (sz % 4 != 0)
                    sz += (UInt16)2;
                
                int diff = 2 * (newValue.Length - prevValue.Length);
                if (diff % 4 != 0)
                    diff = diff + 2;//???
                int dataEnd = mData.Count + diff;
                if (diff > 0)
                {
                    AddBytes(diff);
                    ShiftDataDown(stringLoc, diff, dataEnd);
                }
                else if (diff < 0)
                {
                    ShiftDataUp(stringLoc, -1* diff, dataEnd);
                }
                //write hashId
                BinUtils.WriteNumberAtLocation(stringLoc, hashId, mData);
                
                // write the size
                BinUtils.Write2ByteNumberAtLocation(stringLoc + 4,sz, mData);
                // lay down string 
                stringLoc += 6; /** MODIFYING 'stringLoc' NOW!! **/
                for (int i = 0; i < newValue.Length; i++)
                {
                    BinUtils.Write2ByteNumberAtLocation(stringLoc, newValue[i], mData);
                    stringLoc += 2;
                }
                BinUtils.WriteNumberAtLocation(stringLoc, 0, mData);
                //if( numZeros == 6 )
                //    BinUtils.Write2ByteNumberAtLocation(stringLoc+4, 0, mData);
                AddBodySize(diff);
                retVal = true;
            }
            return retVal;
        }

        private void AddBytes(int numBytes)
        {
            for (int i = 0; i < numBytes; i++)
                mData.Add(0);
        }

        public void SetByte(int loc, byte b)
        {
            mData[loc] = b;
        }
        private void ShiftDataDown(int startIndex, int amount, int dataEnd)
        {
            for (int i = dataEnd - amount; i > startIndex; i--)
            {
                SetByte(i, mData[i - amount]); // for debugging
                //mData[i] = mData[i - amount]; // for speed
            }
        }
        private void ShiftDataUp(int startIndex, int amount, int dataEnd)
        {
            for (int i = startIndex; i < dataEnd; i++)
            {
                SetByte(i, mData[i + amount]); // for debugging
                //mData[i] = mData[i + amount]; // for speed
            }
        }

        public string GetAllStrings()
        {
            StringBuilder sb = new StringBuilder(50*3200 );
            int stringLoc = (int)BodyStart;
            UInt32 currentHash = 0;
            UInt16 sz = 0;
            int byteStart =0;
            int byteEnd = 0;
            char current = '\0';
            string stringId = "";

            while ( ( currentHash = BinUtils.GetNumberAtLocation(stringLoc, mData)) > 0 )
            {
                stringId = HashHelper.GetStringFromHash(currentHash);
                if (stringId == null)
                    stringId = String.Format("0x{0:x6}", currentHash);
                sb.Append(stringId);
                sb.Append("=\"");

                sz = BinUtils.Get2ByteNumberAtLocation(stringLoc + 4, mData);
                //Console.WriteLine("GetAllStrings: Size={0}", sz);
                byteStart = stringLoc + 6;
                byteEnd = stringLoc + sz -1;
                current = '\0';
                for (int i = byteStart; i < byteEnd; i += 2)
                {
                    current = (char)BinUtils.Get2ByteNumberAtLocation(i, mData);
                    if (current == '"')
                        sb.Append("\\\""); // escape the quote
                    else if (current == '\\')
                        sb.Append("\\\\"); // escapr the escape!
                    else if (current != '\0')
                        sb.Append(current);
                }
                sb.Append("\"\n");
                stringLoc = NextStringLoc(stringLoc);
                if (stringLoc + 10 > mData.Count)  // for a string you need minimum 4 bytes for the hash, 2 for size and 4 for nulls
                    break;
            }
            return sb.ToString();
        }
        /// <summary>
        /// returns empty string when cant find string.
        /// </summary>
        /// <param name="stringId"></param>
        /// <returns></returns>
        public string GetString(string stringId)
        {
            uint hash = GetHashId(stringId);
            StringBuilder builder = new StringBuilder(50);
            GetString(hash, builder);
            return builder.ToString();
        }

        private static uint GetHashId(string stringId)
        {
            uint hash = 0;
            if (stringId.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    hash = UInt32.Parse(stringId.Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier);
                }
                catch (Exception) { }
            }
            else
                hash = HashHelper.HashString(stringId);
            return hash;
        }

        /// <summary>
        /// Places the string with the given hashid into the stringbuilder passed in
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="sb">string will be added to the builder</param>
        /// <returns>The location of the string id, -1 if not found </returns>
        public int GetString(UInt32 hashId, StringBuilder sb)
        {
            int stringLoc = (int)BodyStart;
            UInt32 currentHash = 0;
            while ((currentHash = BinUtils.GetNumberAtLocation(stringLoc, mData)) != hashId)
            {
                stringLoc = NextStringLoc(stringLoc);
                if (stringLoc + 10 > mData.Count)
                    return -1;
            }
            UInt16 sz = BinUtils.Get2ByteNumberAtLocation(stringLoc + 4, mData);
            int byteStart = stringLoc + 6;
            int byteEnd = stringLoc + sz -1;
            char current = '\0';
            for (int i = byteStart; i < byteEnd; i += 2)
            {
                current = (char)BinUtils.Get2ByteNumberAtLocation(i, mData);
                if (current != '\0')
                    sb.Append(current);
            }
            return stringLoc;
        }

        private int NextStringLoc(int stringLoc)
        {
            UInt16 sz = BinUtils.Get2ByteNumberAtLocation(stringLoc + 4, mData);
            int retVal = sz + stringLoc;
            return retVal;
        }
/*
 *      string_id_hash (4 bytes)
 *      2 byte number ( total size of the info needed fof the string [string, pointer, size])
 *      string_content (2 byte characters)
 *      termination sequence of 32-bit '0' (ie 00000000)
 */

    }
}

/*
 * what should we do?
 * 1. re-create .cfg files? (may not have all string ids for this to be possible)
 * 2. Add strings if they don't exist      - yes 
 * 3. modify strings if they are different - yes 
 */