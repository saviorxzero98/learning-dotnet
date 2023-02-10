using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileHash
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "FileHash.dll";
            string filePath = $"Files\\{fileName}";


            string md5Hash = GetFileMd5(filePath);
            string sha1Hash = GetFileSha1(filePath);


            Console.WriteLine($"File：{fileName}");
            Console.WriteLine($"MD5：{md5Hash}");
            Console.WriteLine($"SHA1：{sha1Hash}");
        }

        static string GetFileMd5(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    byte[] checksum = md5.ComputeHash(stream);
                    string bits = BitConverter.ToString(checksum);
                    return bits.Replace("-", String.Empty).ToLower();
                }
            }
        }

        static string GetFileSha1(string fileName)
        {
            using (var sha1 = SHA1.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    byte[] checksum = sha1.ComputeHash(stream);
                    string bits = BitConverter.ToString(checksum);
                    return bits.Replace("-", String.Empty).ToLower();
                }
            }
        }
    }
}
