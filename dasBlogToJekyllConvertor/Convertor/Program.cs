using System;

namespace Convertor
{
    class Program
    {
        private const string OUTPUTPATH = "_posts";

        static void Main()
        {
            ConsoleSetup();



            Console.ReadLine();
        }

        private static void ConsoleSetup()
        {
            Console.Title = "dasBlog to Jekyll convertor";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"     .___             __________.__                    __                ____.       __           .__  .__   ");
            Console.WriteLine(@"   __| _/____    _____\______   \  |   ____   ____   _/  |_  ____       |    | ____ |  | _____.__.|  | |  |  ");
            Console.WriteLine(@"  / __ |\__  \  /  ___/|    |  _/  |  /  _ \ / ___\  \   __\/  _ \      |    |/ __ \|  |/ <   |  ||  | |  |  ");
            Console.WriteLine(@" / /_/ | / __ \_\___ \ |    |   \  |_(  <_> ) /_/  >  |  | (  <_> ) /\__|    \  ___/|    < \___  ||  |_|  |__");
            Console.WriteLine(@" \____ |(____  /____  >|______  /____/\____/\___  /   |__|  \____/  \________|\___  >__|_ \/ ____||____/____/");
            Console.WriteLine(@"      \/     \/     \/        \/           /_____/                                \/     \/\/                ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
