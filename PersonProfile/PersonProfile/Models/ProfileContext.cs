using Microsoft.EntityFrameworkCore;
using PersonProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonProfile.Models
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().HasData(
                new Profile() { Id = 1, Name = "sita", Age = 28, Qualification = "B.E CSE", IsEmployed = "yes", NoticePeriod = "7/8/2022", CurrentCTC = 45000 }
                );

        }
    }
}