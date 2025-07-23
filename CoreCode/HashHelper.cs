using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.IO.Compression;

namespace LVLTool
{

    interface IAppendString
    {
        void AppendString(string s);
    }

    public class HashHelper
    {
        IAppendString mAppendStringObj = null;

        private static string DictionaryFile
        {
            get
            {
                if (sDictionaryFile == null)
                {
                    // first check the current Dir for Dictionary.txt
                    if (File.Exists("Dictionary.txt"))
                    {
                        sDictionaryFile = "Dictionary.txt";
                    }
                    else
                    {
                        string assemblyFullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                        int lastSlash = assemblyFullPath.LastIndexOf("\\");
                        sDictionaryFile = assemblyFullPath.Substring(0, lastSlash + 1) + "Dictionary.txt";
                    }
                }
                return sDictionaryFile;
            }
        }

        private static string sDictionaryFile = null;

        public static UInt32 HashString(string input)
        {
            return HashString(input.ToCharArray());
            /*UInt32 FNV_prime = 16777619;
            UInt32 offset_basis = 2166136261;
            UInt32 hash = offset_basis;
            byte c = 0;
            foreach (char c_ in input)
            {
                c = (byte)c_;
                c |= 0x20;
                hash ^= c;
                hash *= FNV_prime;
            }
            return hash;*/
        }

        public static UInt32 HashString(char[] input)
        {
            UInt32 FNV_prime = 16777619;
            UInt32 offset_basis = 2166136261;
            UInt32 hash = offset_basis;
            byte c = 0;
            foreach (char c_ in input)
            {
                c = (byte)c_;
                c |= 0x20;
                hash ^= c;
                hash *= FNV_prime;
            }
            return hash;
        }

        public static string ReverseLookup(uint key)
        {
            String retVal = null;
            if (sHashes == null)
                ReadDictionary();

            if (sHashes.ContainsKey(key))
                retVal = sHashes[key];
            return retVal;
        }

        private static Dictionary<UInt32, string> sHashes = null; // new Dictionary<uint, string>(500);

        internal static void AddHashedString(string input)
        {
            if (sHashes == null)
            {
                ReadDictionary();
            }
            UInt32 hash = HashString(input);
            if (!sHashes.ContainsKey(hash))
                sHashes.Add(hash, input);
        }

        public static string GetStringFromHash(UInt32 hash)
        {
            string retVal = null;
            if (sHashes == null)
            {
                ReadDictionary();
            }
            if (sHashes.ContainsKey(hash))
                retVal = sHashes[hash];
            return retVal;
        }

        /// <summary>
        /// Adds strings to the dictionary
        /// </summary>
        /// <param name="stringsToAdd"></param>
        /// <returns>the number of items successfully added</returns>
        public static int AddStringsToDictionary(List<string> stringsToAdd)
        {
            int count = 0;
            uint hashId = 0;
            StringBuilder sb = new StringBuilder(500);

            if (sHashes == null) ReadDictionary();
            string str = "";

            foreach (string tmp in stringsToAdd)
            {
                str = tmp.Trim();
                if (str.Length > 1)
                {
                    hashId = HashString(str);
                    if (!sHashes.ContainsKey(hashId))
                    {
                        AddHashedString(str);
                        count++;
                        sb.Append(str);
                        sb.Append("\r\n");
                    }
                }
            }
            if (sb.Length > 0)
            {
                StreamWriter wr = null;
                try
                {
                    if (File.Exists(DictionaryFile))
                    {
                        wr = new StreamWriter(DictionaryFile, true);
                        wr.Write(sb.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error updating " + DictionaryFile + "\n" +
                        e.Message);
                }
                finally
                {
                    if (wr != null)
                        wr.Close();
                }
            }
            return count;
        }

        private static void ReadDictionary()
        {
            StreamReader reader = null;
            Stream compressedStream = null;
            GZipStream decompressionStream = null;

            string line = "";
            sHashes = new Dictionary<uint, string>(500);
            try
            {
                if (File.Exists(DictionaryFile))
                {
                    Console.WriteLine("info: Reading '{0}...", DictionaryFile);
                    reader = new StreamReader(DictionaryFile);
                }
                else
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    // when debugging use the line below to look through the different resources in 'resourceNames' to see the name of the target resource
                    //String[] resourceNames = assembly.GetManifestResourceNames();
                    //reader = new StreamReader(assembly.GetManifestResourceStream("LVLTool.dictionary.txt"));
                    compressedStream = assembly.GetManifestResourceStream("LVLTool.dictionary.txt.gz");
                    if (compressedStream == null) throw new InvalidOperationException("Could not find the embedded resource.");
                    decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress);
                    reader = new StreamReader(decompressionStream, Encoding.UTF8);

                }
                while ((line = reader.ReadLine()) != null)
                {
                    AddHashedString(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error processing Dictionary. linesProcessed:{0} '{1}'", sHashes.Count, e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (decompressionStream != null) decompressionStream.Close();
                if (compressedStream != null) compressedStream.Close();
            }
        }

        public void PrintMatches(UInt32 hash, string possibleCharacters, int targetStringSize, string prefix)
        {
            char[] bufferToUse = new char[targetStringSize];
            Array.Copy(prefix.ToCharArray(), bufferToUse, prefix.Length);
            GenerateStringsAndCheckHash(prefix.Length, targetStringSize, hash, possibleCharacters, bufferToUse);
        }

        private void GenerateStringsAndCheckHash(int index, int targetStringSize, UInt32 targetHash, string possibleCharacters, char[] bufferToUse)
        {
            if (index == targetStringSize)
            {
                if (HashString(bufferToUse) == targetHash)
                {
                    string str = String.Format("Match found: {0}\n", new string(bufferToUse));
                    if (mAppendStringObj != null)
                        mAppendStringObj.AppendString(str);
                    else
                        Console.Write(str);
                }
            }
            else
            {
                foreach (char c in possibleCharacters)
                {
                    bufferToUse[index] = c;
                    GenerateStringsAndCheckHash(index + 1, targetStringSize, targetHash, possibleCharacters, bufferToUse);
                }
            }
        }

        public static void WriteDictionary()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<UInt32, string> kvp in sHashes)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                if(kvp.Value.IndexOf('.') > -1)
                    sb.Append(kvp.Value.ToLower());
                else
                    sb.Append(kvp.Value);
                sb.Append("\n");
            }
            File.WriteAllText(DictionaryFile, sb.ToString());
        }

    }
}
