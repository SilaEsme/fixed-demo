using FixedDemo.Domain.Primitives;

namespace FixedDemo.Domain.Entities
{
    public sealed class Asset : AggregateRoot
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
    }
}
