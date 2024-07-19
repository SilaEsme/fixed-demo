using FixedDemo.Domain.Primitives;

namespace FixedDemo.Domain.Entities
{
    public class User : AggregateRoot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
