namespace WorkBoard.Domain;

public sealed class Role : IEquatable<Role>
{
    private readonly List<User> _users = new();
    public string Name { get; }
    public IReadOnlyCollection<User> Users => _users;

    public Role(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty");

        Name = name.Trim();
    }

    public bool Equals(Role? other) => other != null && Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
    public override bool Equals(object? obj) => Equals(obj as Role);
    public override int GetHashCode() => Name.GetHashCode(StringComparison.OrdinalIgnoreCase);
}