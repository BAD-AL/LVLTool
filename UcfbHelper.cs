using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LVLTool
{
    /// <summary>
    /// Class for extracting UCF (UCFB, USFT) files (Zero Engine)
    /// </summary>
    public class UcfbHelper
    {
        public byte[] Data
        {
            get { return mData; }
        }

        private byte[] mData = null;
        static byte[] UCFB = new byte[] { (byte)'u', (byte)'c', (byte)'f', (byte)'b'};
        static byte[] NAME = new byte[] { (byte)'N', (byte)'A', (byte)'M', (byte)'E' };
        static byte[] INFO = new byte[] { (byte)'I', (byte)'N', (byte)'F', (byte)'O' };

        private static string[] sPopularSuffixes = new String[] {
            "_1ctf","_1flag","_Buildings","_Buildings","_Buildings01","_Buildings02","_CP-Assult","_CP-Conquest",
            "_CP-VehicleSpawns","_CP-VehicleSpawns","_CPs","_CommonDesign","_CW-Ships","_GCW-Ships","_Damage",
            "_Damage01","_Damage02","_Death","_DeathRegions","_Design","_Design001","_Design002","_Design01",
            "_Design02","_Design1","_Design2","_Doors","_Layer000","_Layer001","_Layer002","_Layer003","_Layer004",
            "_Light_RG","_NewObjective","_Objective","_Platforms","_Props","_RainShadow","_Roids","_Roids01",
            "_Roids02","_Shadow_RGN","_Shadows","_Shields","_SoundEmmiters","_SoundRegions","_SoundSpaces",
            "_SoundTriggers","_Temp","_Tree","_Trees","_Vehicles","_animations","_campaign","_collision",
            "_con","_conquest","_ctf","_deathreagen","_droids","_eli","_flags","_gunship","_hunt","_invisocube",
            "_light_region","_objects01","_objects02","_reflections","_rumble","_rumbles","_sound","_tdm","_trees",
            "_turrets","_xl","_sniper"
        };
        /// <summary>
        /// Default constructor
        /// </summary>
        public UcfbHelper() { }

        private string mFileName = "";
        /// <summary>
        /// The filename this extractor is working on.
        /// </summary>
        public string FileName
        {
            get { return mFileName; }
            set
            {
                if (mFileName != value)
                {
                    mFileName = value;
                    OnFileNameChanged(mFileName);
                }
            }
        }

        private void OnFileNameChanged(string fileName)
        {
            mData = File.ReadAllBytes(fileName);

            int lastSlash = fileName.LastIndexOf("\\");
            if (lastSlash > -1)
            {
                string baseFileName = fileName.Substring(lastSlash + 1).Replace(".lvl", "");
                HashHelper.AddHashedString(baseFileName);
                HashHelper.AddHashedString("mapname.description." + baseFileName);
                HashHelper.AddHashedString("mapname.name." + baseFileName);
                foreach (string suffix in sPopularSuffixes)
                {
                    HashHelper.AddHashedString(baseFileName + suffix);
                }
            }
        }

        private string GetLvlFileName()
        {
            string fn = FileName.Substring(FileName.LastIndexOf('\\') + 1);
            return fn;
        }

        private string GetExtractDir()
        {
            int lastDot = FileName.LastIndexOf('.');
            string path = FileName.Substring(0, lastDot) + "\\";
            return path;
        }

        private string CreateExtractDir()
        {
            string path = GetExtractDir();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        private string GetFileName(string name, string fileType, bool withDir)
        {
            string retVal = "";
            Dictionary<string, string> fileTypeExtensions = 
                new Dictionary<string, string>() {
                    {"mcfg",".config"},
                    {"snd_",".config"},
                    {"zaf_",".zafbin"},
                    {"zaa_",".zaabin"},
                    {"lvl_", ".lvl" },
                    {"Locl", ".loc"},
                    {"tex_", ".texture" },
                    {"LuaP", ".script"},
                    {"scr_", ".script"},
                    {"SHDR", ".shader"},
                    {"entc" , ".class" },
                    {"skel" , ".model" },
                    {"ANIM" , ".anims" },
                    {"bnd_" , ".boundary" },
                    {"plan" , ".congraph" },
                    {"fx__" , ".envfx" },
                    {"lght" , ".light" },
                    {"wrld" , ".world" },
            };
            string prefix = "";
            string suffix = "";

            if( fileTypeExtensions.ContainsKey(fileType))
                suffix = fileTypeExtensions[fileType];
            else
                suffix = "." + fileType;
            
            if (withDir)
                prefix = GetExtractDir();

            string fileName = prefix +  name + suffix;
            int num = 0;
            while (mManifest.IndexOf(fileName) > -1)
            {
                num++;
                fileName = prefix + name + "_" + num + suffix;
            }
            if (fileName.IndexOf("xml.shader") > -1)
            {
                fileName = fileName.Replace("xml.shader", "shader");
            }
            retVal = fileName;
            return retVal;
        }

        internal static void SaveFileUCFB(string name, byte[] data)
        {
            byte[] encodedLen = BinUtils.EncodeNumber(data.Length );
            byte[] buffer = new byte[8];
            buffer[0] = (byte)'u';
            buffer[1] = (byte)'c';
            buffer[2] = (byte)'f';
            buffer[3] = (byte)'b';

            buffer[4] = encodedLen[0];
            buffer[5] = encodedLen[1];
            buffer[6] = encodedLen[2];
            buffer[7] = encodedLen[3];

            FileStream fs = null;
            try
            {
                fs = new FileStream(name, FileMode.OpenOrCreate);
                fs.Write(buffer, 0, buffer.Length);
                fs.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Program.MessageUser("Error Saving file: " + name +"\n" + ex.Message);
            }
            finally
            {
                if (fs != null) 
                    fs.Close();
            }
        }


        uint mCurrentPos = 0;

        private void ConsumeBytes(uint num)
        {
            mCurrentPos += num;
        }

        bool HasName()
        {
            return BinUtils.BinCompare(NAME, mData, mCurrentPos);
        }

        bool HasInfo()
        {
            return BinUtils.BinCompare(INFO, mData, mCurrentPos);
        }

        private uint ReadNumber()
        {
            uint retVal =  BinUtils.GetNumberAtLocation(mCurrentPos, mData);
            ConsumeBytes(4);
            return retVal;
        }

        private uint PeekNumber(uint location)
        {
            uint retVal = BinUtils.GetNumberAtLocation(location, mData);
            return retVal;
        }

        private string ReadString(int len)
        {
            string retVal = BinUtils.GetByteString(mCurrentPos, mData, len);
            ConsumeBytes((uint)len);
            return retVal;
        }

        private string ReadChunkType()
        {
            string ct = PeekChunkType();
            ConsumeBytes(4);
            return ct;
        }

        private string PeekChunkType()
        {
            string ct = BinUtils.GetByteString(mCurrentPos, mData, 4);
            return ct;
        }

        // ok, maybe I should pass a buffer, but this is so easy. :D
        private byte[] ReadBytes(uint numBytes)
        {
            uint num = numBytes;
            if (num > mData.Length - mCurrentPos)
                num = (uint)(mData.Length - mCurrentPos);

            byte[] retVal = new byte[num];
            Array.Copy(mData, mCurrentPos, retVal, 0, num);
            ConsumeBytes(num);
            return retVal;
        }

        private void AlignHead()
        {
            ConsumeBytes(mCurrentPos % 4);// this will often be 0
        }

        private string PeekName(uint location, string chunkType)
        {
            string name = "";
            if (chunkType == "plan")
            {
                string lvlName = GetLvlFileName();
                return lvlName.Replace(".lvl", "");
            }
            long loc = 0;
            if (chunkType == "lvl_")
            {
                uint hash = BinUtils.GetNumberAtLocation(location+8, mData);
                string str = HashHelper.GetStringFromHash(hash);
                if( str != null )
                    name = str;
                else 
                    name = "0x" + hash.ToString("X");
            }
            if (chunkType == "entc")
            {
                loc = BinUtils.GetLocationOfGivenBytes(location, ASCIIEncoding.ASCII.GetBytes("TYPE"), mData, 80);
            }
            else
            {
                loc = BinUtils.GetLocationOfGivenBytes(location, ASCIIEncoding.ASCII.GetBytes("NAME"), mData, 80);
            }
            if (loc > -1)
            {
                int nameLen = mData[(int)loc + 4] - 1; // -1 for null byte
                if (loc > 0)
                {
                    name = Encoding.ASCII.GetString(mData, (int)loc + 8, (int)nameLen);
                    int zeroTest = name.IndexOf('\0');
                    if (zeroTest > -1)
                        name = name.Substring(0, zeroTest);
                }
                List<string> hexNameTypes = new List<string> { "mcfg", "sanm", "fx__" };
                if (nameLen == 3 && (hexNameTypes.IndexOf(chunkType) > -1 || HasBadCharacters(name)))//sf__
                {
                    uint hash = BinUtils.GetNumberAtLocation(loc + 8, mData);
                    string str = HashHelper.GetStringFromHash(hash);
                    if (str != null)
                        name = str;
                    else
                        name = String.Format("0x{0:X}", hash);
                }
            }
            return name;
        }

        private bool HasBadCharacters(string tst)
        {
            bool retVal = false;
            if (tst.IndexOf('?') > -1)
                retVal = true;
            else
            {
                for (int i = 0; i < tst.Length; i++)
                {
                    if (Char.IsLetterOrDigit(tst[i]) || tst[i] == '_')
                    {
                        //cool
                    }
                    else
                    {
                        retVal = true;
                        break;
                    }
                }
            }
            return retVal;
        }

        private string ReadNameHash()
        {
            string retVal = null;
            if (BinUtils.BinCompare(NAME, mData, mCurrentPos))
            {
                ConsumeBytes(4);
                uint nameLen = BinUtils.GetNumberAtLocation(mCurrentPos, mData);
                ConsumeBytes(4);
                uint nameHash = BinUtils.GetNumberAtLocation(mCurrentPos, mData);
                ConsumeBytes(4);
                retVal = String.Format("0x{0:x}", nameHash);
            }
            return retVal;
        }

        private string ReadName()
        {
            string retVal = null;
            if (BinUtils.BinCompare(NAME, mData, mCurrentPos))
            {
                ConsumeBytes(4);
                uint nameLen = BinUtils.GetNumberAtLocation(mCurrentPos, mData);
                ConsumeBytes(4);
                retVal = ReadString((int)nameLen);
            }
            return retVal;
        }

        private List<string> mManifest = new List<string>();
        private int mPlanFileCount = 0;

        /// <summary>
        /// Returns the path to the extraction folder
        /// </summary>
        /// <returns></returns>
        public string ExtractContents()
        {
            mCurrentPos = 0;
            //mPlanFileCount = 0;
            string retVal =  CreateExtractDir();
            //read file header
            if (!BinUtils.BinCompare(UCFB, mData, mCurrentPos))
            {
                Program.MessageUser("ERROR! This is not a UCF file: "+ FileName);
                return null;
            }
            //ConsumeBytes(4); // advance past ucfb header
            uint bytesLeftInFile = InitializeRead();
            if (mData.Length - (mCurrentPos + bytesLeftInFile) != 0)
            {
                Program.MessageUser("WARNING! ucfb length data kinda sketchy: " + FileName);
            }

            mManifest.Clear();

            try
            {
                while (RipChunk(true) != null) ;
            }
            catch (System.IndexOutOfRangeException) { }
            WriteManifest();
            if (FileName.EndsWith(".lvl"))
            {
                string reqFileName = FileName.Replace(".lvl", ".req");
                int lastSlash = reqFileName.LastIndexOf("\\");
                reqFileName = reqFileName.Substring(lastSlash+1);
                reqFileName = retVal + reqFileName;
                string req = ReqMaker.GetReq(retVal);
                File.WriteAllText(reqFileName, req);
            }
            return retVal;
        }

        private void WriteManifest()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string entry in mManifest)
            {
                builder.Append(entry);
                builder.Append("\n");
            }
            string fileName = GetFileName("__manifest", "txt", true);
            File.WriteAllText(fileName, builder.ToString());
        }

        public Chunk RipChunk(bool saveChunk)
        {
            Chunk retVal = null;
            string ct = PeekChunkType();
            if (string.IsNullOrEmpty(ct))
                return null; // Early Return 
            uint chunkLen = 0;
            string name = null;
            try
            {
                chunkLen = PeekNumber(mCurrentPos + 4);
                name = PeekName(mCurrentPos + 8, ct);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("RipChunk Error: " + e.Message);
                return null;
            }
            //if (!String.IsNullOrEmpty(name) && !name.StartsWith("0x")) HashHelper.AddHashedString(name); 
            
            retVal = new Chunk();
            retVal.Type = ct;
            retVal.Length = chunkLen;
            retVal.Name = name;
            retVal.Start = mCurrentPos;

            byte[] data;

            if (ct == "lvl_")
            {
                ConsumeBytes(16);
                data = ReadBytes(chunkLen+8);
            }
            else
            {
                data = ReadBytes(chunkLen + 8);
            }
            retVal.Data = data;

            AlignHead();
            string fileName = GetFileName(name, ct, true);
            if (ct == "plan")
            {
                fileName = fileName.Replace(".plan","_"+ mPlanFileCount + ".plan");
                mPlanFileCount++;
            }
            if (saveChunk)
            {
                SaveFileUCFB(fileName, data);
            }
            if (mManifest.IndexOf(fileName) > -1 && saveChunk)
            {
                Console.WriteLine("Warning: overwriting file "+ fileName);
            }
            mManifest.Add(fileName);
            return retVal;
        }

        public void ReplaceUcfbChunk(Chunk chk, string mungedFileName, bool replaceFirstOnly)
        {
            byte[] mungedFileBytes = File.ReadAllBytes(mungedFileName);
            ReplaceUcfbChunk(chk, mungedFileBytes, replaceFirstOnly);
        }

        public void ReplaceUcfbChunk(Chunk chk, byte[] mungedFileBytes, bool replaceFirstOnly)
        {
            uint bytesLeftInFile = InitializeRead();
            string curType = PeekChunkType();
            string curName = PeekName(mCurrentPos + 8, curType);
            string cur = curName + "." + curType;
            string target = chk.Name + "." + chk.Type;

            //while ( RipChunk(false) != null )
            do
            {
                curType = PeekChunkType();
                if (string.IsNullOrEmpty(curType))
                    break;
                curName = PeekName(mCurrentPos + 8, curType);
                cur = curName + "." + curType;
                //}
                if (target.Equals(cur, StringComparison.CurrentCultureIgnoreCase))
                {
                    // we've advanced to the correct place
                    uint chunk_start = mCurrentPos;
                    uint chunk_end = PeekNumber(mCurrentPos + 4) + mCurrentPos + 8;
                    int mungedContentLength = mungedFileBytes.Length - 8;
                    uint chunk_len = chunk_end - chunk_start;

                    int difference = (int)(chunk_len - (mungedContentLength));
                    int newLength = mData.Length - difference;
                    byte[] newData = new byte[newLength];
                    Array.Copy(mData, 0, newData, 0, chunk_start /*- 1*/); // copy first chunk
                    Array.Copy(mungedFileBytes, 8, newData, chunk_start, mungedFileBytes.Length - 8); //splice
                    long next_start = chunk_start + mungedFileBytes.Length - 8;// MAYBE +1 ? 
                    if (mData.Length - chunk_end != 0)
                    {
                        Array.Copy(mData, chunk_end, newData, next_start, (mData.Length - chunk_end) - 1);
                    }
                    mData = newData;

                    //Update UCFB Header
                    byte[] encodedLen = BinUtils.EncodeNumber(mData.Length - 8);
                    mData[4] = encodedLen[0];
                    mData[5] = encodedLen[1];
                    mData[6] = encodedLen[2];
                    mData[7] = encodedLen[3];
                    if (replaceFirstOnly)
                        break;
                }
            } while (RipChunk(false) != null);
        }

        public uint InitializeRead()
        {
            mCurrentPos = 0;
            mPlanFileCount = 0;
            ConsumeBytes(4); // advance past ucfb header
            return ReadNumber();
        }

        public void SaveData(string fileName)
        {
            File.WriteAllBytes(fileName, mData);
            Console.WriteLine("Saved file: '{0}'", fileName);
        }

        //private bool MatchType(string cur, string ct)
        //{
        //    if ( (cur == "snd_") && (ct == "mcfg") )
        //        return true;
        //    else
        //        return cur == ct;
        //}

        //private string GetChunkName(string filename)
        //{
        //    string retVal = null;
        //    int slashIndex = filename.LastIndexOf(Path.DirectorySeparatorChar);
        //    int dotIndex = filename.LastIndexOf('.');
        //    if (slashIndex > 0 && dotIndex > 0)
        //    {
        //        retVal = filename.Substring(slashIndex+1, dotIndex - (slashIndex+1)); //TODO: check this 
        //    }
        //    return retVal;
        //}

        //private string GetChunkType(string filename)
        //{
        //    string retVal = null;
        //    string ext = "";
        //    int index = filename.LastIndexOf('.');
        //    if (index > -1)
        //    {
        //        ext = filename.Substring(index);
        //        switch (ext)
        //        {
        //            case ".config": retVal = "mcfg"; break;
        //            //case ".config": retVal = "snd_"; break;
        //            case ".zafbin": retVal = "zaf_"; break;
        //            case ".zaabin": retVal = "zaa_"; break;
        //            case ".lvl":    retVal = "lvl_"; break;
        //            case ".loc":    retVal = "Locl"; break;
        //            case ".texture":retVal = "tex_"; break;
        //            case ".script": retVal = "scr_"; break;
        //            case ".shader": retVal = "SHDR"; break;
        //            case ".class":  retVal = "entc"; break;
        //            case ".model":  retVal = "skel"; break;
        //            case ".anims": retVal = "ANIM"; break;
        //            case ".boundary": retVal = "bnd_"; break;
        //            case ".congraph": retVal = "plan"; break;
        //            case ".envfx": retVal = "fx__"; break;
        //            case ".light": retVal = "lght"; break;
        //            case ".world": retVal = "wrld"; break;
        //        }
        //    }
        //    return retVal;
        //}

        /// <summary>
        /// Throws exception if file isn't ucfb
        /// </summary>
        /// <param name="mungedFile">the mungd file to add to the end</param>
        public void AddItemToEnd(string mungedFile)
        {
            Munger.VerifyUcfbFile(mungedFile);// this will throw if file isn't ucfb
            byte[] newData = File.ReadAllBytes(mungedFile);
            
            List<byte> bigData = new List<byte>(this.mData);
            int newDataStart = bigData.Count;
            bigData.AddRange(newData);

            // throw away the first 8 bytes of munged data
            bigData.RemoveRange(newDataStart, 8);
            
            mData = bigData.ToArray();

            int headerLength = mData.Length - 8; // gotta put this at locs 4-7
            mData[4] = (byte)(headerLength & 0x000000FF);
            mData[5] = (byte)((headerLength & 0x0000FF00) >> 8);
            mData[6] = (byte)((headerLength & 0x00FF0000) >> 16);
            mData[7] = (byte)((headerLength & 0xFF000000) >> 24);

            Console.WriteLine("Added '{0}'", mungedFile);
        }
    }

    public class Chunk
    {
        public string Name   { get; set; }
        public string Type   { get; set; }
        public uint   Length { get; set; }
        public uint   Start  { get; set; }
        public byte[] Data   { get; set; }

        public LocHelper LocHelper { get; set; }

        public override string ToString()
        {
            string retVal = String.Format("{0}.{1}",Name, Type);
            return retVal;
        }

        public byte[] GetAssetData()
        {
            return Data;
        }

        public byte[] GetBodyData()
        {
            byte[] BodyBytes = { 0x42, 0x4f, 0x44, 0x59 }; 
            long loc = BinUtils.GetLocationOfGivenBytes(0, BodyBytes, Data, 80);
            if (loc > -1)
            {
                uint bodyLen = BinUtils.GetNumberAtLocation( loc + 4, Data);
                long bodyStart = loc + 8;
                long bodyEnd = loc + 8 + bodyLen;

                byte[] bodyData = new byte[bodyLen];
                Array.Copy(Data, bodyStart, bodyData, 0, bodyLen);
                return bodyData;
            }
            return null;
        }

        public string GetSummary()
        {
            string retVal = string.Format(
                "Name:   {0}\nType:   {1}\nStart:  0x{2:x}\nLength: 0x{3:x}\nSize:   {4:n} kb",
                Name,Type,Start,Length, (Length / 1024.0) );

            long loc = BinUtils.GetLocationOfGivenBytes(0, ASCIIEncoding.ASCII.GetBytes("INFO"), Data, 80);
            if (loc > -1)
            {
                loc += 4; // advance to position to read 
                uint info =  BinUtils.GetNumberAtLocation(loc, Data);
                retVal += String.Format("\nINFO:   0x{0:x8}", info);
            }

            if (Name.StartsWith("0x"))
            {
                uint hash = UInt32.Parse(Name.Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier);
                string unHashed = HashHelper.GetStringFromHash(hash);
                if (unHashed != null)
                    retVal += string.Format("hash lookup for: {0}>'{1}' \n", Name, unHashed);
                else
                    retVal += "Unknown name hash\n";
            }
            
            return retVal;
        }
    }
}
