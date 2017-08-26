using GalaSoft.MvvmLight;

namespace AccessAdmin.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public AdminViewModel AdminViewModel { get; set; }
        public UserViewModel UserViewModel { get; set; }

        private int selectedTabIndex;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.AdminViewModel = new AdminViewModel();
            this.UserViewModel = new UserViewModel();
        }

        public int SelectedTabIndex
        {
            get => this.selectedTabIndex;

            set
            {
                if (value == this.selectedTabIndex)
                {
                    return;
                }

                this.selectedTabIndex = value;
                this.RaisePropertyChanged();
            }
        }
    }
}