using Firebase.Auth;
using Firebase.Auth.Providers;
using INFT6009.Services;
using System.Net;

namespace INFT6009.Pages;

public partial class LoginPage : ContentPage
{
    private bool _isLoading;
    public bool IsLoading
    {
        get
        {
            return _isLoading;
        }
        set
        {
            _isLoading = value;
            OnPropertyChanged("IsLoading");
        }
    }

    public int Test { get; set; } = 1;

    public LoginPage()
    {
        InitializeComponent();

        BindingContext = this;
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        IsLoading = true;
        if (await FirebaseAuthManager.Login(EntryEmail.Text, EntryPassword.Text))
        {
            await DisplayAlert("Success", $"Hello, {FirebaseAuthManager.CurrentUser.FirstName}!", "OK");
            await Shell.Current.Navigation.PopAsync();
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Could not log in, please check credentials and try again", "Ok");
            EntryPassword.Text = "";
        }
        IsLoading = false;
    }

    private void RegisterLabel_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RegisterPage());
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Test++;
        OnPropertyChanged("Test");
    }
}