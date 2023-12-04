using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Project_1._0.Model.Language
{
    internal class Language
    {
        private string _currentLanguage { get; set; }

        private Dictionary<string, Dictionary<string, string>> dialogueDictionary;

        public Language()
        {
            this._currentLanguage = "en";//default language

            this.dialogueDictionary = new Dictionary<string, Dictionary<string, string>>
        {
         {
            "en", new Dictionary<string, string>
            {
                { "FullNameLanguage", "English" },
                { "WelcomeMessage", "Welcome to our application!" },
                { "EnterBackupJobDetails", "Enter details for Backup Job:" },
                { "BackupJobCreated", "Backup Job created: {0}" },
                { "ModifyPreviousJob", "Modify the previous Job? (y/n)" },
                { "AddAnotherJob", "Add another Job? (y/n)" },
                { "CreatedBackupJobs", "Created Backup Jobs:" },
                { "ModifyAnyJobs", "Modify any Jobs? (y/n)" },
                { "SetLogsRepertory", "Set Logs Repertory" },
                { "ShowLogsRepertory", "Show Logs Repertory" },
                { "ChooseLanguage", "Choose the language:" },
                { "InvalidLanguageChoice", "Please choose between French(fr) or English(en)" },
                { "Yes", "Yes" },
                { "InvalidChoice", "Invalid choice" },
                { "NoJobsMessage", "There are no Jobs" },
                { "PressAnyKeyToLeave", "Press any key to leave:" },
                { "EnterIndexToModify", "Enter the index that you want to modify:" },
                { "EnterIndexToDelete", "Enter the index that you want to delete:" },
                { "JobIndexToRun", "Enter the index of the job to run:" },
                { "CurrentLogsRepertory", "Your current Logs repertory is: {0}" },
                { "RunBackupJob", "RunBackupJob" }
            }
        },
        {
            "fr", new Dictionary<string, string>
            {
                { "WelcomeMessage", "Bienvenue dans notre application !" },
                { "EnterBackupJobDetails", "Entrez les détails du travail de sauvegarde :" },
                { "BackupJobCreated", "Travail de sauvegarde créé : {0}" },
                { "ModifyPreviousJob", "Modifier le travail précédent ? (o/n)" },
                { "AddAnotherJob", "Ajouter un autre travail ? (o/n)" },
                { "CreatedBackupJobs", "Travaux de sauvegarde créés :" },
                { "ModifyAnyJobs", "Modifier des travaux ? (o/n)" },
                { "SetLogsRepertory", "Définir le répertoire des journaux" },
                { "ShowLogsRepertory", "Afficher le répertoire des journaux" },
                { "ChooseLanguage", "Choisissez la langue :" },
                { "FrenchOrEnglish", "Français(fr) ou Anglais(en) :" },
                { "InvalidLanguageChoice", "Veuillez choisir entre Français(fr) ou Anglais(en)" },
                { "Yes", "Oui" },
                { "InvalidChoice", "Choix non valide" },
                { "NoJobsMessage", "Il n'y a pas de travaux" },
                { "PressAnyKeyToLeave", "Appuyez sur n'importe quelle touche pour quitter :" },
                { "EnterIndexToModify", "Entrez l'index que vous souhaitez modifier :" },
                { "EnterIndexToDelete", "Entrez l'index que vous souhaitez supprimer :" },
                { "JobIndexToRun", "Entrez l'index du travail à exécuter :" },
                { "CurrentLogsRepertory", "Votre répertoire actuel des journaux est : {0}" },
                { "RunBackupJob", "Exécuter le travail de sauvegarde" }
            }
        }
    };


        }
        //Temporary function for testing

        public string GetCurrentLanguage()
        {
            return this._currentLanguage;
        }
        public Dictionary<string, string> GetDialogue(string language)
        {
            if (this.dialogueDictionary.ContainsKey(language))
            {
                return this.dialogueDictionary[language];
            }
            else
            {
                throw new Exception("This language doesn't exist");
            }
        }

        //Initialize the language from a file
        public void InitializeLanguage(string filePathfull)
        {
            // Read JSON directly from a file
            string jsonData = File.ReadAllText(filePathfull);
            this.dialogueDictionary = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonData);
        }

        public void ChangeLanguage(string language)
        {
            if (this.dialogueDictionary.ContainsKey(language))
            {
                this._currentLanguage = language;
            }
            else
            {
                throw new Exception("This language doesn't exist");
            }
        }

        public void AddLanguage(string languageCode, Dictionary<string, string> dialogue)
        {
            if (this.dialogueDictionary.ContainsKey(languageCode))
            {
                throw new Exception("This language Already exist");
            }

            else
            {
                dialogueDictionary.Add(languageCode, dialogue);
            }
        }
    }
}