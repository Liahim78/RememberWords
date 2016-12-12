using System.Collections.Generic;

namespace DAL_RememberWords.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }  

        public virtual ICollection<Word> Words { get; set; }
    }
}
