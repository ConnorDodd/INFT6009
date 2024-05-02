using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace INFT6009.Services
{
    public static class NotificationManager
    {
        public static async void ShowToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(message, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
    }
}
