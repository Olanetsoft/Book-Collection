using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public DateTime DatePublished { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }

    }
}
