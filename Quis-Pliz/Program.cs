using System.Configuration;

namespace Quis_Pliz
{
    class Program
    {
        static readonly int N = int.Parse(ConfigurationManager.AppSettings["NumberQuestions"]!);
        const string fileNameOfDate = "datawithJson.txt";
        const string fileNameOfReport = "report.txt";
        static void Main(string[] args)
        {
            IFetcher jf = new JSONFetcher(fileNameOfDate);        
            Game game = new Game(jf, N);
            game.Initializer(fileNameOfReport);
            game.Run();
            game.End();
            Console.ReadLine();
        }
    }
}
