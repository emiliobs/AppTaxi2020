using AppTaxi2020.Common.Models;

namespace AppTaxi2020.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
