namespace Keepr.Models
{
    public class Vault
    {
        public int id { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public string CreatorId { get; set; }
        public Account Creator { get; set; }
    }
}