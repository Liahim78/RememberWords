using System.Collections.Generic;

namespace DAL_RememberWords.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int UserInfo { get; set; }
        public virtual User User { get; set; }  

        public virtual ICollection<Word> Words { get; set; }
    }
}
