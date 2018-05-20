using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetCoreStack.Data;
using NetCoreStack.Data.Interfaces;
using NetCoreStack.Data.Types;
using System;

namespace NetCoreStack.Localization
{
    public static class InstanceFactoryExtensions
    {
        public static ISqlUnitOfWork CreateUnitOfWork(this IServiceProvider serviceProvider)
        {
            IOptions<DbSettings> settings = serviceProvider.GetService<IOptions<DbSettings>>();
            return new SqlUnitOfWork(new LocalizationDBContext(new DataContextConfigurationAccessor(settings)));
        }
    }
}