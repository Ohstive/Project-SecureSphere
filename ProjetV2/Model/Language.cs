using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProjetV2.Model
{
    internal class Language
    {
        private string CurrentLanguage;

        private Dictionary< string ,Dictionary<string, string> > dialogueDictionary;

        public Language() 
        {
            this.CurrentLanguage = "en";//default language
            
            dialogueDictionary = new Dictionary<string,Dictionary<string, string>>()
                ; // Initialize the dictionary

            Dictionary<string, string> Text = new Dictionary<string, string>()
            {
                {"Speed","Speed"},
                {"Job Name","Job Name"},
                {"From","From"}
            };
            AddLanguage(CurrentLanguage, Text);
        }
        //Temporary function for testing
        public void Affiche()
        {
            foreach (var paire in this.dialogueDictionary)
            {
                foreach (var text in paire.Value)

                Console.WriteLine($"Clé:{paire.Key} , Valeur: {text}");
            }
        }
        public string currentLanguage { get=>this.CurrentLanguage;}

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

        public void ChangeLanguage(string language)
        {
            if (this.dialogueDictionary.ContainsKey(language))
            {
                this.CurrentLanguage = language;
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
