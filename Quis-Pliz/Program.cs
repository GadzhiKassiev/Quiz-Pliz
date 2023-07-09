using System.Configuration;

namespace Quis_Pliz
{
    class Program
    {
        static readonly int N = int.Parse(ConfigurationManager.AppSettings["NumberQuestions"]!);
        static void Main(string[] args)
        {
            IFetcher jf = new JSONFetcher("data.json");        
            Game game = new Game(jf, N);
            game.Initializer("report.txt");
            game.Run();
            game.End();
        }
    }
}
