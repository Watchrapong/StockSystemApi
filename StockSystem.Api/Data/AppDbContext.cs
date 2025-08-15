using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StockSystem.Api.Domain;
using StockSystem.Api.Extensions;
using StockSystem.Api.Utils;

namespace StockSystem.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeToRegisters = typeof(BaseEntity).GetTypeInfo().Assembly.DefinedTypes.Select(t => t.AsType());

            modelBuilder.RegisterEntities(typeToRegisters);

            base.OnModelCreating(modelBuilder);

            // Load entity config
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
        }

        public override int SaveChanges()
        {
            // automatic set CreatedOnUtc and UpdatedOnUtc
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedOnUtc = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedOnUtc = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // automatic set CreatedOnUtc and UpdatedOnUtc
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedOnUtc = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedOnUtc = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(default);
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connDev = "Server=localhost,1433;Database=StockSystemDB;TrustServerCertificate=True;User Id=sa;Password=P@ssw0rd1234";
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connDev);

            return new AppDbContext(optionsBuilder.Options);
        }
    }

    static class AppDbContextConfigurations
    {
        internal static void RegisterEntities(this ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var entityTypes = typeToRegisters.Where(t => t.GetTypeInfo().IsSubclassOf(typeof(BaseEntity)) && !t.GetTypeInfo().IsAbstract);

            foreach (var type in entityTypes)
            {
                modelBuilder.Entity(type);
            }
        }
        
        internal static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder) where TEntity : BaseEntity, ISoftDelete
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(entity => entity.DeletedOnUtc == null);
        }

    }

}