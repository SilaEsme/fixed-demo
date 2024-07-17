using System.ComponentModel.DataAnnotations;

namespace FixedDemo.Domain.Primitives
{
    public class BaseObject
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
