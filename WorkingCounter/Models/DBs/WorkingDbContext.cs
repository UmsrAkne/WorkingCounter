namespace WorkingCounter.Models.DBs
{
    using System.IO;
    using System.Data.SQLite;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    public class WorkingDbContext : DbContext
    {
        public DbSet<Work> Works { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!File.Exists("workingdb"))
            {
                SQLiteConnection.CreateFile("workingdb.sqlite"); // ファイルが存在している場合は問答無用で上書き。
            }

            var connectionString = new SqliteConnectionStringBuilder { DataSource = @"workingdb.sqlite" }.ToString();
            optionsBuilder.UseSqlite(new SQLiteConnection(connectionString));
        }
    }
}
