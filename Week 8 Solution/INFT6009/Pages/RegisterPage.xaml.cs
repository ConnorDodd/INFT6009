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
        //Validation such as comparing password and confirm password should go here
        if (!EntryPassword.Text.Equals(EntryPasswordConfirm.Text))
        {
            ErrorLabel.Text = "Passwords must match.";
            return;
        }
        if (!Regex.IsMatch(EntryEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase))
        {
            ErrorLabel.Text = "Email is invalid.";
            return;
        }
        if (String.IsNullOrEmpty(AccountType))
        {
            ErrorLabel.Text = "Must select an account type";
            return;
        }

        UserModel user = new UserModel()
        {
            Email = EntryEmail.Text,
            FirstName = EntryFirstName.Text,
            LastName = EntryLastName.Text,
            AccountType = AccountType
        };

        //Send to AuthManager
        var result = await FirebaseAuthManager.RegisterAccount(user, EntryPassword.Text);
        if (result)
        {
            //If successful, return to login by closing page
            await Shell.Current.Navigation.PopModalAsync();
        }
        else
        {
            switch (FirebaseAuthManager.FailReason)
            {
                case AuthErrorReason.WeakPassword:
                    await DisplayAlert("Error", "Password is too weak, must be over 6 characters, and contain a special character and number.", "OK");
                    break;
                case AuthErrorReason.EmailExists:
                    await DisplayAlert("Error", "Email has already been used to create an account.", "OK");
                    break;
                case AuthErrorReason.Undefined:
                    //Generic error message
                    await DisplayAlert("Error", "Could not create account. Check details and try again.", "OK");
                    break;
            }
        }
    }

    private void LoginLabel_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.Navigation.PopModalAsync();
    }
}