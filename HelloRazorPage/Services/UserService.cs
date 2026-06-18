using Microsoft.EntityFrameworkCore;
using HelloRazorPage.DbModels;

namespace HelloRazorPage.Services
{
    public class UserService
    {
        private readonly ApplicationContext _db;

        public UserService(ApplicationContext db)
        {
            _db = db;
        }

        public List<User> GetAll() => _db.Users.ToList();

        public User? GetById(int id) => _db.Users.FirstOrDefault(x => x.Id == id);

        public User Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public bool Update(User user)
        {
            User found = GetById(user.Id);
            if (found == null) return false;

            found.Login = user.Login;
            found.Password = user.Password;
            _db.SaveChanges();
            return true;
        }

        public bool DeleteById(int id)
        {
            User found = GetById(id);
            if (found == null) return false;

            _db.Users.Remove(found);
            _db.SaveChanges();
            return true;
        }
    }
}