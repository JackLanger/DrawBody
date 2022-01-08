using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DrawBody.controller
{
    public class BaseController : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}