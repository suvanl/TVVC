using System.Collections.ObjectModel;
using System.Linq;

namespace TVVC
{
    /// <summary>
    /// The view model for the application's main directory view
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// A list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        #endregion

        #region Constructor

        public DirectoryStructureViewModel()
        {
            var children = DirectoryStructure.GetLogicalDrives();

            // Creates view models from the given data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
        }

        #endregion
    }
}
