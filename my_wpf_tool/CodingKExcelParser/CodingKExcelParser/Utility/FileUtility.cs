using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKExcelParser.Utility
{
    public static class FileUtility
    {
        public static void WriteBATFile(string filePath, string[] fileContent)
        {
            filePath = Path.GetFullPath(filePath);
            if (!File.Exists(filePath))
            {
                FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.Write);//创建写入文件
                StreamWriter sw = new StreamWriter(fs1);
                foreach (string line in fileContent)
                {
                    sw.WriteLine(line);//开始写入值
                }
                sw.WriteLine("pause");
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write);
                fs.Seek(0, SeekOrigin.Begin);
                fs.SetLength(0);
                StreamWriter sr = new StreamWriter(fs);
                foreach (string line in fileContent)
                {
                    sr.WriteLine(line);//开始写入值
                }
                sr.WriteLine("pause");
                sr.Close();
                fs.Close();
            }
        }

        public static void ExecuteBATFile(string dicPath, string filePath)
        {
            Process proc = null;
            try
            {
                string targetDir = dicPath;
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = filePath;
                //proc.StartInfo.Arguments = ;
                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }

        public static void CopyFile(string pLocalFilePath,string pSaveFilePath)
        {
            var latIndex = pSaveFilePath?.LastIndexOf("\\") ?? pSaveFilePath.Length;
            var saveDic = pSaveFilePath.Substring(0, latIndex);
            ExistDic(saveDic);

            if (File.Exists(pLocalFilePath))
            {
                File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
            }
        }

        public static void ExistDic(params string[] dicPaths)
        {
            foreach (var dicPath in dicPaths)
            {
                if (!Directory.Exists(dicPath))
                {
                    Directory.CreateDirectory(dicPath);
                }
            }
        }
    }
}
