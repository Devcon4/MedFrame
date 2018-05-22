using System;
using Microsoft.EntityFrameworkCore;

namespace demo
{
    public class DemoDB : DbContext
    {
        public DemoDB(DbContextOptions<DemoDB> options) : base(options) { }
        public DemoDB() : base(new DbContextOptions<DemoDB>() { }) {}

        public DbSet<AgendaScheduleItem> AgendaScheduleItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}