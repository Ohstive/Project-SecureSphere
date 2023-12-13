using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms; // Add this using directive

namespace SecureSphereV2.View
{
    public partial class JobForm : Window
    {
        public JobForm()
        {
            InitializeComponent();
        }

        private void BrowseSourceButton_Click(object sender, RoutedEventArgs e)
        {
            // Use Windows Forms FolderBrowserDialog
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    SourceTextBox.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void BrowseTargetButton_Click(object sender, RoutedEventArgs e)
        {
            // Use Windows Forms FolderBrowserDialog
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    TargetTextBox.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void MyEncryptionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            EncryptionKeyTextBlock.Visibility = Visibility.Visible;
            EncryptionKeyTextBox.Visibility = Visibility.Visible;
            EncryptionKeyTextBox.IsReadOnly = false;
        }

        private void MyEncryptionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            EncryptionKeyTextBlock.Visibility = Visibility.Collapsed;
            EncryptionKeyTextBox.Visibility = Visibility.Collapsed;
            EncryptionKeyTextBox.IsReadOnly = true;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the Submit Button Click event logic here
        }
    }
}



