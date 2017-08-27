using AccessAdmin.Domain.Model;

namespace AccessAdmin.Business.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
    }
}
