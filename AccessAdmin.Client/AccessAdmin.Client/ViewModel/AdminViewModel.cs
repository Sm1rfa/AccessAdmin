using System.Collections.ObjectModel;
using System.Windows;
using AccessAdmin.Client.BusinessLogic;
using AccessAdmin.Client.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AccessAdmin.Client.ViewModel
{
    public class AdminViewModel : ViewModelBase
    {
        #region Private fields
        private string systemName;
        private string roleName;
        private Systems selectedSystem;
        private Roles selectedRole;
        private Roles selectedComboRole;
        private Requests selectedRequest;
        private RequestStates selectedState;
        private bool isVisible;
        private ObservableCollection<Systems> systemList;
        private ObservableCollection<Roles> roleList;
        private ObservableCollection<Roles> fullRoleList;
        private ObservableCollection<Requests> requestsList;
        private ObservableCollection<RequestStates> stateList;
        private SystemHelper systemHelper;
        private RoleHelper roleHelper;
        private UserHelper userHelper;
        #endregion

        #region Public commands
        public RelayCommand CreateSystemCommand { get; set; }
        public RelayCommand UpdateSystemCommand { get; set; }
        public RelayCommand DeleteSystemCommand { get; set; }
        public RelayCommand SelectedSystemChangedCommand { get; set; }
        public RelayCommand DeleteRoleCommand { get; set; }
        public RelayCommand CreateRoleCommand { get; set; }
        public RelayCommand AssignToSystemCommand { get; set; }
        public RelayCommand SolveRequestCommand { get; set; }
        #endregion

        public AdminViewModel()
        {
            this.systemHelper = new SystemHelper();
            this.roleHelper = new RoleHelper();
            this.userHelper = new UserHelper();

            this.SystemsList = this.systemHelper.GetSystems().Result;
            this.FullRolesList = this.roleHelper.GetAllRoles().Result;
            this.RequestsList = this.userHelper.GetRequests().Result;

            this.RequestStatesList = new ObservableCollection<RequestStates>
            {
                new RequestStates{ RequestId = 2, RequestName = "Given", RequestState = "20"},
                new RequestStates{ RequestId = 3, RequestName = "Declined", RequestState = "30"}
            };

            this.CreateSystemCommand = new RelayCommand(CreateSystem);
            this.UpdateSystemCommand = new RelayCommand(UpdateSystem);
            this.DeleteSystemCommand = new RelayCommand(DeleteSystem);
            this.SelectedSystemChangedCommand = new RelayCommand(ListRolesPerSystem);
            this.DeleteRoleCommand = new RelayCommand(DeleteRolePerSystem);
            this.CreateRoleCommand = new RelayCommand(CreateRole);
            this.AssignToSystemCommand = new RelayCommand(AssignRoleToSystem);
            this.SolveRequestCommand = new RelayCommand(SolveRequest);
        }

        #region Public properties

        public ObservableCollection<RequestStates> RequestStatesList
        {
            get => this.stateList;
            set
            {
                this.stateList = value;
                this.RaisePropertyChanged();
            }
        }

        public RequestStates SelectedState
        {
            get => this.selectedState;
            set
            {
                this.selectedState = value;
                this.RaisePropertyChanged();
            }
        }

        public string SystemName
        {
            get => this.systemName;
            set
            {
                this.systemName = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<Systems> SystemsList
        {
            get => systemList;
            set
            {
                this.systemList = value;
                this.RaisePropertyChanged();
            }
        }

        public Systems SystemSelected
        {
            get => this.selectedSystem;
            set
            {
                this.selectedSystem = value;
                this.RaisePropertyChanged();
            }
        }

        public string RoleName
        {
            get => this.roleName;
            set
            {
                this.roleName = value;
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

        public ObservableCollection<Roles> RolesList
        {
            get => this.roleList;
            set
            {
                this.roleList = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsVisible
        {
            get => this.isVisible;
            set
            {
                this.isVisible = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<Roles> FullRolesList
        {
            get => this.fullRoleList;
            set
            {
                this.fullRoleList = value;
                this.RaisePropertyChanged();
            }
        }

        public Roles SelectedComboRole
        {
            get => this.selectedComboRole;
            set
            {
                this.selectedComboRole = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<Requests> RequestsList
        {
            get => this.requestsList;
            set
            {
                this.requestsList = value;
                this.RaisePropertyChanged();
            }
        }

        public Requests SelectedRequest
        {
            get => this.selectedRequest;
            set
            {
                this.selectedRequest = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion


        #region Private methods
        private void CreateSystem()
        {
            var sys = new Systems
            {
                SystemName = this.SystemName
            };

            this.systemHelper.CreateSystem(sys);
            MessageBox.Show(
                $"System {this.SystemName} has been created", "Info");
            this.SystemName = string.Empty;
        }

        private void UpdateSystem()
        {
            this.systemHelper.UpdateSystem(this.SystemSelected.SystemId, this.SystemSelected);
            MessageBox.Show(
                $"System {this.SystemSelected.SystemName} has been updated", "Info");
        }

        private void DeleteSystem()
        {
            this.systemHelper.DeleteSystem(this.SystemSelected.SystemId);
            this.SystemsList.Remove(this.SystemSelected);
            MessageBox.Show($"{this.SystemSelected.SystemName} has been deleted", "Info");
        }

        private void ListRolesPerSystem()
        {
            this.RolesList = this.roleHelper.GetRolesPerSystem(this.SystemSelected.SystemId).Result;
            //this.IsVisible = true;
        }

        private void DeleteRolePerSystem()
        {
            this.roleHelper.DeleteRolePerSystem(this.SystemSelected.SystemId, this.SelectedRole.RoleId);
            MessageBox.Show(
                $"Role {this.SelectedComboRole.RoleName} has been deleted for {this.SystemSelected.SystemName}", "Info");
        }

        private void CreateRole()
        {
            var role = new Roles
            {
                RoleName = this.RoleName
            };
            this.roleHelper.CreateRole(role);
            MessageBox.Show(
                $"Role {this.RoleName} has been created", "Info");
            this.FullRolesList = this.roleHelper.GetAllRoles().Result;
            this.RoleName = string.Empty;
        }

        private void AssignRoleToSystem()
        {
            if (this.SystemSelected == null || this.SelectedComboRole == null)
            {
                MessageBox.Show("You need to select system and role!", "Info");
                return;
            }

            this.roleHelper.AssignToSystem(this.SystemSelected.SystemId, this.SelectedComboRole.RoleId);
            MessageBox.Show(
                $"Role {this.SelectedComboRole.RoleName} has been assigned to {this.SystemSelected.SystemName}", "Info");
        }

        private void SolveRequest()
        {
            this.SelectedRequest.RequestStateId.RequestId = this.SelectedState.RequestId;
            this.userHelper.SolveRequest(this.SelectedRequest);
            this.RequestsList.Remove(this.SelectedRequest);
        }
        #endregion
    }
}
