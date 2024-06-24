namespace AccountProject.DomainLayer.Entities
{
    public class Account
    {
        public string ACC_Number { get; set; }
        public string? ACC_Parent { get; set; }
        public decimal? Balance { get; set; }
    }
}
