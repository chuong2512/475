/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace SgLib.Editor
{
    public static class FileIO
    {
        /// <summary>
        /// Replaces / in file path to be the os specific separator.
        /// </summary>
        /// <returns>The path.</returns>
        /// <param name="path">Path with correct separators.</param>
        public static string SlashesToPlatformSeparator(string path)
        {
            return path.Replace("/", Path.DirectorySeparatorChar.ToString());
        }

        /// <summary>
        /// Checks if folder exists.
        /// </summary>
        /// <returns><c>true</c>, if exists, <c>false</c> otherwise.</returns>
        /// <param name="path">Path - the slashes will be corrected.</param>
        public static bool FolderExists(string path)
        {
            path = SlashesToPlatformSeparator(path);
            return Directory.Exists(path);
        }

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="path">Path - the slashes will be corrected.</param>
        public static void CreateFolder(string path)
        {
            path = SlashesToPlatformSeparator(path);
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Creates the folder if it doesn't exist.
        /// </summary>
        /// <param name="path">Path - the slashes will be corrected.</param>
        public static void EnsureFolderExists(string path)
        {
            path = SlashesToPlatformSeparator(path);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Check if file exists.
        /// </summary>
        /// <returns><c>true</c>, if exists was filed, <c>false</c> otherwise.</returns>
        /// <param name="path">File path.</param>
        public static bool FileExists(string path)
        {
            path = SlashesToPlatformSeparator(path);
            return File.Exists(path);
        }

        /// <summary>
        /// Writes the file.
        /// </summary>
        /// <param name="path">File path - the slashes will be corrected.</param>
        /// <param name="body">Body of the file to write.</param>
        public static void WriteFile(string path, string body)
        {
            path = SlashesToPlatformSeparator(path);

            using (var wr = new StreamWriter(path, false))
            {
                wr.Write(body);
            }
        }

        /// <summary>
        /// Writes all lines.
        /// </summary>
        /// <param name="path">Path.</param>
        /// <param name="lines">Lines.</param>
        public static void WriteAllLines(string path, string[] lines)
        {
            path = SlashesToPlatformSeparator(path);
            File.WriteAllLines(path, lines);
        }

        /// <summary>
        /// Reads the file at the specified path.
        /// </summary>
        /// <returns>The file contents -  the slashes will be corrected.</returns>
        /// <param name="path">File path.</param>
        public static string ReadFile(string path)
        {
            path = SlashesToPlatformSeparator(path);

            if (!File.Exists(path))
            {
                return null;
            }

            StreamReader sr = new StreamReader(path);
            string body = sr.ReadToEnd();
            sr.Close();
            return body;
        }

        /// <summary>
        /// Reads all lines of the given text file.
        /// </summary>
        /// <returns>The all lines.</returns>
        /// <param name="path">Path.</param>
        public static string[] ReadAllLines(string path)
        {
            path = SlashesToPlatformSeparator(path);

            if (!File.Exists(path))
            {
                return new string[0];
            }

            return File.ReadAllLines(path);
        }

        /// <summary>
        /// Deletes the file at path if it exists.
        /// </summary>
        /// <param name="path">File path.</param>
        public static void Deletefile(string path)
        {
            path = SlashesToPlatformSeparator(path);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

