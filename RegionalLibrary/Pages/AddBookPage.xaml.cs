using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using RegionalLibrary.Model;

namespace RegionalLibrary.Pages
{
    public partial class AddBookPage : Page
    {
        private readonly DatabaseContext _databaseContext;

        private BindingList<Genre> _genres;
        private BindingList<Author> _authors;

        public AddBookPage()
        {
            InitializeComponent();
            
            _databaseContext = new DatabaseContext();
            
            UpdateBinding();
        }

        private void UpdateBinding()
        {
            LoadDatabase();
            
            GenresList.ItemsSource = _genres;
            AuthorsList.ItemsSource = _authors;
        }
        
        private void LoadDatabase()
        {
            _databaseContext.Genres.Load();
            _databaseContext.Authors.Load();
            
            _genres = _databaseContext.Genres.Local.ToBindingList();
            _authors = _databaseContext.Authors.Local.ToBindingList();
        }

        private void AddBook(object sender, RoutedEventArgs e)
        {
            string name = BookName.Text;
            string shelvingNumber = BookShelvingNumber.Text;
            string yearOfCreation = BookYearOfCreation.Text;
            
            Genre selectedGenre = GenresList.SelectedItem as Genre;
            Author selectedAuthor = AuthorsList.SelectedItem as Author;

            if (name == "" || shelvingNumber == "" || yearOfCreation == "" || selectedGenre == null || selectedAuthor == null)
            {
                MessageBox.Show("Заполнены не все поля", "Ошибка", MessageBoxButton.OK);
                return;
            }

            Book book = new Book 
            {
                Name = name,
                YearOfCreation = yearOfCreation,
                ShelvingNumber = shelvingNumber,
                Author = selectedAuthor,
                Genre = selectedGenre
            };

            _databaseContext.Add(book);
            _databaseContext.SaveChanges();

            MessageBox.Show("Книга успешно добавлена", "Уведомления", MessageBoxButton.OK);

            ResetInputValues();
        }

        private void ResetInputValues()
        {
            BookName.Text = "";
            BookShelvingNumber.Text = "";
            BookYearOfCreation.Text = "";

            GenresList.SelectedItem = null;
            AuthorsList.SelectedItem = null;
        }
    }
}