using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PanelRole : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string PanelName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
