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
            Game game = new Game(N);
            ReportManager rm = new ReportManager(fileNameOfReport);
            game.Initializer(jf.fetchDate(), rm);
            game.Run();
            game.End();
            Console.ReadLine();
        }
    }
}
