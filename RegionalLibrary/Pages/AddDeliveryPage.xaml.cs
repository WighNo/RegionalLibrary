using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RegionalLibrary.Model;

namespace RegionalLibrary.Pages
{
    public partial class AddDeliveryPage : Page
    {
        private readonly DatabaseContext _databaseContext;

        private BindingList<Book> _books;
        private BindingList<Visitor> _visitors;
        private BindingList<Employee> _employees;

        public AddDeliveryPage()
        {
            InitializeComponent();

            _databaseContext = new DatabaseContext();
            
            UpdateBinding();
        }

        private void UpdateBinding()
        {
            LoadDatabase();
            
            _books = _databaseContext.Books.Local.ToBindingList();
            _visitors = _databaseContext.Visitors.Local.ToBindingList();
            _employees = _databaseContext.Employees.Local.ToBindingList();

            VisitorsList.ItemsSource = _visitors;
            EmployeeList.ItemsSource = _employees;
            BookList.ItemsSource = _books;
        }

        private void LoadDatabase()
        {
            _databaseContext.Books.Load();
            _databaseContext.Visitors.Load();
            _databaseContext.Employees.Load();
        }

        private void AddDelivery(object sender, RoutedEventArgs e)
        {
            Book book = BookList.SelectedItem as Book;
            Visitor visitor = VisitorsList.SelectedItem as Visitor;
            Employee employee = EmployeeList.SelectedItem as Employee;

            string startDate = StartDate.Text;
            string endDate = EndDate.Text;

            if (book is null == true || visitor is null == true || employee is null == true || startDate == "" || endDate == "")
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK);
                return;
            }

            Delivery delivery = new Delivery();

            delivery.Book = _databaseContext.Books.First(x => x.Id == book.Id);
            delivery.Visitor = _databaseContext.Visitors.First(x => x.Id == visitor.Id);
            delivery.Employee = _databaseContext.Employees.First(x => x.Id == employee.Id);

            delivery.DateIssue = startDate;
            delivery.ReturnDate = endDate;

            _databaseContext.Deliveries.Add(delivery);
            _databaseContext.SaveChanges();
            
            MessageBox.Show("Выдача успешно добавлена", "Уведомление", MessageBoxButton.OK);
        }
    }
}