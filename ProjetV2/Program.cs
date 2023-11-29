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
            Console.WriteLine(error.IsNotError());

            string Source = @"C:\Users\Ostiv\Documents\Test4\ZIP.rar";

            string Target = @"C:\Users\Ostiv\Documents\Test";

            Jobs J = new Jobs("JOB1",Source,Target);
           


        }
    }
}
