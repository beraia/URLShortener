namespace URL_Shortener.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public virtual User User { get; set; }
    }
}
