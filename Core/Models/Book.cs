using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Book : BaseModel
    {
        public int bookId { get; set; }
        public string bookTitle { get; set; }
        public string bookDescription { get; set; }
        [ForeignKey("genre")]
        public int genreId { get; set; }
        public Genre genre { get; set; }
    }
}
