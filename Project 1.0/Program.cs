using Project_1._0.Model;
using Project_1._0.ViewModel;
using Project_1._0.ViewModel.Services;
using Project_1._0.View;

namespace Project_1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDirectoryPath = @"C:\Users\Ostiv\Documents\Test3";
            string targetDirectoryPath = @"C:\Users\Ostiv\Documents\Test5";
            Jobs jobs = new Jobs("JOB1",sourceDirectoryPath,targetDirectoryPath,0);
            JobManager jobManager = new JobManager(jobs);
       
            //jobManager.JobDifferentialCopy();
            //jobManager.JobFullCopy();

           JobView jobView = new JobView();

        }
    }

}
