using Microsoft.EntityFrameworkCore;
using NetCoreStack.Data.Context;
using NetCoreStack.Data.Interfaces;
using NetCoreStack.Localization.Models;

namespace NetCoreStack.Localization
{
    public class LocalizationDBContext : EfCoreContext
    {
        private readonly IDataContextConfigurationAccessor _configurator;

        public LocalizationDBContext(IDataContextConfigurationAccessor configurator, IDatabasePreCommitFilter filter = null) : base(configurator, filter)
        {
            _configurator = configurator;
        }

        public DbSet<Language> Language { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configurator.SqlConnectionString);
        }
    }
}