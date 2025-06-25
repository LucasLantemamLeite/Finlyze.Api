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

    public UserAccount(string name, string email, string password, string phone, DateOnly birth, bool active = true, int role = 1)
    {
        Name = new Name(name);
        Email = new Email(email);
        Password = new Password(password);
        PhoneNumber = new PhoneNumber(phone);
        BirthDate = new BirthDate(birth);
        Active = new Active(active);
        Role = new Role(role);
    }
}