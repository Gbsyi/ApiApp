namespace ApiApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role[] UserRoles { get; set; }
        public User() { }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
            UserRoles = new Role[0];
        }
        public User(string name, string password, Role[] roles)
        {
            Name = name;
            Password = password;
            UserRoles = roles;
        }


    }
}
