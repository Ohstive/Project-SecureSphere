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

namespace Client
{
    public partial class ClientParametersPage : Page
    {
        public ObservableCollection<string> Parameters { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ExtensionParameters { get; set; } = new ObservableCollection<string>();

        public ClientParametersPage()
        {
            InitializeComponent();
            DataContext = this;
        }

       

        

       

        
        
    }
}



