using Microsoft.EntityFrameworkCore;
using Monitor2.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitor2.DAL
{
    public class MonitorDBContext : DbContext
    {
        protected MonitorDBContext(DbContextOptions options)
            : base(options) { }

        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<ResponseServiceEntity> ResponseServices { get; set; }

        public MonitorDBContext(DbContextOptions<MonitorDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
