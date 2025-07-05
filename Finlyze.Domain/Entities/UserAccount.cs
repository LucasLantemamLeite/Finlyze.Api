using Finlyze.Domain.ValueObject.UserAccountObject;

namespace Finlyze.Domain.Entity;

public class UserAccount : Entity
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public BirthDate BirthDate { get; private set; }
    public CreateAt CreateAt { get; private set; }
    public Active Active { get; private set; }
    public Role Role { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new();

    public void ChangeName(string? name)
    {
        if (name is not null && !string.IsNullOrWhiteSpace(name))
            Name = new Name(name);
    }

    public void ChangeEmail(string? email)
    {
        if (email is not null && !string.IsNullOrWhiteSpace(email))
            Email = new Email(email);
    }

    public void ChangePassword(string? password)
    {
        if (password is not null && !string.IsNullOrWhiteSpace(password))
            Password = new Password(password);
    }

    public void ChangePhoneNumber(string? phone)
    {
        if (phone is not null && !string.IsNullOrWhiteSpace(phone))
            PhoneNumber = new PhoneNumber(phone);
    }

    public UserAccount(string name, string email, string password, string phone, DateOnly birth, bool active = true, int role = 1)
    {
        Name = new Name(name);
        Email = new Email(email);
        Password = new Password(password);
        PhoneNumber = new PhoneNumber(phone);
        BirthDate = new BirthDate(birth);
        CreateAt = new CreateAt();
        Active = new Active(active);
        Role = new Role(role);
    }

    public UserAccount(Guid id, string name, string email, string password, string phone, DateOnly birth, DateTime create, bool active, int role) : base(id)
    {
        Name = new Name(name);
        Email = new Email(email);
        Password = new Password(password);
        PhoneNumber = new PhoneNumber(phone);
        BirthDate = new BirthDate(birth);
        CreateAt = new CreateAt(create);
        Active = new Active(active);
        Role = new Role(role);
    }

    private UserAccount() { }

}