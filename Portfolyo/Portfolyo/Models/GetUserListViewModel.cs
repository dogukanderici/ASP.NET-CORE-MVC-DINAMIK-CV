using Entities.Concrete;

namespace Portfolyo.Models
{
    public class GetUserListViewModel
    {
        public WriterUser UserInfo { get; set; }
        public IList<string> UserRoleInfo { get; set; }
        public List<WriterRole> Roles { get; set; }
        public string UserRoleName { get; set; }
        public string UserMail { get; set; }
    }
}
