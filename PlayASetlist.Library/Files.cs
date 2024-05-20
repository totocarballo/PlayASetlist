using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PlayASetlist.Library
{
    public static class Files
    {
        private static readonly object fileLock = new object();

        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static bool DirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        public static bool Exists(string path)
        {
            return File.Exists(path);
        }

        public static FileStream FileStreamCreate(string path)
        {
            return new FileStream(path, FileMode.Create);
        }

        public static string GetDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            return Path.GetDirectoryName(path);
        }

        public static string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public static string[] GetFilesFromDirectory(string path, string fileName)
        {
            if (!DirectoryExist(path))
            {
                return new string[] { };
            }

            return Directory.GetFiles(path, fileName, SearchOption.AllDirectories);
        }

        public static string HashMD5(string path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
            }
        }

        public static string HashSHA1(string path)
        {
            using (var sha1 = SHA1.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    var hash = sha1.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
            }
        }

        public static string PathCombine(string path, string file)
        {
            return Path.Combine(path, file);
        }

        public static string[] ReadAllLines(string path)
        {
            if (!Exists(path))
            {
                return new string[] { };
            }

            return File.ReadAllLines(path);
        }

        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public static List<string> ReadLines(string path)
        {
            return File.ReadLines(path).ToList();
        }

        public static void WriteAllLines(string path, List<string> lines)
        {
            _ = Task.Run(() =>
            {
                lock (fileLock)
                {
                    File.WriteAllLines(path, lines);
                }
            });
        }

        public static void WriteAllText(string path, string text)
        {
            _ = Task.Run(() =>
            {
                lock (fileLock)
                {
                    File.WriteAllText(path, text);
                }
            });
        }
    }
}