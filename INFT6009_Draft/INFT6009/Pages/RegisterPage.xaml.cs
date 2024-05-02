using INFT6009.Models;
using INFT6009.Services;

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
        //Add in validation
        UserModel user = new UserModel()
        {
            Email = EntryEmail.Text,
            FirstName = EntryFirstName.Text,
            LastName = EntryLastName.Text,
            AccountType = AccountType
        };

        var result = await FirebaseAuthManager.RegisterAccount(user, EntryPassword.Text);
        if (result)
        {
            //Stop spinning loading
            await Shell.Current.Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Error", "Could not create account. Check details and try again.", "OK");
        }
    }

    private void LoginLabel_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.Navigation.PopModalAsync();
    }
}