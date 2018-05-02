using PropertyChanged;
using System.ComponentModel;

namespace TVVC
{
    /// <summary>
    /// A base view model that fires Property Changed events as required
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired whenever any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
