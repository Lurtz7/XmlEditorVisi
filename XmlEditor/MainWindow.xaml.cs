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
using XmlEditor.Core;

namespace XmlEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FileName { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LocalFileRepository repository = new LocalFileRepository();
           var resources = repository.GetXmlFile(FileName);
            //Resource[] resources = new Resource[]
            //{
            //    new Resource { Name = "Name 1", ResourceData = "Yada 1" },
            //    new Resource { Name = "Name 2", ResourceData = "Yada 2" },
            //    new Resource { Name = "Name 3", ResourceData = "Yada 3" },
            //};

            xmlTableDataGrid.ItemsSource = resources;
        }
    }
}
