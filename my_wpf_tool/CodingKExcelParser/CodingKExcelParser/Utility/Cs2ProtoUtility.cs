using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKExcelParser.Utility
{
    public static class Cs2ProtoUtility
    {
        public static List<string> content = new List<string>();
        public static string head = string.Empty;

        public static void ConvertCsToProto(string inputPath,string outputPath)
        {
            head = string.Empty;
            content.Clear();
            ReadCS(inputPath);
            WriteProto(outputPath);
        }

        public static void ReadCS(string filePath)
        {
            string txt = "";

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                // {} 嵌套层数
                int nestedCount = 0;
                bool isInEnum = false;
                bool isInClass = false;
                int index = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    // get head
                    if (head == string.Empty)
                    {
                        if (line.Contains("namespace "))
                        {
                            // TODO test
                            head = line.Substring(line.LastIndexOf("namespace ") + 9).Trim();
                        }
                    }

                    // get content
                    if (!isInEnum && !isInClass)//nestedCount == 0) 不支持嵌套
                    {
                        if (line.Contains("class "))
                        {
                            string newLine;
                            // 去掉继承。
                            if (line.Contains(":"))
                            {
                                newLine = line.Substring(0, line.IndexOf(":"));
                            }
                            else
                            {
                                newLine = line;
                            }

                            newLine = "message " + newLine.Substring(newLine.LastIndexOf("class ") + 6).Trim() + "{";


                            content.Add(newLine);
                            isInClass = true;
                            index = 0;
                            nestedCount++;
                        }
                        else if (line.Contains("enum "))
                        {
                            var newLine = "enum " + line.Substring(line.LastIndexOf("enum ") + 5).Trim() + "{";
                            content.Add(newLine);
                            isInEnum = true;
                            nestedCount++;
                        }
                    }
                    else
                    {
                        if (isInEnum)
                        {
                            if (line.Contains("}"))
                            {
                                content.Add("}");
                                content.Add("");
                                nestedCount--;
                                isInEnum = false;
                            }
                            else if (line.Contains("{"))
                            {

                            }
                            else
                            {
                                content.Add(line.Replace(",", ";"));
                            }
                        }
                        else if (isInClass)
                        {
                            if (line.Contains(";"))
                            {
                                ++index;
                                var newLine = line.Replace(" { get; set; }", "").Replace(";", $" = {index};");

                                if (newLine.Contains("List <") || newLine.Contains("List<"))
                                {
                                    // List
                                    newLine = newLine.Replace("public", "repeated").Replace("List <", "")
                                        .Replace("List<", "").Replace(">", "");
                                }
                                else if (newLine.Contains("public"))
                                {
                                    // field
                                    newLine = newLine.Replace("public", "required").Replace("int", "int32");
                                }

                                content.Add(newLine);
                            }
                            else if (line.Contains("}"))
                            {
                                content.Add("}");
                                content.Add("");
                                nestedCount--;
                                isInClass = false;
                                index = 0;
                            }
                            else if (line.Contains("{"))
                            {

                            }
                        }
                    }
                }
            }
        }

        public static void WriteProto(string outputPath)
        {
            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                sw.WriteLine("syntax = \"proto2\";");
                sw.WriteLine("");
                sw.WriteLine("package " + head + ";");
                sw.WriteLine("");

                foreach (string line in content)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
