using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;
using RegionalLibrary.Pages;

namespace RegionalLibrary
{
    public partial class MainWindow : MetroWindow
    {
        private DatabaseContext _databaseContext;

        private Dictionary<Button, Type> _buttonsPages;

        private Button _lastButton;

        public MainWindow()
        {
            InitializeComponent();
            InitializeButtonsPages();
            
            OpenPage(OpenMainPage, null);
        }

        private void InitializeButtonsPages()
        {
            _buttonsPages = new Dictionary<Button, Type>();
            
            _buttonsPages.Add(OpenMainPage, typeof(MainFramePage));
            _buttonsPages.Add(OpenAddAuthor, typeof(AddAuthorPage));
            _buttonsPages.Add(OpenAddGenre, typeof(AddGenrePage));
            _buttonsPages.Add(OpenAddBook, typeof(AddBookPage));
            _buttonsPages.Add(OpenAddEmployee, typeof(AddEmployeePage));
            _buttonsPages.Add(OpenAddVisitor, typeof(AddVisitorPage));
            _buttonsPages.Add(OpenActiveDeliveries, typeof(ActiveDeliveries));
            _buttonsPages.Add(OpenCreateDelivery, typeof(AddDeliveryPage));
        }

        private void OpenPage(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            
            UpdateButtonsColor(button);
            CahngeFrameContent(button);
        }

        private void UpdateButtonsColor(Button button)
        {
            if(_lastButton is null == false)
                _lastButton.Foreground = Brushes.Gray;
            
            button.Foreground = Brushes.White;
            _lastButton = button;
        }

        private void CahngeFrameContent(Button button)
        {
            
            Main.Content = Activator.CreateInstance(_buttonsPages[button]);
        }
    }
}