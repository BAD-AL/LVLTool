using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace LVLTool
{
    public class HashHelper
    {
        const string DictionaryFile = "Dictionary.txt";

        public static UInt32 HashString(string input)
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

        public static void AddStringsToDictionary(List<string> stringsToAdd)
        {
            uint hashId = 0;
            StringBuilder sb = new StringBuilder(500);
            foreach (string str in stringsToAdd)
            {
                hashId = HashString(str);
                if (!sHashes.ContainsKey(hashId))
                {
                    AddHashedString(str);
                    sb.Append(str);
                    sb.Append("\r\n");
                }
            }
            if (sb.Length > 0)
            {
                StreamWriter wr = null;
                try
                {
                    wr = new StreamWriter(DictionaryFile, true);
                    wr.Write(sb.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error updating " + DictionaryFile +"\n"+
                        e.Message);
                }
                finally
                {
                    if( wr != null)
                        wr.Close();
                }
            }
        }

        private static void ReadDictionary()
        {
            
            StreamReader reader = null;
            string line = "";
            sHashes = new Dictionary<uint, string>(500);
            try
            {
                Console.WriteLine("Reading dictionary...");
                reader = new StreamReader(DictionaryFile);
                while ((line = reader.ReadLine()) != null)
                {
                    AddHashedString(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error processing file " + DictionaryFile + "\n" + e.Message );
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            Console.WriteLine("Done Reading dictionary.");
        }


    }
}
