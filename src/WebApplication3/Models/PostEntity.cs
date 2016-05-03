using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    [Table("postentity")]
    public class PostEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Content { get; set; }
    }
}
