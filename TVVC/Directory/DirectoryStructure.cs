using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TVVC
{
    /// <summary>
    /// Helper class to query information about directories
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Gets all logical drives on the machine
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // Gets every logical drive on the machine
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        /// <summary>
        /// Gets the top-level content of the directory
        /// </summary>
        /// <param name="fullPath">The full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            // Creates an empty list
            var items = new List<DirectoryItem>();

            #region Get Folders

            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch { }

            #endregion

            #region Get Files

            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }

            #endregion

            return items;
        }

        #region Helpers

        /// <summary>
        /// Finds the file or folder name from a full path
        /// </summary>
        /// <param name="path">The full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            // Returns empty if no path exists
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // Converts all forward slashes to backslashes
            var normalizedPath = path.Replace('/', '\\');

            // Finds the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            // Returns the path itself if no backslash is found
            if (lastIndex <= 0)
                return path;

            // Returns the file name (after the final backslash)
            return path.Substring(lastIndex + 1);
        }

        #endregion
    }
}
