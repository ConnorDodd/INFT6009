using INFT6009.Models;
using INFT6009.Models.ViewModels;
using INFT6009.Pages;
using INFT6009.Services;

namespace INFT6009
{
    public partial class MainPage : ContentPage
    {
        //Hold a reference to the bound QuestViewModel
        QuestViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            //Create and bind a view model QuestViewModel
            BindingContext = _viewModel = new QuestViewModel();

            //If the user has not logged in yet, show the login page as a modal
            if (FirebaseAuthManager.CurrentUser == null)
                Shell.Current.Navigation.PushModalAsync(new LoginPage());
        }

        //When the page appears, reload the list of Quests from the database
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //An example of using OnPropertyChanged to force a refresh on the "Quests" property
            _viewModel.OnPropertyChanged("Quests");
        }

        //Now creates a new model when the Add Quest button is clicked, instead of reusing the same one
        private void NewQuestButton_Clicked(object sender, EventArgs e)
        {
            QuestModel model = new QuestModel();
            //Set default values
            //best practice to have dates set to now so the user has minimal scrolling to change
            model.Date = DateTime.Now;
            model.Time = model.Date.TimeOfDay;

            Shell.Current.Navigation.PushAsync(new AddQuestPage(model));
        }

        private void ShowLoginButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PushAsync(new LoginPage());
        }

        //Handle one of the template items being tapped
        private void QuestListing_Tapped(object sender, TappedEventArgs e)
        {
            //Cast the sender to a view so we can access the BindingContext
            var element = (View)sender;
            //Cast the non-specific BindingContext into the specific QuestModel so we can
            //access it's properties.
            var quest = (QuestModel)element.BindingContext;

            //Open AddQuestPage using the quest that was clicked on,
            //thereby populating the page with it's properties
            Shell.Current.Navigation.PushAsync(new AddQuestPage(quest));
        }
    }
}
