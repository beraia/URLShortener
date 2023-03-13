namespace URL_Shortener.Models
{
    public class CreateRequest
    {
        public string Address { get; set; }

    }

    public class CreateResponse : Response
    {
        public string ShortUrl { get; set; }
    }
}
