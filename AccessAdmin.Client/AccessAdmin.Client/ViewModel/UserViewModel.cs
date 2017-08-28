using System.Collections.ObjectModel;
using AccessAdmin.Client.BusinessLogic;
using AccessAdmin.Client.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

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
        private string reasonBox;

        public RelayCommand RequestCommand { get; set; }

        public UserViewModel()
        {
            this.sysHelper = new SystemHelper();
            this.roleHelper = new RoleHelper();

            this.SystemsList = this.sysHelper.GetSystems().Result;
            this.RolesList = this.roleHelper.GetAllRoles().Result;

            this.RequestCommand = new RelayCommand(RequestAccess);
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

        public string ReasonBox
        {
            get => this.reasonBox;
            set
            {
                this.reasonBox = value;
                this.RaisePropertyChanged();
            }
        }

        private void RequestAccess()
        {
            var helper = new UserHelper();
            var id = helper.GetUserByEmail(AdminHelper.UserMail).Result.UserId;

            var request = new Requests
            {
                RequestReason = ReasonBox,
                UserId = new User {  UserId = id },
                SystemId = new Systems { SystemId = this.SelectedSystem.SystemId },
                RoleId = new Roles { RoleId = this.SelectedRole.RoleId }
            };

            helper.MakeRequest(request);
        }
    }
}
