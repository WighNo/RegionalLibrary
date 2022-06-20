using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using RegionalLibrary.Model;

namespace RegionalLibrary.Pages
{
    public partial class AddEmployeePage : Page
    {
        private readonly DatabaseContext _databaseContext;

        private BindingList<Employee> _employees;

        public AddEmployeePage()
        {
            InitializeComponent();

            _databaseContext = new DatabaseContext();
            
            UpdateBinding();
        }

        private void UpdateBinding()
        {
            LoadDatabase();
            
            EmployeeList.ItemsSource = _employees;
        }
        
        private void LoadDatabase()
        {
            _databaseContext.Employees.Load();
            _employees = _databaseContext.Employees.Local.ToBindingList();
        }
        
        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            string firstName = FirstName.Text;
            string secondName = SecondName.Text;
            string middleName = MiddleName.Text;

            string phoneNumber = PhoneNumber.Text;

            if (firstName == "" || secondName == "" || middleName == "" || phoneNumber == "")
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK);
                return;
            }

            Employee employee = new Employee
            {
                FullName = $"{secondName} {firstName} {middleName}",
                PhoneNumber = phoneNumber
            };
            
            _databaseContext.Employees.Add(employee);

            _databaseContext.SaveChanges();
            
            UpdateBinding();
        }
        
        private void RemoveEmployee(object sender, RoutedEventArgs eventArgs)
        {
            Employee employee = EmployeeList.SelectedItem as Employee;
            
            if(employee is null == true)
                return;

            RemoveDeliveryFromEmployee(employee);

            _databaseContext.Employees.Remove(_databaseContext.Employees.First(e => e.Id == employee.Id));
            _databaseContext.SaveChanges();
            
            MessageBox.Show("Сотрудник успешно удалён", "Уведомление", MessageBoxButton.OK);
        }

        private void RemoveDeliveryFromEmployee(Employee employee)
        {
            _databaseContext.Deliveries.Load();
            Delivery delivery = _databaseContext.Deliveries.FirstOrDefault(d => d.Employee.Id == employee.Id);

            if (delivery is null == false)
            {
                _databaseContext.Deliveries.Remove(_databaseContext.Deliveries.First(d => d.Employee.Id == employee.Id));
                _databaseContext.SaveChanges();
            }
        }
    }
}