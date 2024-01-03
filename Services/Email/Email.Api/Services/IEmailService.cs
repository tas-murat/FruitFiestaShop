using Email.API.Message;
using Email.API.Models.Dto;

namespace Email.API.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);
    }
}
