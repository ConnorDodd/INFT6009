using INFT6009.Services;

namespace INFT6009.Pages;

public partial class LoginPage : ContentPage
{
    //Set standard keys for preferences
    private const String EMAIL_KEY = "inft6009emailkey",
        PASSWORD_KEY = "inft6009passwordkey",
        REMEMBER_ME_KEY = "inft6009remembermekey";

	public LoginPage()
	{
		InitializeComponent();
	}

    //When page appears after initialization is complete, update values from preferences.
    protected override void OnAppearing()
    {
        base.OnAppearing();

        //Use saved values or default if none are saved
        EntryEmail.Text = Preferences.Default.Get<String>(EMAIL_KEY, "");
        EntryPassword.Text = Preferences.Default.Get<String>(PASSWORD_KEY, "");
        RememberMeCheckbox.IsChecked = Preferences.Default.Get<bool>(REMEMBER_ME_KEY, false);
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        //If login failed, display an error and return
        if (!await FirebaseAuthManager.Login(EntryEmail.Text, EntryPassword.Text))
        {
            await Application.Current.MainPage.
                DisplayAlert("Error", "Could not log in, please check credentials and try again", "Ok");
            EntryPassword.Text = "";
            return;
        }

        //If login succeded, save details (if checkbox is ticked) and close page
        if (RememberMeCheckbox.IsChecked)
        {
            //Update preferences
            Preferences.Set(EMAIL_KEY, EntryEmail.Text);
            Preferences.Set(PASSWORD_KEY, EntryPassword.Text);
            Preferences.Set(REMEMBER_ME_KEY, true);
        }
        else
        {
            //Clear preferences
            Preferences.Default.Remove(EMAIL_KEY);
            Preferences.Default.Remove(PASSWORD_KEY);
            Preferences.Default.Remove(REMEMBER_ME_KEY);
        }

        //Remove page from stack (close page)
		Shell.Current.Navigation.PopAsync();
    }

    private void RegisterLabel_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RegisterPage());
    }
}