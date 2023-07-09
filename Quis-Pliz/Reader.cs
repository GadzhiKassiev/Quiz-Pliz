namespace Quis_Pliz
{
    internal class Reader
    {       
        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static ConsoleKeyInfo input;

        static Reader()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(reader);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private static void reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = Console.ReadKey();
                gotInput.Set();
            }
        }

        public static string ReadLine(int timeOutMillisecs = Timeout.Infinite)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return input.KeyChar.ToString();
            else
                return "";
        }        
    }
}
