using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_1._0.Model;
using Project_1._0.ViewModel.Services;
using Project_1._0.ViewModel;
using Project_1._0.Model.Language;

namespace Project_1._0.View
{
    internal class JobView
    {
        // This class is used to display the configuration of a job
        private readonly JobManager _jobManager;
        Language language;
        string currentLanguage;
        Dictionary<string, string> dialogueDictionary;

        // Constructor
        public JobView(JobManager jobManager)
        {
            this._jobManager = jobManager;
            this.language = new Language();
            this.currentLanguage = language.GetCurrentLanguage();
            this.dialogueDictionary = language.GetDialogue(currentLanguage);
        }



        List<JobManager> backupJobs = new List<JobManager>(); //Initialize the backup jobs list

        string logDirectory = ""; // Variable to store the chosen log directory
        string JobModification = "";
        string JobsModification = "";

        public JobView()
        {
            ShowLogo();
            Launch();
        }

        public void Launch()
        {

            while (true)
            {
                Console.Clear();
                ShowLogo();
                ShowMenu();
                int choice = int.TryParse(Console.ReadLine(), out choice) ? choice : -1;

                switch (choice)
                {
                    case 1:
                        // Create backup jobs
                        CreateBackupJobs();
                        break;

                    case 2:
                        // Show created backup jobs
                        //ShowBackupJobs();
                        break;

                    case 3:
                        // 
                        //RunAllJobs();
                        break;



                    case 4:
                        // Show log parameters
                        //ShowLogParameters();
                        break;
                    case 5:


                    case 6:
                        //ChooseLanguage();
                        break;

                    case 7:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }


            }
        }

       
        private int GetIntegerInput(string prompt)
        {
            int result;
            return int.TryParse(GetInput(prompt), out result) ? result : -1;
        }

        private string GetInput(string prompt)
        {
            Console.WriteLine($"{prompt}: ");
            return Console.ReadLine();
        }


        private void CreateBackupJobs()
        {
            // Create backup jobs
            Console.Clear();
            ShowLogo();
            Console.WriteLine("Enter details for Backup Job:");
            // Define the variables to create a job
            string name = GetInput("Enter the name of the job");
            string source = GetInput("Enter the source directory of the job");
            string target = GetInput("Enter the target Directory");
            int typeJob = GetIntegerInput("Enter the type of the job : 0 for Full 1 for Differential");

            JobManager newBackupJob = new JobManager(new Jobs(name, source, target, typeJob));
            Console.WriteLine($"Backup Job created: {newBackupJob}");


            string answerModification = GetInput("Do you want to modify the previous Job? (y/n)");
            while (answerModification != "y" && answerModification != "n")
            {
                if (answerModification == "y")
                {
                    answerModification = "y";
                    ModifyBackupJob(newBackupJob);
                }
                else
                {
                    backupJobs.Add(newBackupJob);
                    answerModification = "n";
                    break;
                }
            }


            string answerAdd = GetInput("Do you want to add another Job? (y/n)");
            while (answerAdd != "y" && answerAdd != "n")
            {
                if (answerAdd == "y")
                {
                    answerAdd = "y";
                    CreateBackupJobs();
                }
                else
                {
                    answerAdd = "n";
                    backupJobs.Add(newBackupJob);
                    break;
                    
                }
            }






        }
        private void ModifyBackupJob(JobManager backupJob)
        {
            string nameM = GetInput("Name");
            string sourceM = GetInput("Source Directory");
            string targetM = GetInput("Target Directory");
            int typeJobM = GetIntegerInput("Type (0:Full/1:Differential)");
            JobManager newBackupJobM = new JobManager(new Jobs(nameM,sourceM,targetM,typeJobM)) ;
            backupJobs.Add(newBackupJobM);
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
            Console.WriteLine("║           SecureSphere          ║");
            Console.WriteLine("╚═════════════════════════════════╝");
            Console.WriteLine("║");
            Console.WriteLine("║ 1. Create backup jobs");
            Console.WriteLine("║ 2. Show created backup jobs");
            Console.WriteLine("║ 3. Logs Parameters");
            Console.WriteLine("║ 4. Run a specific backup job");
            Console.WriteLine("║ 5. Run all backup jobs");
            Console.WriteLine("║ 6. Choose the Language");
            Console.WriteLine("║ 7. Exit");
            Console.WriteLine("║");
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║           SecureSphere          ║");
            Console.WriteLine("╚═════════════════════════════════╝");

        }


    }
}


/*
       public void Launch() // Main menu
       {
           while (true)
           {
               ShowMenu();
               int choice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;

               switch (choice)
               {
                   case 1:
                   // Create backup jobs
                   while (answer != "n")
                   {
                       Console.Clear();
                       ShowLogo();
                       Console.WriteLine("Enter details for Backup Job:");

                       string name = GetInput("Name");
                       string source = GetInput("Source Directory");
                       string target = GetInput("Target Directory");
                       int typeJob = GetIntegerInput("Type (0:Full/1:Differential)");

                       JobManager newBackupJob = CreateBackupJob(name, source, target, typeJob);

                       Console.WriteLine($"Backup Job created: {newBackupJob}");
                       Console.WriteLine("Modify the previous Job? (y/n)");

                       JobModification = Console.ReadLine();
                       if (JobModification == "y")
                       {
                           ModifyBackupJob(newBackupJob);
                       }
                       else
                       {
                           backupJobs.Add(newBackupJob);
                       }

                       Console.WriteLine("Add another Job? (y/n)");
                       answer = Console.ReadLine();
                       Console.WriteLine();
                       Console.Clear();
                   }
               }
               break;

               case 2:
                   // Show created backup jobs
                   ShowBackupJobs();
                   break;

               case 3:
                   // Show log parameters
                   ShowLogParameters();
                   break;

               case 4:
                   RunSingleJob();
                   break;

               case 5:
                   RunAllJobs();
                   break;

               case 6:
                   ChooseLanguage();
                   break;

               case 7:
                   Environment.Exit(0);
                   break;

               default:
                   Console.WriteLine("Invalid choice");
                   break;
               }
           }
       }
       */

/*
private string GetInput(string prompt)
{
    Console.Write($"{prompt}: ");
    return Console.ReadLine();
}

private int GetIntegerInput(string prompt)
{
    int result;
    return int.TryParse(GetInput(prompt), out result) ? result : 0;
}

private JobManager CreateBackupJob(string name, string source, string target, int typeJob)
{
    Jobs newJob = new Jobs(name, source, target, typeJob);
    return new JobManager(newJob);
}

private void ModifyBackupJob(JobManager backupJob)
{
    Console.WriteLine(backupJob);
    string nameM = GetInput("Name");
    string sourceM = GetInput("Source Directory");
    string targetM = GetInput("Target Directory");
    int typeJobM = GetIntegerInput("Type (0:Full/1:Differential)");

    JobManager newBackupJobM = CreateBackupJob(nameM, sourceM, targetM, typeJobM);
    backupJobs.Add(newBackupJobM);
}

private void ShowBackupJobs()
{
    Console.Clear();
    ShowLogo();
    if (backupJobs.Count == 0)
    {
        Console.WriteLine("There are no Jobs");
        Console.WriteLine("Press any key to leave:");
        string key = Console.ReadLine();
        Console.Clear();
    }
    else
    {
        Console.WriteLine("Created Backup Jobs:");
        foreach (var job in backupJobs)
        {
            Console.WriteLine(job);
        }

        Console.WriteLine("Modify any Jobs? (y/n)");
        Console.WriteLine("");

        JobsModification = Console.ReadLine();
        if (JobsModification == "y") // Modify or delete jobs
        {
            ModifyOrDeleteJobs();
        }
    }
}

private void ModifyOrDeleteJobs()
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
            ModifyJob();
            break;

        case 2:
            DeleteJob();
            break;
    }
}

private void ModifyJob()
{
    Console.WriteLine("Enter the index that you want to modify:");
    int JobIndex = int.Parse(Console.ReadLine());
    string Newname = GetInput("Name");
    string Newsource = GetInput("Source Directory");
    string Newtarget = GetInput("Target Directory");
    int typeJob = GetIntegerInput("Type (0:Full/1:Differential)");

    JobManager NewBackupJob = CreateBackupJob(Newname, Newsource, Newtarget, typeJob);
    backupJobs[JobIndex - 1] = NewBackupJob;
}

private void DeleteJob()
{
    Console.WriteLine("Enter the index that you want to delete:");
    int JobIndex2 = int.Parse(Console.ReadLine());
    backupJobs.RemoveAt(JobIndex2 - 1);
}

private void ShowLogParameters()
{
    Console.WriteLine("1. Set Logs Repertory");
    Console.WriteLine("2. Show Logs Repertory");
    int logchoice = GetIntegerInput("Choice");
    switch (logchoice)
    {
        case 1:
            SetLogsRepertory();
            break;

        case 2:
            ShowLogsRepertory();
            break;
    }
}

private void SetLogsRepertory()
{
    Console.Clear();
    ShowLogo();
    Console.Write("Enter the directory path for logs: ");
    logDirectory = Console.ReadLine();
    Console.Clear();
}

private void ShowLogsRepertory()
{
    Console.Clear();
    ShowLogo();
    Console.WriteLine($"Your current Logs repertory is: {logDirectory}");
    Console.WriteLine("Press any key to leave:");
    Console.ReadLine();
    Console.Clear();
}

private void RunSingleJob()
{
    foreach (var job in backupJobs)
    {
        int index = backupJobs.IndexOf(job);
        Console.WriteLine($"{index + 1}. {job}");
    }
    Console.WriteLine("Enter the index of the job to run:");
    int jobIndex = int.Parse(Console.ReadLine());
    if (backupJobs.IndexOf(jobIndex).GetType() = "Full")


        }

private void RunAllJobs()
{
    foreach (var job in backupJobs)
    {
        // RunBackupJob(job);
    }
}

private void ChooseLanguage()
{
    Console.Clear();
    ShowLogo();
    Console.WriteLine("Choose the language / Choisir la langue:");
    Console.WriteLine("French(fr) or English(en) / Français(fr) ou Anglais(en):");
    string language = Console.ReadLine();

    if (language != "fr" && language != "en")
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
        Console.WriteLine("Yes");
    }
}
*/