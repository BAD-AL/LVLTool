using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LVLTool
{
    /// <summary>
    /// Gathers strings from core.lvl files and merges them into a given 'base' core.lvl file
    /// </summary>
    public class CoreMerge
    {
        UcfbHelper mCoreUucfbFileHelper = null;
        
        // 'mStringsToAdd' will be used in the 'Gather' operation
        // key = lang, val = content
        Dictionary<string, StringBuilder> mStringsToAdd = new Dictionary<string, StringBuilder>();

        /// <summary>
        /// The base core.lvl file, typically should be named 'base.core.lvl'
        /// </summary>
        public CoreMerge(string baseCoreFile)
        {
            mCoreUucfbFileHelper = new UcfbHelper();
            mCoreUucfbFileHelper.FileName = baseCoreFile;
            mCoreUucfbFileHelper.InitializeRead();
        }

        /// <summary>
        /// Gather the strings from a core.lvl file or .txt file in the correct string format.
        /// </summary>
        /// <param name="file"></param>
        public void GatherStrings(string file)
        {
            if (file.EndsWith(".lvl", StringComparison.InvariantCultureIgnoreCase))
            {
                UcfbHelper ucfbFileHelper = new UcfbHelper();
                ucfbFileHelper.FileName = file;
                ucfbFileHelper.InitializeRead();

                Chunk cur = null;
                while ((cur = ucfbFileHelper.RipChunk(false)) != null)
                {
                    if (cur.Type == "Locl")
                    {
                        cur.LocHelper = new LocHelper(cur.GetAssetData());
                        string allStrings = cur.LocHelper.GetAllStrings();
                        if (mStringsToAdd.ContainsKey(cur.Name))
                        {
                            StringBuilder sb = mStringsToAdd[cur.Name];
                            if (sb[sb.Length - 1] != '\n')
                                sb.Append("\n");
                            sb.Append(allStrings);
                        }
                        else
                        {
                            mStringsToAdd.Add(cur.Name, new StringBuilder(allStrings));
                        }
                    }
                }
            }
            else if (file.EndsWith(".txt", StringComparison.InvariantCultureIgnoreCase))
            {
                int slashLoc = file.LastIndexOf('\\');
                string langName = "";
                if (slashLoc > -1)
                {
                    langName = file.Substring(slashLoc + 1).Replace(".txt", "");
                    string allStrings = File.ReadAllText(file);
                    if (mStringsToAdd.ContainsKey(langName))
                    {
                        StringBuilder sb = mStringsToAdd[langName];
                        if (sb[sb.Length - 1] != '\n')
                            sb.Append("\n");
                        sb.Append(allStrings);
                    }
                    else
                    {
                        mStringsToAdd.Add(langName, new StringBuilder(allStrings));
                    }
                }
            }
            else
            {
                Console.WriteLine("skipping file '{0}'", file);
            }
        }

        private void AddGatheredStrings()
        {
            if (mStringsToAdd.Count > 0)
            {
                Chunk cur = null;
                List<Chunk> locChunks = new List<Chunk>();
                // rip through the whole thing first.
                while ((cur = mCoreUucfbFileHelper.RipChunk(false)) != null)
                {
                    if (cur.Type == "Locl") locChunks.Add(cur);
                }

                Chunk c = null;
                // process from back to front so we don't mess up the chunk's knowledge of it's position
                for(int i = locChunks.Count-1; i> -1; i--)
                {
                    c = locChunks[i];
                    if (mStringsToAdd.ContainsKey(c.Name))
                    {
                        LocHelper helper = new LocHelper(c.GetAssetData());
                        helper.AddNewStrings(mStringsToAdd[c.Name].ToString() );
                        mCoreUucfbFileHelper.ReplaceUcfbChunk(c, helper.GetUcfbData(), true);
                    }
                }
            }
        }

        /// <summary>
        /// Save to the 
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            AddGatheredStrings();
            mCoreUucfbFileHelper.SaveData(fileName);
        }

        public static void MergeLoc(string baseFileName, string saveFileName, List<string> files)
        {
            CoreMerge cm = new CoreMerge(baseFileName);
            foreach (string file in files)
            {
                Console.WriteLine("Gathering strings from '{0}'", file);
                try
                {
                    cm.GatherStrings(file);
                }
                catch (Exception ex)
                {
                    Program.MessageUser(String.Format("Exception encountered while processing file {0}\n{1}\n{2}", 
                        file, 
                        ex.Message,
                        ex.StackTrace ));
                }
            }
            cm.Save(saveFileName);
        }

    }
}
