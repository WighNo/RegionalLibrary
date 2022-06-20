using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using RegionalLibrary.Model;

namespace RegionalLibrary.Pages
{
    public partial class AddVisitorPage : Page
    {
        private readonly DatabaseContext _databaseContext;

        private BindingList<Visitor> _visitors;

        public AddVisitorPage()
        {
            InitializeComponent();

            _databaseContext = new DatabaseContext();
            
            UpdateBinding();
        }

        private void UpdateBinding()
        {
            LoadDatabase();
            
            VisitorsList.ItemsSource = _visitors;
        }
        
        private void LoadDatabase()
        {
            _databaseContext.Visitors.Load();
            _visitors = _databaseContext.Visitors.Local.ToBindingList();
        }

        private void AddVisitor(object sender, RoutedEventArgs e)
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

            Visitor visitor = new Visitor
            {
                FullName = $"{secondName} {firstName} {middleName}",
                PhoneNumber = phoneNumber
            };

            _databaseContext.Visitors.Add(visitor);
            _databaseContext.SaveChanges();
            
            MessageBox.Show("Посетитель успешно добавлен", "Уведомление", MessageBoxButton.OK);
            
            UpdateBinding();
        }

        private void RemoveVisitor(object sender, RoutedEventArgs e)
        {
            Visitor visitor = VisitorsList.ItemsSource as Visitor;
            
            if(visitor is null == true)
                return;
            
            RemoveDeliveryWithVisitor(visitor);

            _databaseContext.Visitors.Remove(visitor);
            _databaseContext.SaveChanges();

            MessageBox.Show("Посетитель успешно удалён", "Уведомление", MessageBoxButton.OK);
        }

        private void RemoveDeliveryWithVisitor(Visitor visitor)
        {
            Delivery delivery = _databaseContext.Deliveries.FirstOrDefault(x => x.Visitor.Id == visitor.Id);

            if (delivery is null == false)
            {
                _databaseContext.Deliveries.Remove(delivery);
                _databaseContext.SaveChanges();
            }
        }
    }
}