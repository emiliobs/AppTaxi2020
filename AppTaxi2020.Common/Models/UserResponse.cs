using AppTaxi2020.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTaxi2020.Common.Models
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PicturePath { get; set; }

        public UserType UserType { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        //

        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
             ? "http://192.168.0.11:8055//images/noimage.png"
             : $"http://192.168.0.11:8055{PicturePath.Substring(1)}";

             //: $"http://192.168.0.11:8055/images/Users/b254c445-7a1b-4774-8473-398ac18d6e78.jpg";

        // return $"https://localhost:44308{this.ImageUrl.Substring(1)}";

        //public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
        //      ? "https://TaxiWeb3.azurewebsites.net//images/noimage.png"
        //      : $"https://zulutaxi.blob.core.windows.net/users/{PicturePath}";
    }
}
