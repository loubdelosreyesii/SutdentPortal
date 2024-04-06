using Microsoft.EntityFrameworkCore;
using SutdentPortal.Web.Models.Entities;

namespace SutdentPortal.Web.Data
{
    //Inherit DBContext Class
    public class ApplicationDbContext : DbContext
    {
        //type ctor to create constructor class
        //Bridge between Application and Sql Server Database.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Representation of collection of your entity
        public DbSet<Student> Students { get; set; }
    }
}
