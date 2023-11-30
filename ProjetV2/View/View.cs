using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetV2.View
{
    internal class ViewJob
    {
        public ViewJob()
        {
            ShowLogo();
        }
        List<BackupJob> backupJobs = new List<BackupJob>(); //Initialize the backup jobs list
        string logDirectory = ""; // Variable to store the chosen log directory

        public void testc()
        {
            List<BackupJob> backupJobs = new List<BackupJob>(); //Initialize the backup jobs list
            string logDirectory = ""; // Variable to store the chosen log directory


            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Create backup jobs");
                Console.WriteLine("2. Show created backup jobs");
                Console.WriteLine("3. Set log directory");
                Console.WriteLine("4. Run a specific backup job");
                Console.WriteLine("5. Run all backup jobs sequentially");
                Console.WriteLine("6. Show the log repertory");
                Console.WriteLine("7. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Prompt user for the number of backup jobs to create
                        Console.Write("Enter the number of backup jobs to create (between 1 and 5): ");
                        int numberOfJobs = int.Parse(Console.ReadLine());

                        // Validate the user input
                        if (numberOfJobs < 1 || numberOfJobs > 5)
                        {
                            Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                            break;
                        }

                        // Create the specified number of backup jobs
                        for (int i = 1; i <= numberOfJobs; i++)
                        {
                            Console.WriteLine($"Enter details for Backup Job {i}:");

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
                            Jobs newBackupJob = new Jobs(name, source, target, type);
                            backupJobs.Add(newBackupJob);

                            Console.WriteLine($"Backup Job {i} created: {newBackupJob}");
                            Console.WriteLine();
                        }
                        break;

                    case 2:
                        // Show created backup jobs
                        Console.WriteLine("Created Backup Jobs:");
                        foreach (var job in backupJobs)
                        {
                            Console.WriteLine(job);
                        }
                        Console.WriteLine();
                        break;

                    case 3:
                        // Set log directory
                        Console.Write("Enter the directory path for logs: ");
                        logDirectory = Console.ReadLine();
                        break;

                    case 4:
                        Console.WriteLine("Enter the index of the job to run:");
                        int jobIndex = int.Parse(Console.ReadLine());
                        RunBackupJob(backupJobs[jobIndex]);
                        break;

                    case 5:
                        foreach (var job in backupJobs)
                        {
                            RunBackupJob(job);
                        }
                        break;

                    case 7:
                        Environment.Exit(0);
                        break;

                    case 6:
                        //Show the log Directory
                        Console.WriteLine(logDirectory);
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

        
    }
    
}
