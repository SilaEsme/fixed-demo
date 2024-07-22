using FixedDemo.Shared.Dtos.Asset;

namespace FixedDemo.App.Services.Data
{
    public interface IInventoryService
    {
        event Action OnChange;
        List<AssetDto> Inventory { get; set; }
        string Message { get; set; }
        public Task GetInventoryItemsAsync(GetInventoryRequestDto request);
        public Task GetAssetAsync(Guid id);
        public Task AddAssetAsync(CreateAssetRequestDto request);
        public Task UpdateAssetAsync(UpdateAssetRequestDto request);
        public Task DeleteAssetAsync(Guid id);
    }
}
