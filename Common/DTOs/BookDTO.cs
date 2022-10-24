using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class BookDTO
    {
        public string bookTitle { get; set; }
        public string bookDescription { get; set; }
        public int genreId { get; set; }
    }
}
