using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using RegionalLibrary.Model;

namespace RegionalLibrary.Pages
{
    public partial class AddGenrePage : Page
    {
        private readonly DatabaseContext _databaseContext;
        private BindingList<Genre> _genres;

        public AddGenrePage()
        {
            InitializeComponent();
            
            _databaseContext = new DatabaseContext();
            
            UpdateBinding();
        }

        private void UpdateBinding()
        {
            LoadDatabase();
            GenresList.ItemsSource = _genres;
        }
        
        private void LoadDatabase()
        {
            _databaseContext.Genres.Load();

            _genres = _databaseContext.Genres.Local.ToBindingList();
        }

        private void AddGenre(object sender, RoutedEventArgs e)
        {
            string genreName = GenreName.Text;

            if (genreName == "")
            {
                MessageBox.Show("Не указано имя жанра", "Ошибка", MessageBoxButton.OK);
                return;
            }

            Genre genre = new Genre {Name = genreName};
            
            _databaseContext.Genres.Add(genre);
            _databaseContext.SaveChanges();

            MessageBox.Show("Жанр добавлен", "Уведомление", MessageBoxButton.OK);
            GenreName.Text = "";
            
            UpdateBinding();
        }
        
        private void DeleteGenre(object sender, RoutedEventArgs e)
        {
            Genre selectedGenre = GenresList.SelectedItem as Genre; 
            
            if(selectedGenre is null == true)
                return;
            
            DeleteBookWithGenre(selectedGenre);

            _databaseContext.Genres.Remove(_databaseContext.Genres.First(g => g.Id == selectedGenre.Id));
            _databaseContext.SaveChanges();
            
            MessageBox.Show("Жанр удалён", "Уведомление", MessageBoxButton.OK);
        }
        
        private void DeleteBookWithGenre(Genre genre)
        {
            _databaseContext.Books.Load();
            
            Book entity = _databaseContext.Books.FirstOrDefault(b => b.Genre.Id == genre.Id);

            if (entity is null == false)
                _databaseContext.Books.Remove(entity);
        }
    }
}