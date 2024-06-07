using INFT6009.Models;

namespace INFT6009.Pages;

public partial class AddQuestPage : ContentPage
{
	QuestModel model;

	//Accept a QuestModel and bind that, so the controls will reflect any values that are already set
	public AddQuestPage(QuestModel _model)
	{
		//This line must be present on all XAML pages/components or they will not display
		InitializeComponent();

		//Keep a reference to the model passed to the contructor
		model = _model;

		//Bind it to the page so page children (i.e. controls) can access it
		BindingContext = _model;
	}

	private void SaveButton_Clicked(object sender, EventArgs e)
	{
		//Clear error label before checking again
		ErrorLabel.IsVisible = false;

		//Make sure fields aren't empty and dates aren't in the past
		if (String.IsNullOrEmpty(model.Address) ||
			String.IsNullOrEmpty(model.Description) ||
			model.Date + model.Time < DateTime.Now)
		{
			//Show error label and stop before closing page
			ErrorLabel.IsVisible = true;
			ErrorLabel.Text = "Please fill out all fields";
			return;
		}

		//Close page 
		Shell.Current.Navigation.PopAsync();
	}

    private void CancelButton_Clicked(object sender, EventArgs e)
    {

    }
}