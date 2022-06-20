using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RegionalLibrary.Model;

namespace RegionalLibrary.Pages
{
    public partial class MainFramePage : Page
    {
        private readonly DatabaseContext _databaseContext;
        private BindingList<Book> _books;

        public MainFramePage()
        {
            InitializeComponent();
            
            _databaseContext = new DatabaseContext();
            
            LoadDatabase();
            UpdateBinding();
        }

        private void LoadDatabase()
        {
            _databaseContext.Authors.Load();
            _databaseContext.Genres.Load();
            
            _databaseContext.Books.Load();
        }

        private void UpdateBinding()
        {
            _books = _databaseContext.Books.Local.ToBindingList();
            BooksList.ItemsSource = _books;
        }

        private void RemoveBook(object sender, RoutedEventArgs e)
        {
            Book selectedBook = BooksList.SelectedItem as Book;

            if(selectedBook is null == true)
                return;

            RemoveDeliveryWithBook(selectedBook);
            
            _databaseContext.Books.Remove(_databaseContext.Books.First(b => b.Id == selectedBook.Id));
            _databaseContext.SaveChanges();
            
            LoadDatabase();
            UpdateBinding();

            MessageBox.Show("Книга успешно удалена", "Уведомление", MessageBoxButton.OK);
        }

        private void RemoveDeliveryWithBook(Book book)
        {
            Delivery delivery = _databaseContext.Deliveries.FirstOrDefault(x => x.Book.Id == book.Id);

            if (delivery is null == false)
            {
                _databaseContext.Deliveries.Remove(_databaseContext.Deliveries.First(x => x.Id == delivery.Id));
                _databaseContext.SaveChanges();
            }
        }
    }
}