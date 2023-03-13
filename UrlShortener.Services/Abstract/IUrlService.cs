using URL_Shortener.Models;

namespace URL_Shortener.Services
{
    public interface IUrlService
    {
        Task<CreateResponse> Create(CreateRequest request);
        Task<GetByIdResponse> GetById(GetByIdRequest request);
        Task<DeleteResponse> Delete(DeleteRequest request);
    }
}
