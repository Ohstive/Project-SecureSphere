using SecureSphere_V1.Model;
using SecureSphere_V1.View;
using SecureSphere_V1.ViewModel;

namespace SecureSphere_V1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JobConfiguration job = new JobConfiguration("Job1", @"C:\test", @"C:\test2", "Full", "Sequential");
            JobManager jobManager = new JobManager(job, @"C:\log", @"C:\DailyLog");
            jobManager.RunJob();
            JobView jobView = new JobView(jobManager);
        }
    }
}
