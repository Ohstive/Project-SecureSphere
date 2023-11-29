using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetV2.ViewModel
{
    internal class LogStatus : Log
    {
        string State;
        int NbFilesLeftToDo;
        float NbFilesSizeLeftToDo;
        int Progression;
        public LogStatus()
        {
            NbFilesLeftToDo = 0;
            NbFilesSizeLeftToDo = 0;
            Progression = 0;
        }

    }
}
