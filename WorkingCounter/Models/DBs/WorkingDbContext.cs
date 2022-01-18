namespace WorkingCounter.Models.DBs
{
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    public class WorkingDbContext : DbContext
    {
        private string databaseFileName = "workingdb.sqlite";

        public DbSet<Work> Works { get; set; }

        public DbSet<WorkingUnit> WorkingUnits { get; set; }

        public void CreateDatabase()
        {
            if (!File.Exists(databaseFileName))
            {
                Database.EnsureCreated();
            }
        }

        public void Insert(Work work)
        {
            Works.Add(work);
            SaveChanges();
        }

        public void Insert(WorkingUnit workingUnit)
        {
            WorkingUnits.Add(workingUnit);
            SaveChanges();
        }

        public List<Work> GetWorks()
        {
            return Works.Select(w => w).ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!File.Exists(databaseFileName))
            {
                SQLiteConnection.CreateFile(databaseFileName); // ファイルが存在している場合は問答無用で上書き。
            }

            var connectionString = new SqliteConnectionStringBuilder { DataSource = databaseFileName }.ToString();
            optionsBuilder.UseSqlite(new SQLiteConnection(connectionString));
        }
    }
}
