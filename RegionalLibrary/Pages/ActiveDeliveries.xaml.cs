using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RegionalLibrary.Model;

namespace RegionalLibrary.Pages
{
    public partial class ActiveDeliveries : Page
    {
        private readonly DatabaseContext _databaseContext;
        private BindingList<Delivery> _visitors;

        public ActiveDeliveries()
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
            
            _databaseContext.Employees.Load();
            _databaseContext.Visitors.Load();
            
            _databaseContext.Deliveries.Load();
        }
        
        private void UpdateBinding()
        {
            _visitors = _databaseContext.Deliveries.Local.ToBindingList();
            DeliveriesList.ItemsSource = _visitors;
        }

        private void RemoveDelivery(object sender, RoutedEventArgs e)
        {
            Delivery delivery = DeliveriesList.SelectedItem as Delivery;

            if(delivery is null == true)
                return;

            _databaseContext.Deliveries.Remove(_databaseContext.Deliveries.First(x => x.Id == delivery.Id));
            _databaseContext.SaveChanges();

            MessageBox.Show("Выдача успешно удалена", "Уведомление", MessageBoxButton.OK);
        }
    }
}