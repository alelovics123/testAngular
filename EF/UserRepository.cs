using Microsoft.EntityFrameworkCore;
using repos.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace repos.EF
{
    public class UserAlreadyExistsExption : Exception { }
    public class HasReferenceForDeleteExption : Exception { }
    public class UserRepository : IUserRepository, IDisposable
    {
        private UserContext Context { get; set; }
        private bool hasAddDefaultsRun { get; set; } = false;
        public UserRepository()
        {
            Context = new UserContext();
        }
        public void Add(UserViewModel user)
        {
            if (Context.Users.Any(n => n.Name == user.Name && n.DateOfBirth == user.DateOfBirth && n.PlaceOfBirth == user.PlaceOfBirth))
            {
                throw new UserAlreadyExistsExption();
            }
            else
            {
                Context.Add(
                    new User{ Name = user.Name, DateOfBirth = user.DateOfBirth, RecommenderId = user.RecommenderId, PlaceOfBirth = user.PlaceOfBirth });
                Context.SaveChanges();
            }
        }

        public void AddDefaults()
        {
            if (!hasAddDefaultsRun)
            {
                var pastDate = DateTime.Now.AddYears(-20);
                for (int i = 0; i < 100; i++)
                {
                    Context.Add(
                        new User { Name = "" + i, DateOfBirth = pastDate.AddDays(i), RecommenderId = i > 0 ? i - 1 : null, PlaceOfBirth = "city " + i });
                }
                Context.SaveChanges();
                hasAddDefaultsRun = true;
            }
        }

        public void Delete(int userId, bool? hasConfirmed)
        {
            var toBeDeleted = Context.Users.Find(userId);
            if (toBeDeleted != null)
            {
                if (Context.Users.Any(n=>n.RecommenderId==toBeDeleted.Id)&& !(hasConfirmed??false)) 
                {
                    throw new HasReferenceForDeleteExption();
                }
                var users=Context.Users.Where(n=>n.RecommenderId==toBeDeleted.Id);
                foreach (var user in users)
                {
                    user.RecommenderId = null;
                }
                if (users.Count() > 0) { Context.SaveChanges(); }
                Context.Users.Remove(toBeDeleted);
                Context.SaveChanges();
            }
            else { throw new Exception("unkown id"); }
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public List<UserViewModel> GetAll(Func<User, bool>? filterFunction)
        {
            var retVal = Context.Users.AsNoTracking().AsEnumerable();
            if (filterFunction != null) { retVal = retVal.Where(filterFunction); }
            return retVal.Select(n => new UserViewModel
            {
                Id = n.Id,
                DateOfBirth = n.DateOfBirth,
                Name = n.Name,
                PlaceOfBirth = n.PlaceOfBirth,
                RecommenderId = n.RecommenderId
            }).ToList();
        }

        public void Modify(UserViewModel user)
        {
            var toBeModified = Context.Users.Find(user.Id);
            if (toBeModified != null)
            {
                toBeModified.Name = user.Name;
                toBeModified.DateOfBirth = user.DateOfBirth;
                if (toBeModified.RecommenderId != user.RecommenderId)
                {
                    toBeModified.RecommenderId = user.RecommenderId;
                }
                toBeModified.PlaceOfBirth = user.PlaceOfBirth;
                Context.SaveChanges();
            }
            else { throw new Exception("unkown id"); }
        }
    }
}
