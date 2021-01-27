using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Models
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
