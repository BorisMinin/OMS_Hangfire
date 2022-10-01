using Microsoft.EntityFrameworkCore;

namespace OMS.Data.Access.Maps
{
    public interface IMap
    {
        void Visit(ModelBuilder builder);
    }
}