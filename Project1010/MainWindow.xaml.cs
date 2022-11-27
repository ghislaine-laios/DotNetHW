using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Project1010.Models;

namespace Project1010
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        private readonly CollectionViewSource _classViewSource;
        private readonly CollectionViewSource _studentViewSource;
        public MainWindow()
        {
            InitializeComponent();
            _classViewSource = (CollectionViewSource) FindResource("ClassViewSource");
            _studentViewSource = (CollectionViewSource)FindResource("StudentViewSource");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _dbContext.Database.EnsureCreated();
            _dbContext.Classes.Load();
            _classViewSource.Source = _dbContext.Classes.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.SaveChanges();
            ClassDataGrid.Items.Refresh();
            StudentsDataGrid.Items.Refresh();
        }

        private void ClassDataGrid_Selected(object sender, SelectedCellsChangedEventArgs selectedCellsChangedEventArgs)
        {

        }
    }
}
