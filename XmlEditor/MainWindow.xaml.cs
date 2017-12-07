using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        static ObservableCollection<Resource> resourceList = new ObservableCollection<Resource>();
        Resource resource = new Resource();


        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            resourceList.CollectionChanged += ResourceList_CollectionChanged;


        }

        private void ResourceList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ObservableCollection<Resource> senderItem = (ObservableCollection<XmlEditor.Core.Resource>)sender;

                if (senderItem[senderItem.Count -1].DateChange == null)
                {
                    
                    
                        string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                        senderItem[senderItem.Count - 1].DateChange = dateTime;

                    
                }

            }

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            var resources = repository.GetXmlFile(FileName);


            resourceList.Clear();

            foreach (var item in resources)
            {
                resourceList.Add(new Resource
                {
                    Name = item.Name,
                    Language = item.Language,
                    DateChange = item.DateChange,
                    GenericKey = item.GenericKey,
                    ResourceData = item.ResourceData,
                    Tenant = item.Tenant

                });
            }
            xmlTableDataGrid.ItemsSource = resourceList;
        }

        private void checkForChanges()
        {
            if (xmlTableDataGrid.Items.Count > 0 && (xmlTableDataGrid.Items.Count == resourceList.Count))
            {

                for (int i = 0; i < xmlTableDataGrid.Items.Count; i++)
                {
                    Resource table = (Resource)xmlTableDataGrid.Items[i];
                    Resource list = resourceList[i];


                    if (table.Name != list.Name || table.ResourceData != list.ResourceData)
                    {

                        string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                        table.DateChange = dateTime;
                        list.DateChange = dateTime;
                        list.Name = table.Name;
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
            foreach (var item in xmlTableDataGrid.Items)
            {
                if (item.GetType() == resource.GetType())
                {
                    Resource savedItem = (Resource)item;

                    resourceList.Add(savedItem);

                }
            }


            repository.SaveXmlFile(FileName, resourceList);
            saveStatusBarMsg.Text = $"Last saved: {DateTime.UtcNow}";

        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

            var deletedRow = xmlTableDataGrid.SelectedItem;
            if (deletedRow.GetType() == resource.GetType())
            {
                Resource removedRow = (Resource)deletedRow;
                resourceList.Remove(removedRow);

            }




        }


        private void xmlTableDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {

            checkForChanges();
            var ss= xmlTableDataGrid.Items[xmlTableDataGrid.Items.Count];
        }

        private void xmlTableDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if (e.Column.Header.ToString() == "DateChange")
            {

                e.Column.IsReadOnly = true;
            }
         }
    }
}
