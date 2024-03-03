namespace BLO.Models
{
    public class UserListRequest
    {
        public int? id { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? skillsets { get; set; }
        public string? hobby { get; set; }
        public short status { get; set; }
    }
}
