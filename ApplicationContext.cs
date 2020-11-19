using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_work_doc_lib.Model;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Course_work_doc_lib
{
    class ApplicationContext : DbContext
     {
        public static string connection = "server=remotemysql.com;UserId=T9UwHjA0Pi;Password=8XJbDpqpEy;database=T9UwHjA0Pi;";
        public DbSet<User> Users { get; set; }
        public DbSet<UrlDocs> UrlDoc { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connection);
        }
    }
}
