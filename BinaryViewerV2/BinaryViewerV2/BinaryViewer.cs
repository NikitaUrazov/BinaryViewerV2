using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryViewerV2
{
    internal class BinaryViewer
    {
        private static bool isOpen = false;
        public static bool IsOpen { get { return isOpen; } }
        private static FileStream fs;
        public static long Length { get { return fs.Length; } }
        private static StringBuilder sb = new StringBuilder();
        private static List<string> result = new List<string>();
        public static List<string> Result { get { return result; } }

        public static void Open(string filePath)
        {
            if (File.Exists(filePath))
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                isOpen = true;
            }                
        }

        public static void ReadForward(long offsetStart, long offsetEnd)
        {
            result.Clear();

            fs.Position = offsetStart;

            int chunkSize = 16;
            byte[] bytes = new byte[chunkSize];

            while (fs.Position <= offsetEnd && !(fs.Position >= fs.Length))
            {
                sb.Clear();
                sb.Append($"{fs.Position.ToString("X8")}: ");

                int bytesToRead = chunkSize;
                long bytesLeft = fs.Length - fs.Position;
                if (bytesLeft < bytesToRead)
                    bytesToRead = (int)bytesLeft;

                fs.Read(bytes, 0, bytesToRead);

                for (int i = 0; i < bytesToRead; i++)
                    sb.Append($"{bytes[i].ToString("X2")} ");

                for (int i = bytesToRead; i < bytes.Length; i++)
                    sb.Append("   ");

                sb.Append("| ");

                for (int i = 0; i < bytesToRead; i++)
                {
                    if (bytes[i] >= 33 && bytes[i] <= 126)
                        sb.Append($"{(char)bytes[i]} ");
                    else
                        sb.Append($". ");
                }

                for (int i = bytesToRead; i < bytes.Length; i++)
                    sb.Append("  ");

                result.Add(sb.ToString());
            }
        }

        public static void ReadBackward(long offsetStart, long offsetEnd)
        {
            result.Clear();

            fs.Position = offsetStart;
            int chunkSize = 16;
            byte[] bytes = new byte[chunkSize];
            int bytesToRead;
            long temp;

            while (fs.Position >= offsetEnd && fs.Position > 0)
            {
                if (fs.Position - chunkSize < 0)
                    bytesToRead = (int)fs.Position;
                else
                    bytesToRead = chunkSize;

                fs.Position -= bytesToRead;
                temp = fs.Position;
                fs.Read(bytes, 0, bytesToRead);
                fs.Position = temp;

                sb.Clear();
                sb.Append($"{fs.Position.ToString("X8")}: ");
                for (int i = 0; i < bytesToRead; i++)
                    sb.Append($"{bytes[i].ToString("X2")} ");

                for (int i = bytesToRead; i < bytes.Length; i++)
                    sb.Append("   ");

                sb.Append("| ");

                for (int i = 0; i < bytesToRead; i++)
                {
                    if (bytes[i] >= 33 && bytes[i] <= 126)
                        sb.Append($"{(char)bytes[i]} ");
                    else
                        sb.Append($". ");
                }

                for (int i = bytesToRead; i < bytes.Length; i++)
                    sb.Append("  ");

                result.Add(sb.ToString());
            }
        }
    }
}
