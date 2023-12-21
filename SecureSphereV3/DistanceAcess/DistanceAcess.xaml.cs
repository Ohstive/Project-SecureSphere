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
using SecureSphereV2.ViewModel;

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
            
                       
            
        }

       

        

        

       

        

        

    }
}
