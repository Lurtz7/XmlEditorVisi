using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
        //static ObservableCollection<Resource> resourceList = new ObservableCollection<Resource>();
        Resource resource = new Resource();
        ResourceList resourceList;

        public MainWindow(string fileName)
        {
            InitializeComponent();

            FileName = fileName;
            

            this.Loaded += MainWindow_Loaded;
            //resourceList.CollectionChanged += ResourceList_CollectionChanged;
            xmlTableDataGrid.Loaded += SetMinWidths;
        }
        public void SetMinWidths(object source, EventArgs e)
        {

            foreach (var column in xmlTableDataGrid.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            }
        }

        private void ResourceList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                ObservableCollection<Resource> senderItem = (ObservableCollection<Resource>)sender;


                var index = e.NewStartingIndex;
                string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                senderItem[index].DateChange = dateTime;



            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ObservableCollection<Resource> senderItem = (ObservableCollection<Resource>)sender;

                if (senderItem[senderItem.Count - 1].DateChange == null)
                {


                    string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                    senderItem[senderItem.Count - 1].DateChange = dateTime;


                }

            }

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var resources = LocalFileRepository.GetXmlFile(FileName);
            resourceList = new ResourceList(resources);
            xmlTableDataGrid.ItemsSource = resourceList;
            //var resources = repository.GetXmlFile(FileName);

            //resourceList.Clear();

            //foreach (var item in resources)
            //{
            //    resourceList.Add(new Resource
            //    {
            //        Name = item.Name,
            //        Language = item.Language,
            //        DateChange = item.DateChange,
            //        GenericKey = item.GenericKey,
            //        ResourceData = item.ResourceData,
            //        Tenant = item.Tenant
            //    });
            //}
            //IsValid isValid = new IsValid();
            //isValid.ValidList(resourceList);
            //xmlTableDataGrid.ItemsSource = resourceList;
        }

        private void checkForChanges(int index)
        {
            Resource table = (Resource)xmlTableDataGrid.Items[index];
            Resource list = (Resource)resourceList[index];

            if (table != list)
            {
                string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                table.DateChange = dateTime;
                list.DateChange = dateTime;
            }
        }

        private List<Resource> AddToListForSave()
        {
            List<Resource> resourceList = new List<Resource>();
            foreach (var item in xmlTableDataGrid.Items)
            {
                if (item.GetType() == resource.GetType())
                {
                    Resource savedItem = (Resource)item;

                    resourceList.Add(savedItem);

                }
            }
            return resourceList;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            xmlTableDataGrid.CommitEdit();
            xmlTableDataGrid.CommitEdit();

            List<Resource> resourceList = AddToListForSave();

            repository.SaveXmlFile(FileName, resourceList);
            saveStatusBarMsg.Text = $"Last saved: {DateTime.UtcNow}";
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


        private void xmlTableDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //if (e.Column.Header.ToString() == "Language")
            //{
            //    Binding bindingLanguage = new Binding("Language");
            //    bindingLanguage.ValidatesOnExceptions = true;

            //    //xmlTableDataGrid.RowValidationErrorTemplate
            //    //this.Style = Resources["errorStyle"] as Style;
            //    //this.Style = (Style)FindResource("errorStyle");
                
            //    //resourceList.SetResourceReference(Control.StyleProperty, "errorStyle");
            //    //SetResourceReference(Control.StyleProperty, "errorStyle");
            //    //DataGrid dataGrid = new DataGrid();
            //    //dataGrid.SetResourceReference(Control.StyleProperty, "errorStyle");
            //    //    OfType<DataGridTextColumn>();

            //    //EditingElementStyle = "{StaticResource errorStyle}"
            //}
            //if (e.Column.Header.ToString() == "GenericKey")
            //{
            //    e.Column.IsReadOnly = true;
            //}
            //if (e.Column.Header.ToString() == "Tenant")
            //{

            //    e.Column.IsReadOnly = true;
            //}

            //if (e.Column.Header.ToString() == "DateChange")
            //{

            //    e.Column.IsReadOnly = true;
            //}       

            
        }

      
        private void xmlTableDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Resource senderItem = (Resource)e.Row.Item;

            int index = e.Row.GetIndex();
            string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");            
            resourceList[index].DateChange = dateTime;

            resourceList.Add(senderItem);
            resourceList.Remove(resourceList[resourceList.Count - 1]);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        { }
            //    //xmlTableDataGrid.CommitEdit();
            //    //xmlTableDataGrid.CommitEdit();
            //    string messageBoxText = "Do you want to save changes?";
            //    string caption = "XmlEditor 1.0";
            //    MessageBoxButton button = MessageBoxButton.YesNoCancel;
            //    MessageBoxImage icon = MessageBoxImage.Warning;
            //    var originalList = repository.GetXmlFile(FileName);


            //    for (int i = 0; i < xmlTableDataGrid.Items.Count; i++)
            //    {
            //        var tableItems = xmlTableDataGrid.Items[i];

            //        if (tableItems.GetType() == originalList[0].GetType())
            //        {
            //            Resource table = (Resource)xmlTableDataGrid.Items[i];
            //            Resource list = originalList[i];



            //            if (list.DateChange != table.DateChange)
            //            {
            //                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            //                switch (result)
            //                {
            //                    case MessageBoxResult.Yes:
            //                        List<Resource> resourceList = AddToListForSave();
            //                        repository.SaveXmlFile(FileName, resourceList);

            //                        break;
            //                    case MessageBoxResult.No:
            //                        e.Cancel = false;
            //                        break;
            //                    case MessageBoxResult.Cancel:
            //                        e.Cancel = true;
            //                        break;
            //                }
            //                break;
            //            }
            //        }
            //    }
            //}
        }
}
