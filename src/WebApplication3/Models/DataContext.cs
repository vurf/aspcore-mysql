using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Config;

namespace WebApplication3.Models
{
    [DbConfigurationType(typeof(MySQLConfiguration))]
    public class DataContext : DbContext 
    {
        public DataContext(IConfiguration config) : base("Server=localhost; Database=test; Uid=root; Pwd=2034020v;")
        {
            /*base(config.Get("Data:DefaultConnection:ConnectionString"))*/
        }

        public virtual DbSet<PostEntity> Posts { get; set; }
    }
}
