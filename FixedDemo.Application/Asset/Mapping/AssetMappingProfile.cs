using AutoMapper;

namespace FixedDemo.Application.Asset.Mapping
{
    internal class AssetMappingProfile : Profile
    {
        public AssetMappingProfile()
        {
            CreateMap<Domain.Entities.Asset, Core.Dtos.Asset.AssetDto>();
        }
    }
}
