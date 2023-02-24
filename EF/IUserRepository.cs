using repos.Models;

namespace repos.EF
{
    public interface IUserRepository
    {
        void Add(UserViewModel user);
        List<UserViewModel> GetAll(Func<User, bool>? filterFunction);
        void Modify(UserViewModel user);
        void Delete(int userId,bool? hasConfirmed);
        void AddDefaults();
    }
}
