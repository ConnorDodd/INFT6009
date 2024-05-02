using INFT6009.Models;
using INFT6009.Pages;
using INFT6009.Services;
using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;

namespace INFT6009
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<QuestModel> Quests { get; set; } = new ObservableCollection<QuestModel>();
        private bool _isRefreshing = false;

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            }
        }

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

            //If the user has not logged in yet, show the login page as a modal
            if (FirebaseAuthManager.CurrentUser == null)
            {
                Dispatcher.Dispatch(async () =>
                    await Shell.Current.Navigation.PushModalAsync(new LoginPage())
                );
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadQuests();
        }

        public async void LoadQuests()
        {
            var quests = await FirebaseStorageManager.GetQuests() ?? new List<QuestModel>();
            Quests = new ObservableCollection<QuestModel>(quests);
            OnPropertyChanged("Quests");

            MapControl.Pins.Clear();
            foreach (QuestModel quest in Quests)
            {
                var pin = new Microsoft.Maui.Controls.Maps.Pin
                {
                    Label = quest.When,
                    Address = quest.Address,
                    Location = new Location(double.Parse(quest.Latitude), double.Parse(quest.Longitude)),
                    Type = PinType.Generic
                };
                pin.MarkerClicked += async (s, e) =>
                {
                    await Shell.Current.Navigation.PushAsync(new AddQuestPage(quest));
                };

                MapControl.Pins.Add(pin);
            }

        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            LoadQuests();
        }

        private void AddQuestButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PushAsync(new AddQuestPage());
        }

        private async void QuestListing_Tapped(object sender, TappedEventArgs e)
        {
            var binding = (View)sender;
            var quest = (QuestModel)binding.BindingContext;
            await Shell.Current.Navigation.PushAsync(new AddQuestPage(quest));
        }

        private void SettingsToolbarItem_Clicked(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PushAsync(new SettingsPage());
        }
    }
}
