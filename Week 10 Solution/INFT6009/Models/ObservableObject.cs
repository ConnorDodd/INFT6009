using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace INFT6009.Models
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //This is just a fancy way of writing a function that calls another function/event
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
