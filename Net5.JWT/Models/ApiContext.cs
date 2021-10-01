using Microsoft.EntityFrameworkCore;
using Net5.JWT.Models.GetDataDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.JWT.Models
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<User_Role> Roles { get; set; }
        public DbSet<Found_Animal> Found_Animals { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Shelter> Shelters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { entity.HasIndex(u => u.Email).IsUnique(); });
            modelBuilder.Entity<Found_Animal>().Property(f => f.ShelterRefId).IsRequired(false);
            //modelBuilder.Entity<Found_Animal>().HasMany(c => c.Comments).WithOne(f => f.Found_Animal).OnDelete(DeleteBehavior.Cascade);
           // modelBuilder.Entity<Comments>().HasOne(f => f.Found_Animal).WithMany(c => c.Comments).OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => e.Found_AnimalRefId);
            //modelBuilder.Entity<Found_Animal>().HasMany<Comments>(c => c.C).WithOne();
        }
    }
}
