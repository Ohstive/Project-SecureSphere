using ProjetV2.Model;
using ProjetV2.View;
using ProjetV2.ViewModel;


namespace ProjetV2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ErrorCatchException error = new ErrorCatchException();

            string Source = @"C:\Users\Ostiv\Documents\Test";
            string Target = @"C:\Users\Ostiv\Documents\Test3";
            Jobs J = new Jobs("JOB",Source,Target);
            JobDirectory JD = new JobDirectory(J);
            Console.WriteLine("dtzqt");
        }
    }
}
