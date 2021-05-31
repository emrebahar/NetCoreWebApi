using Microsoft.EntityFrameworkCore;
using NetCoreWebApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApi.DAL.Context
{
    public class NetCoreWebApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-I0M32R9\\SQLEXPRESS; database=UdemyWebApi ; integrated security=true;");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
