using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TVVC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region On Loaded

        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Gets every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // Creates a new item for each one
                var item = new TreeViewItem()
                {
                    // Sets the header
                    Header = drive,
                    // Sets the full path
                    Tag = drive
                };

                // Adds a dummy item
                item.Items.Add(null);

                // Listens out for the item being expanded
                item.Expanded += Folder_Expanded;

                // Adds it to the main TreeView
                FolderView.Items.Add(item);
            }
        }

        #endregion

        #region Folder Expanded

        /// <summary>
        /// When a folder is expanded, find the sub-folders/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks
            var item = (TreeViewItem)sender;
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            item.Items.Clear();

            var fullPath = (string)item.Tag;

            #endregion

            #region Get Folders

            var directories = new List<string>();

            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath
                };

                subItem.Items.Add(null);
                subItem.Expanded += Folder_Expanded;

                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files

            var files = new List<string>();

            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            files.ForEach(filePath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filePath),
                    Tag = filePath
                };

                item.Items.Add(subItem);
            });

            #endregion
        }


        #endregion

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