using INFT6009.Models;
using INFT6009.Services;
using Microsoft.Maui.Controls.Maps;
using System.Net;

namespace INFT6009.Pages;

public partial class AddQuestPage : ContentPage
{
    private Location location;

    public AddQuestPage()
    {
        InitializeComponent();

        BindingContext = new QuestModel();
    }

    public AddQuestPage(QuestModel quest)
    {
        InitializeComponent();

        BindingContext = quest;
        MapControl.Pins.Add(new Microsoft.Maui.Controls.Maps.Pin()
        {
            Label = "Quest",
            Address = quest.Address,
            Location = new Location(double.Parse(quest.Latitude), double.Parse(quest.Longitude)),
            Type = PinType.Generic
        });
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PopAsync();
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        QuestModel model = (QuestModel)BindingContext;
        DateTime date = DateEntry.Date.Add(TimeEntry.Time);
        model.When = date.ToString();
        model.UserId = FirebaseAuthManager.CurrentUser.UserId;
        model.Latitude = location.Latitude.ToString();
        model.Longitude = location.Longitude.ToString();

        var result = await FirebaseStorageManager.AddQuest(model);
        if (result)
        {
            NotificationManager.ShowToast("Quest added successfully");
            await Shell.Current.Navigation.PopAsync();
        }
        else
            await DisplayAlert("Error", "Could not save quest, some error occurred", "OK");
    }

    private async void MapControl_MapClicked(object sender, Microsoft.Maui.Controls.Maps.MapClickedEventArgs e)
    {
        location = e.Location;

        IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);
        Placemark? placemark = placemarks?.FirstOrDefault();

        if (placemark != null)
            AddressEntry.Text = placemark.ToString();
        else
            AddressEntry.Text = "Unspecified Address";
    }
}