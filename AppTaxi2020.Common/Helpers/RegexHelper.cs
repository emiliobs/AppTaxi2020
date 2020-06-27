using System;
using System.Net.Mail;

namespace AppTaxi2020.Common.Helpers
{
    public class RegexHelper : IRegexHelper
    {
        public bool IsValidEmail(string emailaAddress)
        {
            try
            {
                new MailAddress(emailaAddress);
                return true;
            }
            catch (FormatException)
            {

                return false;
            }
        }
    }
}