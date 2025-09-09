using System.Collections.Generic;
using System.Reflection.Emit;
using CMCS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CMCS.Web.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Lecturer> Lecturers => Set<Lecturer>();
        public DbSet<Claim> Claims => Set<Claim>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<ClaimStatusHistory> ClaimStatusHistories => Set<ClaimStatusHistory>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed a demo lecturer so UI works immediately
            modelBuilder.Entity<Lecturer>().HasData(
                new Lecturer { Id = 1, FullName = "Demo Lecturer", Email = "demo.lecturer@example.com" }
            );
        }
    }
}