namespace WorkBoard.Domain;

public sealed class Role : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    private Role() { }

    public Role(string name)
    {
        Name = name;
    }
}

public sealed class UserRole
{
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;
    public int RoleId { get; private set; }
    public Role Role { get; private set; } = null!;

    private UserRole() { }

    public UserRole(int userId, User user, int roleId, Role role)
    {
        UserId = userId;
        User = user;
        RoleId = roleId;
        Role = role;
    }
}
