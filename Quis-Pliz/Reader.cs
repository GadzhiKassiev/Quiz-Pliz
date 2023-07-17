namespace Quis_Pliz
{
    internal class Reader
    {       
        static Thread s_inputThread;
        static AutoResetEvent s_getInput, s_gotInput;
        static ConsoleKeyInfo s_input;

        static Reader()
        {
            s_getInput = new AutoResetEvent(false);
            s_gotInput = new AutoResetEvent(false);
            s_inputThread = new Thread(reader);
            s_inputThread.IsBackground = true;
            s_inputThread.Start();
        }
        public static string ReadLine(int timeOutMillisecs = Timeout.Infinite)
        {
            s_getInput.Set();
            bool success = s_gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return s_input.KeyChar.ToString();
            else
                return "";
        }

        private static void reader()
        {
            while (true)
            {
                s_getInput.WaitOne();
                while (Console.KeyAvailable)
                    Console.ReadKey(false);
                s_input = Console.ReadKey();
                s_gotInput.Set();
            }
        }      
    }
}
