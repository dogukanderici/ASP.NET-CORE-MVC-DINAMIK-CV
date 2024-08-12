using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Service : IEntity
    {
        [Key]
        public int SercviceId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
