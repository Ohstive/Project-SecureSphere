using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409


namespace InterfaceSecureSphere
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            NavView.ItemInvoked += NavView_ItemInvoked;

            // Set the default selected menu item
            NavView.SelectedItem = NavView.MenuItems[0];

            // Navigate to the default page
            NavigateToDefaultPage();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                // Handle settings invocation
            }
            else
            {
                // Handle regular item invocation
                string selectedItemTag = args.InvokedItemContainer.Tag.ToString();
                NavigateToPage(selectedItemTag);
            }
        }

        private void NavigateToDefaultPage()
        {
            // Navigate to the default page (adjust this based on your default page)
            ContentFrame.Navigate(typeof(JobMenuPage));
        }

        private void NavigateToPage(string pageTag)
        {
            switch (pageTag)
            {
                case "JobMenu":
                    ContentFrame.Navigate(typeof(JobMenuPage));
                    break;
                case "LogsMenu":
                    ContentFrame.Navigate(typeof(LogsMenuPage));
                    break;
                    // Add more cases as needed
            }
        }
    }
}
