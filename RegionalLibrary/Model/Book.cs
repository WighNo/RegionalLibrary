using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RegionalLibrary.Model
{
    public class Book : INotifyPropertyChanged
    {
        private string _shelvingNumber;
        private string _yearOfCreation;

        private string _name;

        private Genre _genre;
        private Author _author;
        
        public int Id { get; set; }
        
        public string ShelvingNumber 
        {
            get => _shelvingNumber;
            set
            {
                _shelvingNumber = value;
                OnPropertyChanged("ShelvingNumber");
            }
        }
        
        public string YearOfCreation 
        {
            get => _yearOfCreation;
            set
            {
                _yearOfCreation = value;
                OnPropertyChanged("YearOfCreation");
            }
        }

        public string GetYearOfCreation => $"{YearOfCreation} год";
        
        public string Name 
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        
        public string GetSubName
        {
            get
            {
                string[] words = _name.Split(' ');

                string result = "";

                for (int i = 0; i < words.Length; i++)
                    result += $"{words[i]}\n";

                return result;
            }
        }

        public Genre Genre 
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged("Genre");
            }
        }
        
        public Author Author 
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged("Author");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}