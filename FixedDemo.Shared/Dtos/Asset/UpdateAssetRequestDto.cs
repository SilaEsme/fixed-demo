using System.ComponentModel.DataAnnotations;

namespace FixedDemo.Shared.Dtos.Asset
{
    public class UpdateAssetRequestDto
    {
        public Guid Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
    }
}
