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
                switch (selectedItemTag)
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
}

