using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_1._0.Model;
using Project_1._0.ViewModel.Services;
using Project_1._0.ViewModel;

namespace Project_1._0.View
{
    internal class JobView
    {
        // This class is used to display the configuration of a job
        private readonly JobConfiguration _jobConfiguration;

        // Constructor
        public JobView(JobConfiguration jobConfiguration)
        {
            _jobConfiguration = jobConfiguration;
        }

        // Display the configuration of a job
        public void DisplayJobConfiguration()
        {
            
        }

        static void ShowLogo()
        {
            Console.WriteLine("                           ::::::::    ::::::::::   ::::::::    :::    :::   :::::::::    ::::::::::   ::::::::    :::::::::    :::    :::   ::::::::::   :::::::::    :::::::::: ");
            Console.WriteLine("                         :+:    :+:   :+:         :+:    :+:   :+:    :+:   :+:    :+:   :+:         :+:    :+:   :+:    :+:   :+:    :+:   :+:          :+:    :+:   :+:         ");
            Console.WriteLine("                        +:+          +:+         +:+          +:+    +:+   +:+    +:+   +:+         +:+          +:+    +:+   +:+    +:+   +:+          +:+    +:+   +:+          ");
            Console.WriteLine("                       +#++:++#++   +#++:++#    +#+          +#+    +:+   +#++:++#:    +#++:++#    +#++:++#++   +#++:++#+    +#++:++#++   +#++:++#     +#++:++#:    +#++:++#      ");
            Console.WriteLine("                             +#+   +#+         +#+          +#+    +#+   +#+    +#+   +#+                +#+   +#+          +#+    +#+   +#+          +#+    +#+   +#+           ");
            Console.WriteLine("                     #+#    #+#   #+#         #+#    #+#   #+#    #+#   #+#    #+#   #+#         #+#    #+#   #+#          #+#    #+#   #+#          #+#    #+#   #+#            ");
            Console.WriteLine("                     ########    ##########   ########     ########    ###    ###   ##########   ########    ###          ###    ###   ##########   ###    ###   ##########    ");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        static void ShowMenu()
        {
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║            SecureSphere         ║");
            Console.WriteLine("╚═════════════════════════════════╝");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1.Create backup jobs           ");
            Console.WriteLine("2. Show created backup jobs     ");
            Console.WriteLine("3. Logs Parameters              ");
            Console.WriteLine("4. Run a specific backup job");
            Console.WriteLine("5. Run all backup jobs");
            Console.WriteLine("6. Choose the Language");
            Console.WriteLine("7. Exit");
        }
    }
}
