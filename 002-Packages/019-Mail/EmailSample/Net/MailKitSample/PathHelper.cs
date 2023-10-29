namespace MailKitSample
{
    public static class PathHelper
    {
        /// <summary>
        /// Get Absolute Path
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToAbsolutePath(string path, string rootPath = "")
        {
            // Releation File Path
            if (path.StartsWith("~/") || path.StartsWith("~\\"))
            {
                path = path.Replace("~/", string.Empty).Replace("~\\", string.Empty);
            }

            // Get Root Path
            if (string.IsNullOrWhiteSpace(rootPath))
            {
                rootPath = AppDomain.CurrentDomain.BaseDirectory;
            }

            // Combine Base Path
            path = CombinePath(rootPath, path);

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// Combine Path
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static string CombinePath(string path1, string path2)
        {
            // Combine Path
            string path = (Path.Combine(path1, path2));

            // Directory Separator
            path = FormatDirectorySeparatorChar(path);

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// Combine Path
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="path3"></param>
        /// <returns></returns>
        public static string CombinePath(string path1, string path2, string path3)
        {
            // Combine Path
            string path = (Path.Combine(path1, path2, path3));

            // Directory Separator
            path = FormatDirectorySeparatorChar(path);

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// Combine Path
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="path3"></param>
        /// <param name="path4"></param>
        /// <returns></returns>
        public static string CombinePath(string path1, string path2, string path3, string path4)
        {
            // Combine Path
            string path = (Path.Combine(path1, path2, path3, path4));

            // Directory Separator
            path = FormatDirectorySeparatorChar(path);

            return Path.GetFullPath(path);
        }


        /// <summary>
        /// Format Directory Separator
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string FormatDirectorySeparatorChar(string path)
        {
            // Directory Separator
            string slash = Path.DirectorySeparatorChar.ToString();
            path = path.Replace("\\", slash)
                       .Replace("/", slash);

            return Path.GetFullPath(path);
        }
    }
}
