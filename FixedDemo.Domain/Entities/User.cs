using FixedDemo.Domain.Primitives;

namespace FixedDemo.Domain.Entities
{
    public class User : AggregateRoot
    {
        public string? Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public required byte[] PasswordHash { get; set; }
    }
}
