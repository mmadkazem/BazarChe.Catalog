namespace Catalog.Common.Persistance.Context;


public sealed class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    
}