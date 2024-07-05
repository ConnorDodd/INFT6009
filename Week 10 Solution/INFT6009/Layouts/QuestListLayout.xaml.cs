using INFT6009.Models;
using INFT6009.Pages;

namespace INFT6009.Layouts;

public partial class QuestListLayout : ContentView
{
	public QuestListLayout()
	{
		InitializeComponent();
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