using FixedDemo.Domain.Primitives;
using System.ComponentModel.DataAnnotations;

namespace FixedDemo.Domain.Entities
{
    public sealed class Asset : AggregateRoot
    {
        [Key]
        public Guid Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
    }
}
