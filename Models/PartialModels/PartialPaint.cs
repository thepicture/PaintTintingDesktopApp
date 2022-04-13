using PropertyChanged;
using System.ComponentModel;

namespace PaintTintingDesktopApp.Models.Entities
{
    [AddINotifyPropertyChangedInterface]
    public partial class Paint : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
