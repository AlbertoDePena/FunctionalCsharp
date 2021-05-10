using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionalCsharp.Tests
{
    public interface IUserStore
    {
        Task<Either<User, string>> FindUser(int id);

        Task<IReadOnlyCollection<User>> GetUsers();
    }

    public class UserStore : IUserStore
    {
        private List<User> users;

        public UserStore()
        {
            this.users = new List<User>();
        }

        public Task<Either<User, string>> FindUser(int id)
        {
            var user = users.Where(x => x.Id == id).FirstOrDefault();

            Either<User, string> either;

            if (user is null)
            {
                either = Either<User, string>.Error($"User with ID {id} not found");
            }
            else
            {
                either = Either<User, string>.Success(user);
            }

            return Task.FromResult(either);
        }

        public Task<IReadOnlyCollection<User>> GetUsers()
        {
            return Task.FromResult<IReadOnlyCollection<User>>(users.ToArray());
        }
    }

    public class User
    {
        public int Id { get;  }
        public string Name { get; }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}