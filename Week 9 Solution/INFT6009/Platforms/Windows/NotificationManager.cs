using INFT6009.Platforms.Windows;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace INFT6009.Services
{
    public static partial class NotificationManager
    {
        static NotificationManager()
        {
            var builder = new BackgroundTaskBuilder
            {
                Name = "ToastNotificationBackgroundTask",
                TaskEntryPoint = typeof(ToastNotificationBackgroundTask).FullName
            };
            builder.SetTrigger(new ToastNotificationActionTrigger());

            var task = builder.Register();
        }

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
