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
using System.Net.Sockets;
using System.IO;
using System.Net;
using SecureSphereV2.View;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.ComponentModel;
using SecureSphereV2.Model;
using System.Collections.ObjectModel;


namespace SecureSphereV2.D
{

    
    /// <summary>
    /// Logique d'interaction pour DistanceAcess.xaml
    /// </summary>
    public partial class DistanceAcess : Page
    {
        private SharedDataService sharedDataService;
        public string SavedPassword;


        public DistanceAcess(SharedDataService sharedDataService)
        {
            InitializeComponent();
            this.sharedDataService = sharedDataService;
            this.SavedPassword = sharedDataService.SavedPassword;
                       
            UpdatePasswordVisibility();
        }

       

        

        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePasswordVisibility();
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdatePasswordVisibility();
        }

        private void UpdatePasswordVisibility()
        {
            if (showPasswordCheckBox.IsChecked == true)
            {
                passwordTextBox.Visibility = Visibility.Visible;
                passwordTextBox.Focus();
                passwordBox.Visibility = Visibility.Collapsed;
                passwordTextBox.Text = passwordBox.Password; // Copiez le mot de passe du PasswordBox dans la TextBox
            }
            else
            {
                passwordTextBox.Visibility = Visibility.Collapsed;
                passwordBox.Visibility = Visibility.Visible;
                passwordBox.Password = passwordTextBox.Text; // Copiez le mot de passe de la TextBox dans le PasswordBox
            }

            // Enregistrez le mot de passe dans la variable statique
            
        }

        private void SavePassword()
        {
            if (showPasswordCheckBox.IsChecked == true)
            {
                passwordBox.Password = passwordTextBox.Text ;
                sharedDataService.SavedPassword = passwordBox.Password ;
                
            }
            else
            {
                sharedDataService.SavedPassword = passwordBox.Password;
            }
                // Sauvegarder la valeur du mot de passe dans la classe statique

            
        }

        private void SavePassword_Click(object sender, RoutedEventArgs e)
        {
            SavePassword();
            System.Windows.MessageBox.Show("Password Saved.");
        }

    }
}
