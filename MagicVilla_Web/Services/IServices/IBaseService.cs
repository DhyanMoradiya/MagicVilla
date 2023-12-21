using MagicVilla_Web.Model;
using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBaseService
    {
        public Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
