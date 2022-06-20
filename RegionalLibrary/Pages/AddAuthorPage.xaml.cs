using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RegionalLibrary.Model;

namespace RegionalLibrary.Pages
{
    public partial class AddAuthorPage : Page
    {
        private readonly DatabaseContext _databaseContext;
        private BindingList<Author> _authors;

        public AddAuthorPage()
        {
            InitializeComponent();

            _databaseContext = new DatabaseContext();
            
            UpdateBinding();
        }

        private void UpdateBinding()
        {
            LoadDatabase();

            AuthorsList.ItemsSource = _authors;
        }
        
        private void LoadDatabase()
        {
            _databaseContext.Authors.Load();

            _authors = _databaseContext.Authors.Local.ToBindingList();
        }
        
        private void AddAuthor(object sender, RoutedEventArgs e)
        {
            string firstName = FirstName.Text;
            string secondName = SecondName.Text;
            string middleName = MiddleName.Text;
            
            if (firstName == "" || secondName == "" || middleName == "")
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK);
                return;
            }

            Author author = new Author {FullName = $"{firstName} {secondName} {middleName}"};

            _databaseContext.Authors.Add(author);
            _databaseContext.SaveChanges();
            
            UpdateBinding();
            
        }
        
        private void DeleteAuthor(object sender, RoutedEventArgs e)
        {
            Author selectedAuthor = AuthorsList.SelectedItem as Author;

            if(selectedAuthor is null == true)
                return;

            DeleteBookWithAuthor(selectedAuthor);
            _databaseContext.Authors.Remove(_databaseContext.Authors.First(a => a.Id == selectedAuthor.Id));
            
            _databaseContext.SaveChanges();

            MessageBox.Show("Автор удалён", "Уведомление", MessageBoxButton.OK);
        }

        private void DeleteBookWithAuthor(Author author)
        {
            _databaseContext.Books.Load();
            
            Book entity = _databaseContext.Books.FirstOrDefault(b => b.Author.Id == author.Id);

            if (entity is null == false)
                _databaseContext.Books.Remove(entity);
        }
    }
}