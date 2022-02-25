namespace WorkingCounter.Models.DBs
{
    using System;
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

        public DbSet<WorkTemplate> WorkTemplates { get; set; }

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

        public void Insert(WorkTemplate workTemplate)
        {
            WorkTemplates.Add(workTemplate);
            SaveChanges();
        }

        public void Delete(WorkingUnit workingUnit)
        {
            WorkingUnits.Remove(workingUnit);
            SaveChanges();
        }

        public List<Work> GetWorks()
        {
            return Works.Select(w => w).OrderByDescending(w => w.AdditionDate).ToList();
        }

        public List<Work> GetWorks(DateTime startDate, TimeSpan duration)
        {
            return Works.Where(w =>
                (w.StartDate >= startDate && w.StartDate <= startDate.AddDays(duration.TotalDays))
                || (w.LimitDate >= startDate && w.LimitDate <= startDate.AddDays(duration.TotalDays)))
                .OrderByDescending(w => w.AdditionDate)
                .ToList();
        }

        public List<WorkingUnit> GetWorkingUnits(int parentWorkId)
        {
            return WorkingUnits.Where(wu => wu.ParentWorkId == parentWorkId).OrderBy(wu => wu.AdditionDate).ToList();
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
