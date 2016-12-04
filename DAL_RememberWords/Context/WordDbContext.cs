using System.Data.Entity;
using DAL_RememberWords.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL_RememberWords.Context
{
    public class WordDbContext: IdentityDbContext<User>
    {
        public DbSet<Group> Groups { get; set; }
        
        public DbSet<Word> Words { get; set; }

        public WordDbContext()
            :base("WordContext", throwIfV1Schema: false)
        {
            
        }
        public static WordDbContext Create()
        {
            return new WordDbContext();
        }
    }

}
