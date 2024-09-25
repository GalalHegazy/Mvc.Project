using System.Threading.Tasks;

namespace Mvc.Project.PL.Servies.EmailSender
{
    public interface IEmailSender
    {
        Task SendAsync(string from,string recipients,string subject,string body);
    }
}
