using System;
using System.Collections.Generic;
using System.Text;

namespace CodingK_Extension
{
    public static class MyExtension
    {
        /// <summary>
        /// 打印出bytes来看序列化状态下的字符串是啥样的
        /// </summary>
        /// <param name="readBuffer"></param>
        /// <returns></returns>
        public static string BytesToString(this byte[] readBuffer)
        {
            var test = "";
            int count = readBuffer.Length;
            int charCount = Encoding.Default.GetCharCount(readBuffer, 0, count);
            var readCharArray = new char[charCount];

            Encoding.Default.GetDecoder().GetChars(readBuffer, 0, count, readCharArray, 0);
            for (int i = 0; i < readCharArray.Length; i++)
            {
                test += readCharArray[i];
            }

            return test;
        }
    }
}
