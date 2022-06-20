using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RegionalLibrary.Model
{
    public class Delivery : INotifyPropertyChanged
    {
        private string _dateIssue;
        private string _delay;
        private string _returnDate;

        private Book _book;

        private Employee _employee;
        private Visitor _visitor;
        
        public int Id { get; set; }

        public string DateIssue
        {
            get => _dateIssue;
            set
            {
                _dateIssue = value;
                OnPropertyChanged("DateIssue");
            }
        }
            
        public string Delay
        {
            get => _delay;
            set
            {
                _delay = value;
                OnPropertyChanged("Delay");
            }
        }

        public string ReturnDate 
        {
            get => _returnDate;
            set
            {
                _returnDate = value;
                OnPropertyChanged("ReturnDate");
            }
        }

        public Book Book
        {
            get => _book;
            set
            {
                _book = value;
                OnPropertyChanged("Book");
            }
        }

        public Employee Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                OnPropertyChanged("Employee");
            }
        }

        public Visitor Visitor
        {
            get => _visitor;
            set
            {
                _visitor = value;
                OnPropertyChanged("Visitor");
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