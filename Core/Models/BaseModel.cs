using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BaseModel
    {
        public DateTime creationDate { get; set; }
        public byte isDeleted { get; set; }
    }
}
