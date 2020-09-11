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
        private Dictionary<uint, string> mStringSet  = null;
        private Dictionary<uint, string> mStringSet2 = null;

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

        public byte[] GetUcfbData()
        {
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
                //if (mBodySizeLoc == -1)
                {
                    byte[] bodyStr = new byte[] { 0x42, 0x4f, 0x44, 0x59 };//"BODY"
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
        }

        private void AddBodySize(int size)
        {
            uint bodySize = BinUtils.GetNumberAtLocation(BodySizeLoc, mData);
            bodySize = (uint)(bodySize + size);
            BinUtils.WriteNumberAtLocation(BodySizeLoc, bodySize, mData);
        }

        public LocHelper(byte[] data)
        {
            mData = new List<byte>( data.Length + 1024);
            mData.AddRange(data);
        }

        private void MakeUcfb()
        {
            // trim
            int end = (int) BodyEnd;
            for (int i = mData.Count - 1; i > end; i--)
                mData.RemoveAt(i);

            // add UCFB header
            if (mData[0] != (byte)'u')
            {
                mData.Insert(0, 0);
                mData.Insert(0, 0);
                mData.Insert(0, 0);
                mData.Insert(0, 0);
                mData.Insert(0, 0);
                mData.Insert(0, 0);
                mData.Insert(0, 0);
                mData.Insert(0, 0);

                mData[0] = (byte)'u';
                mData[1] = (byte)'c';
                mData[2] = (byte)'f';
                mData[3] = (byte)'b';
            }
            // add bytes at the end (if necessary)
            int rem = (mData.Count-1) % 4;
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
            int sz = mData.Count - 8;
            BinUtils.WriteNumberAtLocation(4, (uint)sz, mData);// ucfb size
            BinUtils.WriteNumberAtLocation(0xc, (uint)(sz-8), mData); // Locl size
        }

        /// <summary>
        /// Add string to the end of the loc file
        /// </summary>
        /// <param name="stringId"></param>
        /// <param name="content"></param>
        public void AddString(string stringId, string content)
        {
            uint hashId = HashHelper.HashString(stringId);
            
            UInt16 sz = (UInt16)((content.Length * 2) + 2 + 4 + 4); // 2:size info 4:hashId 4 min zero bytes
            bool sixZeros = false;
            if (sz % 4 != 0)
            {
                sz += (UInt16)2;
                sixZeros = true;
            }
            if (BodyEnd + sz > mData.Count)
            {
                AddBytes( (int)(BodyEnd + sz ) - mData.Count); 
            }
            int stringLoc = (int)(BodyEnd - 4);
            //write hashId
            BinUtils.WriteNumberAtLocation(stringLoc, hashId, mData);

            // write the size
            BinUtils.Write2ByteNumberAtLocation(stringLoc + 4, sz, mData);
            // lay down string 
            stringLoc += 6; /** MODIFYING 'stringLoc' NOW!! **/
            for (int i = 0; i < content.Length; i++)
            {
                BinUtils.Write2ByteNumberAtLocation(stringLoc, content[i], mData);
                stringLoc += 2;
            }
            BinUtils.WriteNumberAtLocation(stringLoc, 0, mData);
            if (sixZeros)
                BinUtils.Write2ByteNumberAtLocation(stringLoc + 4, 0, mData);
            AddBodySize(sz);
        }

        /// <summary>
        /// String size seem to be always be a multiple of 4. With 4-6 'zero bytes' at the end
        /// </summary>
        /// <param name="stringId">the id of the string to modify</param>
        /// <param name="newValue">the new value</param>
        /// <returns>The string location, -1 if not found</returns>
        public int SetString(string stringId, string newValue)
        {
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
                bool sixZeros = false;
                int diff = (int)(sz - oldSz);  //2 * (newValue.Length - prevValue.Length);
                if (diff % 4 != 0)
                {
                    diff = diff + 2;
                    sixZeros = true;
                }
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
                if( sixZeros )
                    BinUtils.Write2ByteNumberAtLocation(stringLoc+4, 0, mData);
                AddBodySize(diff);
            }
            return stringLoc;
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
            mStringSet = new Dictionary<uint, string>(2000);
            mStringSet2 = new Dictionary<uint, string>(200);
            StringBuilder cur = new StringBuilder(80);

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
                    {
                        sb.Append("\\\""); // escape the quote
                        cur.Append("\\\"");
                    }
                    else if (current == '\\')
                    {
                        sb.Append("\\\\"); // escape the escape!
                        cur.Append("\\\\");
                    }
                    else if (current != '\0')
                    {
                        sb.Append(current);
                        cur.Append(current);
                    }
                }
                if (mStringSet.ContainsKey(currentHash))
                {
                    mStringSet2.Add(currentHash, cur.ToString());
                    //Console.WriteLine("Error! key 0x{0:x} ({1}) already exists as: {2}", currentHash, stringId, mStringSet[currentHash]);
                    //Console.WriteLine("Cannot add {0}:{1}",stringId, cur.ToString() );
                }
                else
                    mStringSet.Add(currentHash, cur.ToString());
                cur.Length = 0; // clear
                sb.Append("\"\n");
                stringLoc = NextStringLoc(stringLoc);
                if (stringLoc + 10 > mData.Count)  // for a string you need minimum 4 bytes for the hash, 2 for size and 4 for nulls
                    break;
            }
            Console.WriteLine("mStringSet2.Count:{0}",mStringSet2.Count);
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
        /// Places the string with the given hashid into the stringbuilder passed in.
        /// String ids can be present multiple times. This will return the last occurance
        /// of the string. 
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="sb">string will be added to the builder</param>
        /// <returns>The location of the string id, -1 if not found </returns>
        public int GetString(UInt32 hashId, StringBuilder sb)
        {
            int stringLoc = (int)BodyStart;
            int occurance = 0;
            int target_occurance = 1;

            if (mStringSet2 != null && mStringSet2.ContainsKey(hashId))
                target_occurance++;

            UInt32 currentHash = 0;
            while (true)
            {
                currentHash = BinUtils.GetNumberAtLocation(stringLoc, mData);
                if (currentHash == hashId)
                {
                    occurance++;
                    if( occurance == target_occurance)
                        break;
                }
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

        public void SaveToUcfbFile(string filename)
        {
            // for debugging
            System.IO.File.WriteAllBytes(filename, GetUcfbData());
        }

        private string GetKey(int pos, string text)
        {
            string retVal = null;
            StringBuilder sb = new StringBuilder(30);
            string skipThese = "\r\n\" ";
            for (int i = pos; i < text.Length; i++)
            {
                if (text[i] == '=')
                    break;
                if(!skipThese.Contains(text[i]))
                    sb.Append(text[i]);
            }
            retVal = sb.ToString().Trim();
            return retVal;
        }

        private string ParseStringValue(int pos, string text)
        {
            string retVal = null;
            StringBuilder sb = new StringBuilder(30);
            int index = text.IndexOf('\"', pos);
            char prev = '\0';
            if (index > -1)
            {
                for (int i = index + 1; i < text.Length; i++)
                {
                    if (text[i] == '"' && prev != '\\')
                        break;
                    sb.Append(text[i]);
                    prev = text[i];
                }
                retVal = sb.ToString();
            }
            return retVal;
        }

        public void ApplyText(string text)
        {
            GetAllStrings();
            int pos = 0;
            string key = "";
            string value = "";
            Dictionary<string, string> stringsToAdd = new Dictionary<string, string>();
            Dictionary<string, string> stringsToSet = new Dictionary<string, string>();
            uint stringId = 0;
            while (pos < text.Length)
            {
                key = GetKey(pos, text);
                if (String.IsNullOrEmpty(key))
                {
                    pos++;
                    continue;
                }
                if (key.Length > 2 && key[0] == '0' && key[1] == 'x')
                    stringId = UInt32.Parse(key.Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier);
                else 
                    stringId = HashHelper.HashString(key);
                pos += (key.Length);
                value = ParseStringValue(pos, text);
                if (value == null)
                {
                    pos++;
                    Console.WriteLine("Warning! No value found for key:'{0}'", key);
                    continue;
                }
                if (mStringSet.ContainsKey(stringId))
                {
                    if (mStringSet[stringId] != value)
                    {
                        if (stringsToSet.ContainsKey(key))
                            stringsToSet[key] = value;
                        else
                            stringsToSet.Add(key, value);
                    }
                }
                else
                {
                    if( stringsToAdd.ContainsKey(key))
                        stringsToAdd[key] = value;
                    else 
                        stringsToAdd.Add(key, value);
                }
                pos += value.Length;
                // advance to next line
                pos = text.IndexOf('\n', pos);
                if (pos == -1)
                    break;
                pos++;// advance past new line
            }
            foreach (string k in stringsToSet.Keys)
                SetString(k, stringsToSet[k]);

            List<string> hashThese = new List<string>();
            foreach (string k in stringsToAdd.Keys)
            {
                hashThese.Add(k);
                AddString(k, stringsToAdd[k]);
                Console.WriteLine("adding key:'{0}'\n  value:'{1}'", k, stringsToAdd[k]);
            }
            if (hashThese.Count > 0)
                HashHelper.AddStringsToDictionary(hashThese);
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