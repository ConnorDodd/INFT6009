using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.UI.Notifications;

namespace INFT6009.Platforms.Windows
{
    internal class ToastNotificationBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var details = taskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;
            if (details != null)
            {
                var arguments = details.Argument; // "action=viewMessage&messageId=12345"
                var userInput = details.UserInput;

                // Parse the arguments
                var args = new WwwFormUrlDecoder(arguments);
                var action = args.GetFirstValueByName("action");
                var messageId = args.GetFirstValueByName("messageId");

                // Handle the arguments (e.g., navigate to a specific page in the app)
            }
        }
    }
}
