namespace AccessAdmin.Business.Interfaces
{
    public interface ILoginRepository
    {
        int AuthenticateUser(string email, string password);
        bool IsUserAdmin(string email);
    }
}
