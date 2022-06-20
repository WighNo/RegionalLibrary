using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RegionalLibrary.Model
{
    public class Visitor : INotifyPropertyChanged
    {
        private string _fullName;
        private string _phoneNumber;

        public int Id { get; set; }
        
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }
        
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public string[] GetSubName => _fullName.Split(' ');
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}