namespace Database
{
    public class Argument
    {
        /// <summary>Nacte argumenty do adresare</summary>
        public static Dictionary<string, string> GetArgument(string[] args)
        {
            Dictionary<string, string> arguments = [];
            foreach (var arg in args)
            {
                string[] roz = arg.Split(':');
                if (roz[0] == null && roz[1] == null)   //&&-AND,   ||-OR
                {
                    Console.WriteLine("Chyba argumentu");
                    continue;
                }
                arguments.Add(roz[0], roz[1]);
            }

            //kontrola existence klíče názvu výkresu
            if (arguments.ContainsKey("V") == false)
            {
                arguments.Add("V", "B7001");
                Console.WriteLine("Nebyl zadán výkres. Automaticky nastaven výkres č." + arguments["V"].ToString());
                //Console.ReadKey();
            }

            //kontrola existence klíče názvu výkresu
            if (arguments.ContainsKey("K") == false)
            {
                arguments.Add("K", "false");
                Console.WriteLine("Požadavek kopirování DBF nastaven na:" + arguments["K"].ToString());
                //Console.ReadKey();
            }
            //kontrola existence klíče cesty
            if (arguments.ContainsKey("C") == false)
            {
                arguments.Add("C", "Pokus.xml");
                Console.WriteLine("Nebyl zadán soubor. Automaticky nastaven soubor :" + arguments["C"].ToString());
                //Console.ReadKey();
            }
            //bool Kopirovat = ("true" == Pole["K"].ToString().ToLower()) ? true : false;
            return arguments;
        }

        public static Dictionary<string, string> AllArgument(string[] args)
        {
            Dictionary<string, string> arg = GetArgument(args);
            Console.WriteLine("GetArgument(args)");
            return arg;
        }
    }
}
