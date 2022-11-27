using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Project0926
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private TreeViewItem _dummyItem = new TreeViewItem();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                AddItemToTree(FolderTree, s, s);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var root = (TreeViewItem)sender;
            if (root.Items.Count > 0) return;
            root.Items.Clear();
            var path = root.Tag.ToString()!;
            UpdateFiles(path);
            foreach (string s in Directory.GetDirectories(path))
            {
                AddItemToTree(root, s, s.Substring(s.LastIndexOf("\\", StringComparison.Ordinal) + 1));
            }
        }

        private void AddItemToTree(ItemsControl root, string path, string header)
        {
            var item = new TreeViewItem
            {
                Header = header,
                Tag = path
            };
            item.Expanded += Folder_Expanded;
            root.Items.Add(item);
        }

        private void UpdateFiles(string dirPath)
        {
            ModelView.Files.Clear();
            var info = new DirectoryInfo(dirPath);
            foreach (var directoryInfo in info.GetDirectories())
            {
                ModelView.Files.Add(directoryInfo);
            }

            foreach (var fileInfo in info.GetFiles())
            {
                ModelView.Files.Add(fileInfo);
            }
        }

        private MainModelView ModelView => (MainModelView)this.DataContext;

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listView = (ListView) sender;
            var selected = listView.SelectedItem;
            if (selected is null) return;
            var info = (FileSystemInfo) selected;
            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer",
                Arguments = "\"" + info.FullName + "\""
            });
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
