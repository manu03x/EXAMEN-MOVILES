using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Examen
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public string welcomeMessage;
        public string imageUrl;


        public string WelcomeMessage
        {
            get { return welcomeMessage; }
            set
            {
                if (value != welcomeMessage)
                {
                    welcomeMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                if (value != imageUrl)
                {
                    imageUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
