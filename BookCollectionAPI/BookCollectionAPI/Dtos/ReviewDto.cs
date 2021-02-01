using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
    }
}
