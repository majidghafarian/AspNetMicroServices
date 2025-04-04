

using Ordering.Application.Models;

namespace Ordering.Application.Contracts.infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);  
    }
}
