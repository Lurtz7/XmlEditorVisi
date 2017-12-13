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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using XmlEditor.Core;


namespace XmlEditor
{
    /// <summary>
    /// Interaction logic for ChooseSourceWindow.xaml
    /// </summary>
    public partial class ChooseSourceWindow : Window
    {
        public ChooseSourceWindow()
        {
            InitializeComponent();
        }
      

        private void localFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "xml files(*.xml)|*.xml|All files (*.*)|*.*";
            bool? result = openFile.ShowDialog();
            if (result == true)
            {
               
                string filename = openFile.FileName;
                var mainWindow = new MainWindow(filename);
                mainWindow.FileName = filename;
                

                mainWindow.Show();
            }

        }
    }
}
