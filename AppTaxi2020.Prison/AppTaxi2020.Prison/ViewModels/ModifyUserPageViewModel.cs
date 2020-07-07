using AppTaxi2020.Common.Enums;
using AppTaxi2020.Common.Helpers;
using AppTaxi2020.Common.Models;
using AppTaxi2020.Common.Services;
using AppTaxi2020.Prison.Helpers;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTaxi2020.Prison.ViewModels
{
    public class ModifyUserPageViewModel : ViewModelBase
    {

        private bool _isRunning;
        private bool _isEnabled;
        private ImageSource _image;
        private UserResponse _user;
        private MediaFile _file;
        private DelegateCommand _changeImagenCommand;
        private DelegateCommand _saveCommand;
        private readonly INavigationService _navigationService;
        private readonly IFilesHelper _filesHelper;
        private readonly IApiService _apiService;

        public ModifyUserPageViewModel(INavigationService navigationService, IFilesHelper filesHelper, IApiService apiService) : base(navigationService)
        {
            Title = Languages.ModifyUser;
            IsEnabled = true;
            User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            Image = User.PictureFullPath;
            _navigationService = navigationService;
            _filesHelper = filesHelper;
            _apiService = apiService;
        }

        public DelegateCommand ChangeImagenCommand => _changeImagenCommand ?? (_changeImagenCommand = new DelegateCommand(ChangeImageAsync));
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void SaveAsync()
        {
            var isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            var userRequest = new UserRequest
            {
                Address = User.Address,
                Document = User.Document,
                Email = User.Email,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Password = "Eabs123.", // It doesn't matter what is sent here. It is only for the model to be valid
                Phone = User.PhoneNumber,
                PictureArray = imageArray,
                UserTypeId = User.UserType == UserType.User ? 1 : 2,
                CultureInfo = Languages.Culture
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.PutAsync(url, "api", "/Account", userRequest, "bearer", token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Settings.User = JsonConvert.SerializeObject(User);
            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.UserUpdated, Languages.Accept);
        }



        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(Languages.PictureSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.FromCamera);

            if (source == Languages.Cancel)
            {
                _file = null;
                return;
            }

            if (source == Languages.FromCamera)
            {
                _file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = PhotoSize.Small,
                });
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();
                    return stream;
                });
            }
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(User.Document))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.DocumentError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.FirstName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.LastName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Address))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.AddressError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.PhoneNumber))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PhoneError, Languages.Accept);
                return false;
            }

            return true;

        }



    }
}
