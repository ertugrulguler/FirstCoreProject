using FirstWebApiCoreProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApiCoreProject.Context
{
    public class ProjectContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }

        
    }
}
