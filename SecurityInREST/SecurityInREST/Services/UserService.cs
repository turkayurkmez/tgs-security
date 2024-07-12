namespace SecurityInREST.Services
{

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class UserService
    {
        private readonly List<User> _users = new List<User>()
        {
            new User { Id = 1, UserName="test1", Email="a@b.com", Name="test", Password="123", Role="Admin" },
            new User { Id = 2, UserName="test2", Email="a@b.com", Name="test", Password="123", Role="Editor" },
            new User { Id = 3, UserName="test3", Email="a@b.com", Name="test", Password="123", Role="Client" },

        };

        public User? Validate(string userName, string password) => _users.SingleOrDefault(u => u.UserName == userName && u.Password == password);
    }
}
