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



using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SecureSphereV2.ViewModel;

namespace SecureSphereV2.View
{
    public partial class ParametersPage : Page
    {
        public ObservableCollection<string> Parameters { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ExtensionParameters { get; set; } = new ObservableCollection<string>();

        public ParametersPage(SharedDataService sharedDataService)
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string newString = stringTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(newString))
            {
                Parameters.Add(newString.ToLower());
                stringTextBox.Clear();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ParametersListBox.SelectedItem != null)
            {
                Parameters.Remove(ParametersListBox.SelectedItem.ToString());
            }
        }

        private void AddExtensionButton_Click(object sender, RoutedEventArgs e)
        {
            string newString = extensionTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(newString))
            {
                ExtensionParameters.Add(newString.ToLower());
                extensionTextBox.Clear();
            }
        }

        private void DeleteExtensionButton_Click(object sender, RoutedEventArgs e)
        {
            if (PriorityExtensionListBox.SelectedItem != null)
            {
                ExtensionParameters.Remove(PriorityExtensionListBox.SelectedItem.ToString());
            }
        }

        private void DeleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            Parameters.Clear();
        }

        private void DeleteAllExtensionButton_Click(object sender, RoutedEventArgs e)
        {
            ExtensionParameters.Clear();
        }
    }
}



