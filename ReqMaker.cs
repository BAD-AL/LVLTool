using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LVLTool
{
    public class ReqMaker
    {

        public static String GetReq(string directory)
        {
            // config, script, texture, lvl, 
            StringBuilder builder = new StringBuilder();
            builder.Append("ucft\n{\n");

            AddFiles(directory, builder, "config");
            AddFiles(directory, builder, "script");
            AddFiles(directory, builder, "texture");
            AddFiles(directory, builder, "loc");
            AddFiles(directory, builder, "lvl");
            
            builder.Append("\n}");
            return builder.ToString();
        }

        private static void AddFiles(string directory, StringBuilder builder, string type)
        {
            string pattern = "*." + type;
            List<string> files = GetFiles(directory, pattern);
            string file = "";
            int lastSlash = 0;
            if (files.Count > 0)
            {
                builder.Append("\n\tREQN\n\t{\n");
                builder.Append(String.Format("\t\t\"{0}\"\n", type));
                for (int i = 0; i < files.Count; i++)
                {
                    file = files[i].Replace("." + type, "");
                    lastSlash = file.LastIndexOf("\\") + 1;
                    file = file.Substring(lastSlash);
                    builder.Append(String.Format("\t\t\"{0}\"\n", file));
                }
                builder.Append("\t}\n");
            }
        }



        private static List<string> GetFiles(string directory, string pattern)
        {
            string[] files = Directory.GetFiles(directory, pattern, SearchOption.TopDirectoryOnly);
            return new List<string>(files);
        }
    }
}
