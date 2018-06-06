using Shop.Services.Mappings.Profiles;
using System;
using System.Collections.Generic;

namespace Shop.Services.Mappings
{
    /// <summary>
    /// Конфигурация маппинга объектов.
    /// </summary>
    public class AutoMapperConfiguration
    {
        public static IEnumerable<Type> GetProfiles()
        {
            return new[]
            {
                typeof(EntityToResourceModelMappingProfile)
            };
        }
    }
}