using System;
using System.Collections.Generic;
using System.Data;
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
using XmlEditor.Core;

namespace XmlEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LocalFileRepository repository = new LocalFileRepository();
        public string FileName { get; set; }
        DataTable dt = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
           
            
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            var resources = repository.GetXmlFile(FileName);
            
            xmlTableDataGrid.ItemsSource = resources;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            List<Resource> resourceList = new List<Resource>();
            foreach (Resource item in xmlTableDataGrid.Items)
            {
                resourceList.Add(item);   
            }
            repository.SaveXmlFile(FileName, resourceList);
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
