using System.Collections.ObjectModel;
using AccessAdmin.Client.BusinessLogic;
using AccessAdmin.Client.Model;
using GalaSoft.MvvmLight;

namespace AccessAdmin.Client.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private SystemHelper sysHelper;
        private RoleHelper roleHelper;
        private Systems selectedSystem;
        private Roles selectedRole;
        private ObservableCollection<Systems> sysList;
        private ObservableCollection<Roles> roleList;

        public UserViewModel()
        {
            this.sysHelper = new SystemHelper();
            this.roleHelper = new RoleHelper();

            this.SystemsList = this.sysHelper.GetSystems().Result;
            this.RolesList = this.roleHelper.GetAllRoles().Result;
        }

        public ObservableCollection<Systems> SystemsList
        {
            get => this.sysList;
            set
            {
                this.sysList = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<Roles> RolesList
        {
            get => this.roleList;
            set
            {
                this.roleList = value;
                this.RaisePropertyChanged();
            }
        }

        public Systems SelectedSystem
        {
            get => this.selectedSystem;
            set
            {
                this.selectedSystem = value;
                this.RaisePropertyChanged();
            }
        }

        public Roles SelectedRole
        {
            get => this.selectedRole;
            set
            {
                this.selectedRole = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
