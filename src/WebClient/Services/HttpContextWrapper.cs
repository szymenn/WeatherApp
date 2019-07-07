using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace WebClient.Services
{
    public class HttpContextWrapper : IHttpContextWrapper
    {
        public async Task<string> GetTokenAsync(string tokenName, HttpContext context)
        {
            return await context.GetTokenAsync(tokenName);
        }
    }
}