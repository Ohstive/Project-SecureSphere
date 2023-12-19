using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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



namespace SecureSphereV2.View
{
    public partial class ParametersPage : Page
    {
        // Assuming Parameters is a property in your view model
        public ObservableCollection<string> ExtensionParameters { get; set; } = new ObservableCollection<string>();

        public ParametersPage(SharedDataService sharedDataService)
        {
            InitializeComponent();

            // Set the DataContext to the instance of your view model or data source
            DataContext = this; // Assuming Parameters is a property in your view model
        }

        private void AddExtensionButton_Click(object sender, RoutedEventArgs e)
        {
            string newString = stringTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(newString))
            {
                ExtensionParameters.Add(newString.ToLower());
                stringTextBox.Clear();
            }
        }

        private void DeleteExtensionButton_Click(object sender, RoutedEventArgs e)
        {
            if (stringListBox.SelectedItem != null)
            {
                ExtensionParameters.Remove(stringListBox.SelectedItem.ToString());
            }
        }
    }
}



