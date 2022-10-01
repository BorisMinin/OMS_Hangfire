using AutoMapper;
using OMS.API.Models.Dtos.CategoryDto;
using OMS.Data.Model.Entities;

namespace OMS.Maps
{
    /// <summary>
    /// Производится сопоставление Entity на DTO
    /// </summary>
    public class CategoryMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryDtoGet>();
        }
    }
}