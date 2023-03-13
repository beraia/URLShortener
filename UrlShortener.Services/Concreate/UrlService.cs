using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using URL_Shortener.Data;
using URL_Shortener.Models;

namespace URL_Shortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UrlService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<GetByIdResponse> GetById(GetByIdRequest request)
        {
            var id = request.Id;

            var url = await _dbContext.Urls.FirstOrDefaultAsync(x => x.Id == id);

            if (url == null)
            {
                return new GetByIdResponse() { Success = false, Message = "Wrong Id" };
            }

            return new GetByIdResponse()
            {
                Address = url.Address,
                Success = true,
            };
        }

        public async Task<CreateResponse> Create(CreateRequest request)
        {
            var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "";

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var isAuthenticated = _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

            var shortUrl = _configuration["MyUrl"];

            if (Uri.IsWellFormedUriString(request.Address, UriKind.Absolute))
            {
                try
                {
                    var client = new HttpClient();
                    var result = await client.GetAsync(request.Address);

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception("Url Can Not Be Fetched");
                    }
                }
                catch (Exception)
                {
                    return new CreateResponse
                    {
                        Success = false,
                        Message = "Url can not be fetched"
                    };
                }
                if (isAuthenticated)
                {
                    Url url = new Url()
                    {
                        User = user,
                        Address = request.Address,
                    };

                    _dbContext.Urls.Add(url);
                    _dbContext.SaveChanges();
                    return new CreateResponse() { Success = true, Message = "Added by an authorized user.", ShortUrl = shortUrl += url.Id };
                }
                else
                {
                    Url url = new Url()
                    {
                        Address = request.Address,
                    };

                    _dbContext.Urls.Add(url);
                    _dbContext.SaveChanges();

                    return new CreateResponse() { Success = true, Message = "Added by an unauthorized user.", ShortUrl = shortUrl += url.Id };
                }
            }

            return new CreateResponse() { Success = false, Message = "Wrong url" };
        }



        public async Task<DeleteResponse> Delete(DeleteRequest request)
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var id = request.Id;

            var url = await _dbContext.Urls.FirstOrDefaultAsync(x => x.Id == id);

            if (url == null)
            {
                return new DeleteResponse() { Success = false, Message = "Wrong Id" };
            }
            _dbContext.Remove(url);
            await _dbContext.SaveChangesAsync();
            return new DeleteResponse() { Success = true, Message = "Deleted" };
        }
    }
}
