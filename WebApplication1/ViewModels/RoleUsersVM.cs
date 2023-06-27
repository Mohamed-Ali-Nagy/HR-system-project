namespace HRSystem.ViewModels
{
    public class RoleUsersVM
    {
        public string RoleName { get; set; }
        public List<string> UserEmail { get; set; } = new List<string>();
    }
}
