using System.Data.Entity;
using DAL_RememberWords.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL_RememberWords.Context
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Group> Groups { get; set; }
        
        public DbSet<Word> Words { get; set; }

        public ApplicationDbContext()
            :base("WordContext", throwIfV1Schema: false)
        {
            
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}
