using Yogam.AMC.Data.Models;

namespace Yogam.AMC.Infrastructure
{
    public interface ICurrentUser
    {
        ApplicationUser User { get; }
    }
}