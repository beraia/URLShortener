namespace URL_Shortener.Models
{
    public class GetByIdRequest
    {
        public Guid Id { get; set; }
    }

    public class GetByIdResponse : Response
    {
        public string Address { get; set;}
    }
}
