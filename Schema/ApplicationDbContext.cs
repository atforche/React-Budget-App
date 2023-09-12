using Entities;
using Microsoft.EntityFrameworkCore;

namespace Schema;

public class ApplicationDbContext : DbContext
{
    #region Properties

    public DbSet<Month> Months { get; set; } = null!;

    #endregion

    #region Methods

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"");

    #endregion
}
