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
                        int jobIndex = int.Parse(Console.ReadLine());
                        backupJobs[jobIndex-1].JobRun();
                        GetInput(language.GetDialogue("Job Done")); 
                        break;
                    
                    case 5:
                        //Run all the jobs sequentially
                        foreach (JobManager job in backupJobs)
                        {
                            job.JobRun();
                            
                        }
                        GetInput(language.GetDialogue("Jobs Done"));
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
            string name = GetInput(language.GetDialogue("Job Name"));
            string source = GetInput(language.GetDialogue("Job Source"));
            string target = GetInput(language.GetDialogue("Job Target"));
            int typeJob = GetIntegerInput(language.GetDialogue("Job Type"));

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
                answerModification = GetInput($"{language.GetDialogue("ModifyPreviousJob")} {language.GetDialogue("Yes")}/{language.GetDialogue("No")}");
                if (answerModification == language.GetDialogue("Yes"))
                {
                    answerModification = language.GetDialogue("Yes");
                    ModifyBackupJob(newBackupJob);
                    answerModification = language.GetDialogue("No");
                }   
                else
                {
                    backupJobs.Add(newBackupJob);
                    answerModification = language.GetDialogue("No");
                    break;
                }
            }


            string answerAdd = "";
            while (answerAdd != language.GetDialogue("No"))
            {
                answerAdd = GetInput($"{language.GetDialogue("AddAnotherJob")} {language.GetDialogue("Yes")}/{language.GetDialogue("No")   }");
                if (backupJobs.Count > 4)
                {
                    Console.WriteLine(  language.GetDialogue("Jobs limit")  );
                    GetInput(language.GetDialogue("PressAnyKeyToLeave"));
                    break;
                }
                if (answerAdd == language.GetDialogue("Yes"))
                {
                    answerAdd = language.GetDialogue("Yes");
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
            string nameM = GetInput(language.GetDialogue("Job Name"));
            string sourceM = GetInput(  language.GetDialogue("Job Source"));
            string targetM = GetInput(  language.GetDialogue("Job Target"));
            int typeJobM = GetIntegerInput(language.GetDialogue("Job Type"));
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
                Console.WriteLine(language.GetDialogue("PressAnyKeyToLeave"));
                string key = Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine(language.GetDialogue("BackupJobCreated"));
                ShowIndexBackupJobs();


                Console.WriteLine($"{language.GetDialogue("ModifyAnyJobs")} {language.GetDialogue("Yes")}/{language.GetDialogue("No")}"); 
                Console.WriteLine("");

                JobsModification = Console.ReadLine();
                if (JobsModification == language.GetDialogue("Yes")) // Modify or delete jobs
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
            int Mchoice = int.TryParse(Console.ReadLine(), out Mchoice) ? Mchoice : -1;
            switch (Mchoice)
            {
                case 1:
                    Console.WriteLine(language.GetDialogue("EnterIndexToModify"));
                    int Indexchoice = int.TryParse(Console.ReadLine(), out Indexchoice) ? Indexchoice : -1;
                    string nameM = GetInput(language.GetDialogue("Job Name"));
                    string sourceM = GetInput(language.GetDialogue("Job Source"));
                    string targetM = GetInput(language.GetDialogue("Job Target"));
                    int typeJobM = GetIntegerInput(language.GetDialogue("Job Type"));
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
            int JobIndex2 = int.Parse(Console.ReadLine());
            backupJobs.RemoveAt(JobIndex2 - 1);
        }

        private void ShowLogParameters()
        {
            Console.WriteLine($"1. {language.GetDialogue("SetLogsRepertory")}");
            Console.WriteLine($"2. {language.GetDialogue("ShowLogsRepertory")}");
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
            Console.Write(language.GetDialogue("ShowLogsRepertory"));
            logDirectory = Console.ReadLine();
            Console.Clear();
        }

        private void ShowLogsRepertory()
        {
            Console.Clear();
            ShowLogo();
            Console.WriteLine($"{language.GetDialogue("CurrentLogsRepertory")}: {logDirectory}");
            Console.WriteLine(language.GetDialogue("PressAnyKeyToLeave"));
            Console.ReadLine();
            Console.Clear();
        }

        private void ChooseLanguage()
        {
            Console.Clear();
            ShowLogo();

            Console.WriteLine(language.GetDialogue("ChooseLanguage"));
            Console.WriteLine(language.GetDialogue("InvalidLanguageChoice"));
            string languageM = Console.ReadLine();
            language.ChangeLanguage(languageM);
            if (languageM != "fr" && languageM != "en")
            {
                Console.WriteLine(language.GetDialogue("InvalidLanguageChoice"));
                Console.WriteLine(language.GetDialogue("PressAnyKeyToLeave"));

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


  