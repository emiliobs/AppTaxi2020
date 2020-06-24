using System;
using System.Collections.Generic;
using System.Text;

namespace AppTaxi2020.Common.Models
{
    public class TokenResponse
    {
      
            public string Token { get; set; }

            public DateTime Expiration { get; set; }

            public DateTime ExpirationLocal => Expiration.ToLocalTime();
        }

}
