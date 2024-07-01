using Firebase.Auth;
using INFT6009.Models;
using INFT6009.Services;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Text.RegularExpressions;

namespace INFT6009.Pages;

public partial class RegisterPage : ContentPage
{
    public string AccountType { get; set; }

    public RegisterPage()
	{
		InitializeComponent();

        BindingContext = this;
	}

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        
    }

    private void LoginLabel_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.Navigation.PopModalAsync();
    }
}