using Microsoft.Toolkit.Uwp.Notifications;
using Windows.ApplicationModel.Background;

namespace INFT6009.Services
{
    public static partial class NotificationManager
    {
        static partial void DoSendNotification(string title, string message, DateTime scheduledTime)
        {
            ToastButton button = new ToastButton()
                .SetContent("See More")
                .AddArgument("action", "seeMore")
                .SetAfterActivationBehavior(ToastAfterActivationBehavior.Default);

            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 1001)
                .AddText(title)
                .AddText(message)
                .AddButton(button)
                .Schedule(scheduledTime);
        }
    }
}
