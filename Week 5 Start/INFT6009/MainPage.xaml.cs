using INFT6009.Models;
using INFT6009.Pages;

namespace INFT6009
{
    public partial class MainPage : ContentPage
    {
        QuestModel model;

        public MainPage()
        {
            InitializeComponent();

            model = new QuestModel();
            //Set default values, best practice to have dates set to now so the user has minimal scrolling to change
            model.Date = DateTime.Now;
            model.Time = model.Date.TimeOfDay;
        }


        private void NewQuestButton_Clicked(object sender, EventArgs e)
        {
            //Passes the model to the new page
            //Because this is a reference to the same object that is stored here (rather than a clone) any changes made on
            //the other page will apply to the reference here as well
            Shell.Current.Navigation.PushAsync(new AddQuestPage(model));
        }
    }

}
