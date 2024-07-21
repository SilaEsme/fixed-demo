namespace FixedDemo.Shared.Dtos.Asset
{
    /// <summary>
    /// General Asset Dto
    /// </summary>
    public class AssetDto
    {
        public Guid Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
    }
}
