using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_1._0.Model;
using Project_1._0.ViewModel.Services;
using Project_1._0.ViewModel;
using Project_1._0.Model.Language;
using System.Xml.Linq;

namespace Project_1._0.View
{
    internal class JobView
    {
        // This class is used to display the configuration of a job

        private Language language;
        string currentLanguage;
        Dictionary<string, string> dialogueDictionary;

        List<JobManager> backupJobs = new List<JobManager>(); //Initialize the backup jobs list

        string logDirectory = ""; // Variable to store the chosen log directory
        string JobModification = "";
        string JobsModification = "";

        // Constructor
        public JobView()
        {
            this.language = new Language();
            language.ChangeLanguage("en");
            this.currentLanguage = language.GetCurrentLanguage();
            this.dialogueDictionary = language.GetAllDialogue(currentLanguage);
            Console.WriteLine(language.GetDialogue("FullNameLanguage"));
            language.InitializeLanguage(@"C:\Users\nemgb\Source\Repos\Project-SecureSphere\Project 1.0\Model\Language\Language.json");
            
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
                Console.Write(language.GetDialogue("EnterAnswer"));
                int choice = int.TryParse(Console.ReadLine(), out choice) ? choice : -1;

                switch (choice)
                {
                    case 1:
                        // Create backup jobs
                        if (backupJobs.Count > 4)
                        {
                            Console.WriteLine(language.GetDialogue("Jobs limit"));
                            GetInput(language.GetDialogue("PressAnyKeyToLeave"));
                            break;
                        }
                        CreateBackupJobs();
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
                        //Run one specific job
                        ShowIndexBackupJobs();
                        Console.WriteLine(language.GetDialogue("JobIndexToRun"));
                        Console.Write(language.GetDialogue("EnterAnswer"));
                        int jobIndex = int.Parse(Console.ReadLine());
                        backupJobs[jobIndex-1].JobRun();
                        Console.WriteLine(language.GetDialogue("Job Done"));
                        GetInput(language.GetDialogue("PressAnyKeyToLeave"));
                        break;
                    
                    case 5:
                        //Run all the jobs sequentially
                        foreach (JobManager job in backupJobs)
                        {
                            job.JobRun();
                            
                        }
                        Console.WriteLine(language.GetDialogue("Job Done"));
                        GetInput(language.GetDialogue("PressAnyKeyToLeave"));
                        break;
               
                    case 6:
                        ChooseLanguage();
                        break;

                    case 7:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine(language.GetDialogue("InvalidChoice"));
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
            
            Console.WriteLine(language.GetDialogue("EnterBackupJobDetails"));
            // Define the variables to create a job
            Console.WriteLine(language.GetDialogue("Job Name"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            string name = Console.ReadLine();

            Console.WriteLine(language.GetDialogue("Job Source"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            string source = Console.ReadLine();

            Console.WriteLine(language.GetDialogue("Job Target"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            string target = Console.ReadLine();

            Console.WriteLine(language.GetDialogue("Job Type"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            int typeJob = int.TryParse(Console.ReadLine(), out typeJob) ? typeJob : -1;

            JobManager newBackupJob = new JobManager(new Jobs(name, source, target, typeJob));
            if(typeJob == 0)
            {
                Console.WriteLine($"{language.GetDialogue("BackupJobCreated")}: {language.GetDialogue("Name")}: {name} "+$"{ language.GetDialogue("Type")} : {language.GetDialogue("Full")}" +$"{language.GetDialogue("Source")}: "+source+ $" {language.GetDialogue("Target")}: "+target);
            }
            else
            {
                Console.WriteLine($"{language.GetDialogue("BackupJobCreated")}: {language.GetDialogue("Name")}: {name} " + $"{language.GetDialogue("Type")} : {language.GetDialogue("Differential")} " + $"{language.GetDialogue("Source")}: " + source + $" {language.GetDialogue("Target")}: " + target);
            }
           


            string answerModification = "";
            while (answerModification != language.GetDialogue("No"))
            {
                Console.WriteLine($"{language.GetDialogue("ModifyPreviousJob")} {language.GetDialogue("Yes")}/{language.GetDialogue("No")}");
                Console.Write(language.GetDialogue("EnterAnswer"));
                answerModification = Console.ReadLine();
                if (answerModification == language.GetDialogue("y"))
                {
                    answerModification = language.GetDialogue("y");
                    ModifyBackupJob(newBackupJob);
                    answerModification = language.GetDialogue("n");
                }   
                else
                {
                    backupJobs.Add(newBackupJob);
                    answerModification = language.GetDialogue("n");
                    break;
                }
            }


            string answerAdd = "";
            while (answerAdd != language.GetDialogue("No"))
            {
                Console.WriteLine($"{language.GetDialogue("AddAnotherJob")} {language.GetDialogue("Yes")}/{language.GetDialogue("No")}");
                Console.Write(language.GetDialogue("EnterAnswer"));
                answerAdd = Console.ReadLine();
                if (backupJobs.Count > 4)
                {
                    Console.WriteLine(  language.GetDialogue("Jobs limit")  );
                    GetInput(language.GetDialogue("PressAnyKeyToLeave"));
                    break;
                }
                else if (answerAdd == language.GetDialogue("y"))
                {
                    answerAdd = language.GetDialogue("y");
                    CreateBackupJobs();
                }
                break;
            }


        }

        private void ShowIndexBackupJobs()
        {
            foreach (JobManager job in backupJobs)
            {
                int index = backupJobs.IndexOf(job);
                string ModiType = "";
                if(job.Jobs.JobConfiguration.BackupType == 0)
                {
                    ModiType = language.GetDialogue("Full");
                }
                else
                {
                    ModiType = language.GetDialogue("Differential");
                }
                Console.WriteLine($"{index + 1}.{language.GetDialogue("Name")}:{job.Jobs.JobConfiguration.JobName}  " +
                    $"{language.GetDialogue("Source")}: {job.Jobs.JobConfiguration.SourceDirectoryPath} " +
                    $"{language.GetDialogue("Target")}: {job.Jobs.JobConfiguration.TargetDirectoryPath} " +
                    $"{language.GetDialogue("Type")}: {ModiType} ");
            }
        }
        private void ModifyBackupJob(JobManager backupJob)
        {
            Console.WriteLine(language.GetDialogue("Job Name"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            string nameM = Console.ReadLine();

            Console.WriteLine(language.GetDialogue("Job Source"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            string sourceM = Console.ReadLine();

            Console.WriteLine(language.GetDialogue("Job Target"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            string targetM = Console.ReadLine();

            Console.WriteLine(language.GetDialogue("Job Type"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            int typeJobM = int.TryParse(Console.ReadLine(), out typeJobM) ? typeJobM : -1;

            JobManager newBackupJobM = new JobManager(new Jobs(nameM,sourceM,targetM,typeJobM)) ;
            backupJobs.Add(newBackupJobM);
        }

        private void ShowBackupJobs()
        {
            Console.Clear();
            ShowLogo();
            if (backupJobs.Count == 0)
            {
                Console.WriteLine(language.GetDialogue("NoJobsMessage"));
                GetInput(language.GetDialogue("PressAnyKeyToLeave"));
                Console.Clear();
            }
            else
            {
                Console.WriteLine(language.GetDialogue("BackupJobCreated"));
                ShowIndexBackupJobs();


                Console.WriteLine($"{language.GetDialogue("ModifyAnyJobs")} {language.GetDialogue("Yes")}/{language.GetDialogue("No")}");
                Console.Write(language.GetDialogue("EnterAnswer"));

                JobsModification = Console.ReadLine();
                if (JobsModification == language.GetDialogue("y")) // Modify or delete jobs
                {
                    ModifyOrDeleteJobs();
                }
            }
        }
        private void ModifyOrDeleteJobs()
        {
            ShowIndexBackupJobs();
            Console.WriteLine("");
            Console.WriteLine($"1. {language.GetDialogue("ModifyOneJob")}");
            Console.WriteLine($"2. {language.GetDialogue("DeleteOneJob")}");
            Console.Write(language.GetDialogue("EnterAnswer"));
            int Mchoice = int.TryParse(Console.ReadLine(), out Mchoice) ? Mchoice : -1;
            switch (Mchoice)
            {
                case 1:
                    Console.WriteLine(language.GetDialogue("EnterIndexToModify"));
                    Console.Write(language.GetDialogue("EnterAnswer"));
                    int Indexchoice = int.TryParse(Console.ReadLine(), out Indexchoice) ? Indexchoice : -1;

                    Console.WriteLine(language.GetDialogue("Job Name"));
                    Console.Write(language.GetDialogue("EnterAnswer"));
                    string nameM = Console.ReadLine();

                    Console.WriteLine();
                    Console.Write(language.GetDialogue("EnterAnswer"));
                    string sourceM = Console.ReadLine();

                    Console.WriteLine();
                    Console.Write(language.GetDialogue("EnterAnswer"));
                    string targetM = Console.ReadLine();

                    Console.WriteLine();
                    Console.Write(language.GetDialogue("EnterAnswer"));
                    int typeJobM = int.TryParse(Console.ReadLine(), out typeJobM) ? typeJobM : -1;

                    JobManager newBackupJobM = new JobManager(new Jobs(nameM, sourceM, targetM, typeJobM));
                    backupJobs[Indexchoice-1] = newBackupJobM;
                    break;

                case 2:
                    DeleteJob();
                    break;
            }
        }
        private void DeleteJob()
        {
            Console.WriteLine(language.GetDialogue("EnterIndexToDelete"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            int JobIndex2 = int.TryParse(Console.ReadLine(), out JobIndex2) ? JobIndex2 : -1;
            backupJobs.RemoveAt(JobIndex2 - 1);
        }

        private void ShowLogParameters()
        {
            Console.WriteLine($"1. {language.GetDialogue("SetLogsRepertory")}");
            Console.WriteLine($"2. {language.GetDialogue("ShowLogsRepertory")}");
            Console.Write(language.GetDialogue("EnterAnswer"));
            int logchoice = int.TryParse(Console.ReadLine(), out logchoice) ? logchoice : -1;
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
            Console.Write(language.GetDialogue("ShowLogsRepertory"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            logDirectory = Console.ReadLine();
            Console.Clear();
        }

        private void ShowLogsRepertory()
        {
            Console.Clear();
            ShowLogo();
            Console.WriteLine($"{language.GetDialogue("CurrentLogsRepertory")}: {logDirectory}");
            GetInput(language.GetDialogue("PressAnyKeyToLeave"));
            Console.Clear();
        }

        private void ChooseLanguage()
        {
            Console.Clear();
            ShowLogo();

            Console.WriteLine(language.GetDialogue("ChooseLanguage"));
            Console.WriteLine(language.GetDialogue("InvalidLanguageChoice"));
            Console.Write(language.GetDialogue("EnterAnswer"));
            string languageM = Console.ReadLine();

            if (languageM != "fr" && languageM != "en")
            {
                Console.WriteLine(language.GetDialogue("InvalidLanguageChoice"));
                GetInput(language.GetDialogue("PressAnyKeyToLeave"));

            }
            language.ChangeLanguage(languageM);


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

        private void ShowMenu()
        {
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║           SecureSphere          ║");
            Console.WriteLine("╚═════════════════════════════════╝");
            Console.WriteLine("║");
            Console.WriteLine($"║ 1. {language.GetDialogue("CreateJobMenu")}");
            Console.WriteLine($"║ 2. {language.GetDialogue("ShowJobMenu")}");
            Console.WriteLine($"║ 3. {language.GetDialogue("LogsMenu")}");
            Console.WriteLine($"║ 4. {language.GetDialogue("RunOneJobMenu")}");
            Console.WriteLine($"║ 5. {language.GetDialogue("RunAllJobsMenu")}");
            Console.WriteLine($"║ 6. {language.GetDialogue("ChooseLanguageMenu")}");
            Console.WriteLine($"║ 7. {language.GetDialogue("ExitMenu")}");
            Console.WriteLine("║");
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║           SecureSphere          ║");
            Console.WriteLine("╚═════════════════════════════════╝");

        }


    }
}


  