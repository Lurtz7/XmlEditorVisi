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
        static List<Resource> resourceList = new List<Resource>();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
           
            
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            var resources = repository.GetXmlFile(FileName);
            
            xmlTableDataGrid.ItemsSource = resources;

            resourceList.Clear();

            foreach (var item in resources)
            {
                resourceList.Add(new Resource {
                    Name = item.Name,
                    Language = item.Language,
                    DateChange = item.DateChange,
                    GenericKey = item.GenericKey,
                    ResourceData = item.ResourceData,
                    Tenant = item.Tenant
                    
                });
            }
        }

        private void checkForChanges()
        {
            if (xmlTableDataGrid.Items.Count > 0 && (xmlTableDataGrid.Items.Count == resourceList.Count))
            {
                
                for (int i = 0; i < xmlTableDataGrid.Items.Count; i++)
                {
                    Resource table = (Resource)xmlTableDataGrid.Items[i];
                    Resource list = resourceList[i];

                    if (table.Name != list.Name)
                    {
                        DateTime dateTime = DateTime.Now.ToUniversalTime();                      
                        table.DateChange = dateTime;
                        list.DateChange = dateTime;
                        xmlTableDataGrid.Items.Refresh();
                        
                       
                    }
                }

                
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

            xmlTableDataGrid.CommitEdit();
            xmlTableDataGrid.CommitEdit();


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
           var row = xmlTableDataGrid.SelectedItem;
        }


        private void xmlTableDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            checkForChanges();
            

        }
    }
}
