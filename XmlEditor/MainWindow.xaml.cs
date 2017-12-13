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
        Resource resource = new Resource();
        ResourceList resourceList;


        public MainWindow(string fileName)
        {
            InitializeComponent();

            FileName = fileName;
            this.Title = FileName;
            this.Loaded += MainWindow_Loaded;
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





        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            var resources = LocalFileRepository.GetXmlFile(FileName);
            resourceList = new ResourceList(resources);
            xmlTableDataGrid.ItemsSource = resourceList;



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

            bool isSaved = repository.SaveXmlFile(FileName, resourceList);
            if (isSaved)
            {
                saveStatusBarMsg.Text = $"Last saved: {DateTime.Now}";

            }
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


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            xmlTableDataGrid.CommitEdit();
            xmlTableDataGrid.CommitEdit();
            string messageBoxText = "Do you want to save changes?";
            string caption = "XmlEditor 1.0";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            var originalList = LocalFileRepository.GetXmlFile(FileName);


            for (int i = 0; i < xmlTableDataGrid.Items.Count; i++)
            {
                var tableItems = xmlTableDataGrid.Items[i];

                if (tableItems.GetType() == originalList[0].GetType())
                {
                    Resource table = (Resource)xmlTableDataGrid.Items[i];
                    Resource list = originalList[i];



                    if (list.DateChange != table.DateChange)
                    {
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                List<Resource> resourceList = AddToListForSave();
                                repository.SaveXmlFile(FileName, resourceList);

                                break;
                            case MessageBoxResult.No:
                                e.Cancel = false;
                                break;
                            case MessageBoxResult.Cancel:
                                e.Cancel = true;
                                break;
                        }
                        break;
                    }
                }
            }
        }


    }
}
