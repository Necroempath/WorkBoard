using System.Data;

namespace WorkBoard.Domain;

public sealed class User : BaseEntity
{
    private readonly HashSet<Role> _roles = new();
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public IReadOnlySet<Role> Roles => _roles;

    private User() { }

    public User(string name, string email, string passwordHash)
    {
        AssignRole(new Role("User"));
        SetName(name);
        SetEmail(email);
        SetPasswordHash(passwordHash);
    }

    public void Rename(string name)
    {
        SetName(name);
    }

    public void UpdateEmail(string email)
    {
        SetEmail(email);
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        SetPasswordHash(passwordHash);
    }

    public void AssignRole(Role role)
    {
        _roles.Add(role);
    }

    public void RemoveRole(Role role)
    {
        _roles.Remove(role);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("User name name cannot be empty");

        if (name.Length < 2)
            throw new ArgumentException("User name must be at least 2 characters long");

        if (name.Length > 100)
            throw new ArgumentException("User name cannot exceed 100 characters");

        Name = name;

    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        email = email.Trim().ToLowerInvariant();

        if (!email.Contains('@'))
            throw new ArgumentException("Invalid email format");

        Email = email;
    }

    private void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty");

        PasswordHash = passwordHash;
    }
}