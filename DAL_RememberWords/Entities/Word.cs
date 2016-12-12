using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL_RememberWords.Entities
{
    public class Word
    {
        public int Id { get; set; }

        public string WordValue { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        
        
        public string Discription { get; set; }

        public int Level { get; set; }

        public DateTime Date { get; set; }

        public Word()
        {
        }
    }
}
