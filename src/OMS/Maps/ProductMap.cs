using AutoMapper;
using OMS.API.Models.Dtos.ProductDto;
using OMS.Data.Model.Entities;

namespace OMS.Maps
{
    public class ProductMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductDtoGet>();
        }
    }
}