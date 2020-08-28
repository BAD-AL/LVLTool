using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LVLTool
{
    public class HashHelper
    {
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

        private static void AddHashedString(string input)
        {
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

        private static void ReadDictionary()
        {
            string fileName = "Dictionary.txt";
            StreamReader reader = null;
            string line = "";
            sHashes = new Dictionary<uint, string>(500);
            try
            {
                Console.WriteLine("Reading dictionary...");
                reader = new StreamReader(fileName);
                while ((line = reader.ReadLine()) != null)
                {
                    AddHashedString(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error processing file " + fileName + "\n" + e.Message );
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
