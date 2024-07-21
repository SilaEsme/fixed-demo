using AutoMapper;

namespace FixedDemo.Application.Asset.Mapping
{
    internal class AssetMappingProfile : Profile
    {
        public AssetMappingProfile()
        {
            CreateMap<Domain.Entities.Asset, Shared.Dtos.Asset.AssetDto>();
            CreateMap<Commands.CreateAssetCommand, Domain.Entities.Asset>();
            CreateMap<Commands.UpdateAssetCommand, Domain.Entities.Asset>();
        }
    }
}
