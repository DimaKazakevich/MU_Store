using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace WebUI.App_Start
{
    internal class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}