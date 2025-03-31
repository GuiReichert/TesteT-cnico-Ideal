using Microsoft.EntityFrameworkCore;
using TesteTécnicoIdeal.API.Models;

namespace TesteTécnicoIdeal.API.Database
{
    public class TesteTécnicoIdeal_DbContext : DbContext
    {
        public TesteTécnicoIdeal_DbContext(DbContextOptions<TesteTécnicoIdeal_DbContext> db) : base(db)
        {
            
        }


        public DbSet<User> Users { get; set; }


    }
}
