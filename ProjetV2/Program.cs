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
            //error.SourceTargetSimilar();
          

            string Source = @"C:\Users\Ostiv\Documents\Test";

            string Target = @"C:\Users\Ostiv\Documents\Test4";

            Jobs J = new Jobs("JOB1",Source,Target);
            ViewJob V = new ViewJob();
           


        }
    }
}
