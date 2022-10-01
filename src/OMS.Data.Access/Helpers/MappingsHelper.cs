using OMS.Data.Access.Maps;
using OMS.Data.Access.Maps.EntityMaps;
using System.Reflection;

namespace OMS.Data.Access.Helpers
{
    /// <summary>
    /// Сопоставление выполняется через класс MappingsHelper:
    /// Для создания нового объекта, реализующего IMap, зайти в папку EntityMaps,
    /// результатом будет то, что новый объект появится в коллекции 
    /// </summary>
    public static class MappingsHelper
    {
        /// <summary>
        /// возвращает массив объектов типа IMap
        /// </summary>
        /// <returns>IMap[]</returns>
        public static IEnumerable<IMap> GetMainMappings()
        {
            // assemblyTypes содержит коллекцию типов объявленных в сборке (Expenses.Data.Access), в которой объявлен тип UserMap
            var assemblyTypes = typeof(CategoryMap).GetTypeInfo().Assembly.DefinedTypes;

            // mappings содержит коллекцию типов реализовавших IMap и находящихся в пространстве имен Expenses.Data.Access.Maps.EntityMaps
            var mappings = assemblyTypes
                .Where(t => t.Namespace != null &&
                t.Namespace.Contains(typeof(CategoryMap).Namespace))
                .Where(t => typeof(IMap).GetTypeInfo().IsAssignableFrom(t));

            mappings = mappings.Where(x => !x.IsAbstract);

            var result = mappings.Select(m => (IMap)Activator.CreateInstance(m.AsType())).ToArray();
            
            return result;
        }
    }
}