using Azure;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace AzureTest
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

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            ///Exemple 1
            AzureAuthentication azureAuthentication = new AzureAuthentication(
              ""
              , ""
              , ""
              );

            AzureStorage storage = new AzureStorage(azureAuthentication);

            using (var fileStream = File.OpenRead(txtPath.Text))
            {
                storage.Upload(fileStream);
            }

            /////Exemple 2
            //AzureStorage storage1 = new AzureStorage("","","");
            //using (var fileStream = File.OpenRead(txtPath.Text))
            //{
            //    string fileName = storage1.Upload(fileStream);
            //}


        }


        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txtPath.Text = openFileDialog.FileName;
        }
    }
}
