using System.ComponentModel;

namespace appWPF
{
    public class Notifica : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
