using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Skill : IEntity
    {
        [Key]
        public int SkillId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
