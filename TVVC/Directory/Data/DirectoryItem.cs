namespace TVVC.Directory.Data
{
    /// <summary>
    /// Information about directory items, such as drives, files and folders
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The absolute path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of the directory item
        /// </summary>
        public string Name { get { return DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}
