using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        //private readonly ILogger logger;

        //public AppDbContext()
        //{
            
        //}

        public AppDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public virtual DbSet<SampleTable> SampleTable { get; set; }
        public virtual DbSet<Employeecs> Employee { get; set; }
        public virtual DbSet<Department> Department { get; set; }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Author> Author { get; set; }

        public virtual DbSet<AuthorshipRole> AuthorshipRoles { get; set; }
        public virtual DbSet<BookAuthorShip> BookAuthorShips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dBContextOptionsBuilder)
        {
            //logger.LogError("connection : " + configuration["Connection"]);
            dBContextOptionsBuilder.UseSqlServer(
                 configuration["DBConnection"],
                  x => x.UseNetTopologySuite().EnableRetryOnFailure());

            //dBContextOptionsBuilder.UseCosmos(
            //"https://testcosmosdbjsp.documents.azure.com:443/",
            //"UqlUjVkzQunAcVDnZ2RItJamZ2kyET8vt9NjfqEauSN1019TDZs6KgklwcUMKRFDpsuzjv6zZQYfGMmKaCUdeg==",
            //databaseName: "test-db");


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
