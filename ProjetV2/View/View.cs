using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetV2.Model;
using ProjetV2.ViewModel;

namespace ProjetV2.View
{
    internal class ViewJob
    {
        public ViewJob()
        {
            ShowLogo();
        }
        List<JobManager> backupJobs = new List<JobManager>(); //Initialize the backup jobs list
        string logDirectory = ""; // Variable to store the chosen log directory
        string answer = "";
        string JobModification = "";
        string JobsModification = "";

        public void testc()
        { 
            while (true)
            {
                ShowMenu();
                int choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;
       
                switch (choice)
                {
                    case 1:
                        // Create the specified number of backup jobs
                    while (answer != "n")
                    {
                        Console.Clear();
                        ShowLogo();
                        Console.WriteLine($"Enter details for Backup Job:");

                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Source Directory: ");
                        string source = Console.ReadLine();

                        Console.Write("Target Directory: ");
                        string target = Console.ReadLine();

                        Console.Write("Type (Full/Differential): ");
                        BackupType type;
                        Enum.TryParse(Console.ReadLine(), true, out type);

                        // Create and add backup job to the list
                        BackupJob newBackupJob = new BackupJob(name, source, target, type);
                        

                        Console.WriteLine($"Backup Job created: {newBackupJob}");
                        Console.WriteLine("Modify the previous Job ? (y/n)");
                        modification = Console.ReadLine();
                        if (modification == "y")
                        {
                            Console.WriteLine(newBackupJob);
                            Console.Write("Name: ");
                            string nameM = Console.ReadLine();

                            Console.Write("Source Directory: ");
                            string sourceM = Console.ReadLine();

                            Console.Write("Target Directory: ");
                            string targetM = Console.ReadLine();

                            Console.Write("Type (Full/Differential): ");

                            Enum.TryParse(Console.ReadLine(), true, out type);
                            BackupJob newBackupJobM = new BackupJob(nameM, sourceM, targetM, type);
                            backupJobs.Add(newBackupJobM);

                        }
                        else
                        {

                            backupJobs.Add(newBackupJob);                        
                        }
                        Console.WriteLine("Add an another Job ? (y/n)");
                        answer = Console.ReadLine();
                        Console.WriteLine();
                        Console.Clear();
                    }
                        break;

                    case 2: 
                    // Show created backup jobs
                    
                        Console.Clear();
                        ShowLogo();
                        if (backupJobs.Count == 0)
                        {
                            Console.WriteLine("There is no Jobs");
                            Console.WriteLine("Press any keybind to leave:");
                            string key = Console.ReadLine();
                        }
                        else
                        {

                            Console.WriteLine("Created Backup Jobs:");
                            foreach (var job in backupJobs)
                            {
                                Console.WriteLine(job);
                            }
                            
                            Console.WriteLine("Modify any Jobs ? (y/n)");
                            Console.WriteLine("");

                            JobsModification = Console.ReadLine();
                            if (JobsModification == "y")//Modify or delete jobs
                        {
                                foreach (var job in backupJobs)
                                {
                                    int index = backupJobs.IndexOf(job);
                                    Console.WriteLine($"{index + 1}. {job}");
                                }
                                Console.WriteLine("");
                                Console.WriteLine("1. To modify one Job");
                                Console.WriteLine("2. To delete one Job");
                                int Mchoice = int.Parse(Console.ReadLine());
                                switch (Mchoice)
                                {
                                    case 1:
                                        Console.WriteLine("Enter the index that you want to modify:");
                                        int JobIndex = int.Parse(Console.ReadLine());
                                        Console.Write("Name: ");
                                        string Newname = Console.ReadLine();

                                        Console.Write("Source Directory: ");
                                        string Newsource = Console.ReadLine();

                                        Console.Write("Target Directory: ");
                                        string Newtarget = Console.ReadLine();

                                        Console.Write("Type (Full/Differential): ");
                                        BackupType Newtype;
                                        Enum.TryParse(Console.ReadLine(), true, out Newtype);
                                        BackupJob NewBackupJob = new BackupJob(Newname, Newsource, Newtarget, Newtype);
                                        backupJobs[JobIndex - 1] = NewBackupJob;
                                        break;

                                    case 2:
                                        Console.WriteLine("Enter the index that you want to delete:");
                                        int JobIndex2 = int.Parse(Console.ReadLine());
                                        backupJobs.RemoveAt(JobIndex2 - 1);
                                        break;
                                }

                            }
                            else
                            {

                            }

                        }
                    break;
 

                    case 3:
                        // Show log parameters
                        Console.WriteLine("1. Set Logs Repertory");
                        Console.WriteLine("2. Show Logs Repertory");
                        int logchoice = int.TryParse(Console.ReadLine(), out logchoice) ? logchoice : 0;
                        switch (logchoice)
                        {
                            case 1:
                                Console.Clear();
                                ShowLogo();
                                Console.Write("Enter the directory path for logs: ");
                                logDirectory = Console.ReadLine();
                                Console.Clear();
                                break;
                            case 2:
                                Console.Clear();
                                ShowLogo();
                                Console.WriteLine($"Your current Logs repertory is: {logDirectory}");
                                Console.WriteLine("Press any keybind to leave:");
                                DisplayBackup = Console.ReadLine();
                                Console.Clear();
                                break;
                        }
                        break;

                        
                        
                        break;

                    case 4:
                        Console.WriteLine("Enter the index of the job to run:");
                        int jobIndex = int.Parse(Console.ReadLine());
                        //RunBackupJob(backupJobs[jobIndex]);
                        break;

                    case 5:
                        foreach (var job in backupJobs)
                        {
                            //RunBackupJob(job);
                        }
                        break;

                    case 7:
                        Environment.Exit(0);
                        break;

                    case 6:
                        Console.Clear();
                        ShowLogo();
                        Console.WriteLine("Choose the language / Choisir la langue:");
                        Console.WriteLine("French(fr) or English(en) / Français(fr) ou Englais(en):");
                        string language = Console.ReadLine();
                        if (language != "fr" || language != "en")
                        {
                        
                            Console.WriteLine("Please choose between French(fr) or English(en)");
                            language = Console.ReadLine();
                        }
                        else if (language == "fr")
                        {
                            Console.WriteLine("Oui baguette");
                        }
                        else if (language == "en")
                        {
                            Console.WriteLine("Yes")
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
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
            Console.WriteLine("╔═══════════SecureSphere══════════╗");
            Console.WriteLine("║                                 ║");
            Console.WriteLine("║      Choose an option:          ║");
            Console.WriteLine("║ 1. Create backup jobs           ║");
            Console.WriteLine("║ 2. Show created backup jobs     ║");
            Console.WriteLine("║ 3. Logs Parameters              ║");
            Console.WriteLine("║ 4. Run a specific backup job    ║");
            Console.WriteLine("║ 5. Run all backup jobs          ║");
            Console.WriteLine("║    sequentially                 ║");
            Console.WriteLine("║ 6. Choose the Language          ║");
            Console.WriteLine("║ 7. Exit                         ║");
            Console.WriteLine("╚═════════════════════════════════╝");


        }
    }
    
}
