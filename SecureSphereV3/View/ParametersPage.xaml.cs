using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;
using SecureSphereV2.ViewModel;


namespace SecureSphereV2.View
{
    public partial class ParametersPage : Page
    {



        private SharedDataService sharedDataService;

        public ParametersPage(SharedDataService sharedDataService)
        {
            InitializeComponent();
            this.sharedDataService = sharedDataService;
            DataContext = this.sharedDataService;
        }

        private void AddButtonExtensionCrypted_Click(object sender, RoutedEventArgs e)
        {
            string newString = stringTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(newString))
            {
                if(sharedDataService.ExtensionCrypted.Contains(newString.ToLower()))
                {
                    System.Windows.MessageBox.Show("This extension is already in the list.");
                    return;
                }
                else
                {
                    sharedDataService.ExtensionCrypted.Add(newString.ToLower());
                    stringTextBox.Clear();
                }
            }
        }
     
        private void DeleteButtonExtensionCrypted_Click(object sender, RoutedEventArgs e)
        {
            if (ParametersListBox.SelectedItem != null)
            {
                sharedDataService.ExtensionCrypted.Remove(ParametersListBox.SelectedItem.ToString());
            }
        }

        private void DeleteAllButtonExtensionCrypted_Click(object sender, RoutedEventArgs e)
        {
            sharedDataService.ExtensionCrypted.Clear();
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button deleteButton = (System.Windows.Controls.Button)sender;
            string itemToDelete = (string)deleteButton.DataContext;

            if (!string.IsNullOrEmpty(itemToDelete))
            {
                sharedDataService.ExtensionCrypted.Remove(itemToDelete);
            }
        }





        private void AddButtonExtensionPriority_Click(object sender, RoutedEventArgs e)
        {
            string newString = extensionTextBox.Text.Trim();



            if (!string.IsNullOrEmpty(newString))
            {
                if (sharedDataService.ExtensionPriority.Contains(newString.ToLower()))
                {
                    System.Windows.MessageBox.Show("This extension is already in the list.");
                    return;
                }
                else
                {
                    sharedDataService.ExtensionPriority.Add(newString.ToLower());
                    extensionTextBox.Clear();

                }
            }
        }

        private void DeletePriorityItemButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button deleteButton = (System.Windows.Controls.Button)sender;
            string itemToDelete = (string)deleteButton.DataContext;

            if (!string.IsNullOrEmpty(itemToDelete))
            {
                sharedDataService.ExtensionPriority.Remove(itemToDelete);
            }
        }

      

        private void DeleteAllButtonExtensionPriority_Click(object sender, RoutedEventArgs e)
        {
            sharedDataService.ExtensionPriority.Clear();
        }

        private void DeleteButtonExtensionPriority_Click(object sender, RoutedEventArgs e)
        {
            if (ParametersListBox.SelectedItem != null)
            {
                sharedDataService.ExtensionPriority.Remove(ParametersListBox.SelectedItem.ToString());
            }
        }


    }
}



