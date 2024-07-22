using FixedDemo.Shared.Dtos.Asset;
using FixedDemo.Shared.Wrapper;
using System.Net.Http.Json;

namespace FixedDemo.App.Services.Data
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _httpClient;
        public InventoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<AssetDto> Inventory { get; set; } = new();
        public string Message { get; set; }

        public event Action OnChange;

        public async Task AddAssetAsync(CreateAssetRequestDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Inventory/add", request);
            OnChange?.Invoke();
        }

        public async Task DeleteAssetAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Inventory/{id}");
            OnChange?.Invoke();
        }

        public async Task GetAssetAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<ApiResult<AssetDto>>($"api/Inventory/{id}");
            if(result != null && result.IsSuccess && result.Data != null)
            {
                Inventory = [result.Data];   
            }
            OnChange?.Invoke();
        }

        public async Task GetInventoryItemsAsync(GetInventoryRequestDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Inventory", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResult<IEnumerable<AssetDto>>>();
            if (result != null && result.IsSuccess && result.Data != null)
            {
                Inventory = result.Data.ToList();
            }
        }

        public async Task UpdateAssetAsync(UpdateAssetRequestDto request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Inventory", request);
            OnChange?.Invoke();
        }
    }
}
