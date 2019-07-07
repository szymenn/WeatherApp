using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebClient.Services
{
    public interface IHttpContextWrapper
    {
        Task<string> GetTokenAsync(string tokenName, HttpContext context);
    }
}