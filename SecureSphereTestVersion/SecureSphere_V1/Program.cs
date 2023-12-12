using SecureSphere_V1.Model;
using SecureSphere_V1.View;
using SecureSphere_V1.ViewModel;

namespace SecureSphere_V1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JobConfiguration job = new JobConfiguration("Job1", @"C:\Users\Ostiv\Documents\Test3", @"C:\Users\Ostiv\Documents\Test4", "Full", "Sequential");
            JobManager jobManager = new JobManager(job, @"C:\Users\Ostiv\Documents\Log", @"C:\Users\Ostiv\Documents\LogStatut");
            jobManager.RunJob();
            JobView jobView = new JobView(jobManager);
        }
    }
}
