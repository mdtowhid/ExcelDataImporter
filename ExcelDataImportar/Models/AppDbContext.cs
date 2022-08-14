using Microsoft.EntityFrameworkCore;

namespace ExcelDataImportar.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
               : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<AuditData> AuditData { get; set; }
        public DbSet<AuditMasterData> ExcelMasterDatas { get; set; }

    }
}
