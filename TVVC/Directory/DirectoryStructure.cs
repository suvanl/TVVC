namespace TVVC
{
    /// <summary>
    /// Helper class to query information about directories
    /// </summary>
    public static class DirectoryStructure
    {
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

            // Return the file name (after the final backslash)
            return path.Substring(lastIndex + 1);
        }

        #endregion
    }
}
