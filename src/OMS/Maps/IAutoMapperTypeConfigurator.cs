using AutoMapper;

namespace OMS.Maps
{
    //todo: написать комментарий
    /// <summary>
    /// 
    /// </summary>
    public interface IAutoMapperTypeConfigurator
    {
        void Configure(IMapperConfigurationExpression configuration);
    }
}