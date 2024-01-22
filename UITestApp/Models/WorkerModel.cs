namespace ElbrusAPI.Models
{
    public class WorkerModel
    {
        public int Id { get; set; }
        public string?  FIO { get; set; }
        public string? login { get; set; }
        public string? password { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }

    }
}
