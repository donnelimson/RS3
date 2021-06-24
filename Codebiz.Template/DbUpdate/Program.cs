using System;

namespace DbUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to PSSLAI-AML Db Updater");

            try
            {
                DatabaseHelper.Initialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex);
            }

            Console.Write("\n\nPress any key to exit. ");
            Console.ReadKey(false);
        }
    }
}
