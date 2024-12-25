namespace Talabat.Admin.DashBoard.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleFromViewModel> Roles { get; set; }
    }
}
