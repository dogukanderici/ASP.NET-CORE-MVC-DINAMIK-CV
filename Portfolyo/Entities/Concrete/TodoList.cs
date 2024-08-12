using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TodoList : IEntity
    {
        [Key]
        public int TodoId { get; set; }
        //public string Content { get; set; }
        public string TodoContent { get; set; }
        public bool Status { get; set; }
    }
}
