namespace Keepr.Models
{
    public class Keep
    {
        public new int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }
        public int Kept { get; set; }
        public new string CreatorId { get; set; }
        public new Account? Creator { get; set; }
    }
    public class VaultKeepViewModel : Keep
    {
        public int VaultKeepId { get; set; }
    }
}
