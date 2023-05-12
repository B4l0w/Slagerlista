using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace slagerlista
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] beolvasas = File.ReadAllLines("osszeszene.txt");
            string[] osszeszene = new string[beolvasas.Length];
            string[] pontszamok = File.ReadAllLines("pontszamok.txt");
            List<string> zenek = new List<string>();

            for (int i = 0; i < beolvasas.Length; i++)
            {
                osszeszene[i] = beolvasas[i];
            }

            string[] menupontok = { "Top10 megadása", "Top10 kilistázása"};
            for (int i = 0; i < menupontok.Length; i++)
            {
                Console.WriteLine($"[{i+1}] {menupontok[i]}");
            }
            char valasz = Console.ReadKey().KeyChar;
            Console.Clear();
            switch (valasz)
            {
                case '1':
                    StreamWriter fajlbair = new StreamWriter("osszeszene.txt");
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine(i+1);
                        Console.WriteLine("Adja meg a zene címét");
                        string zenecim = Console.ReadLine();
                        Console.WriteLine("Adja meg a zene szerzőjét");
                        string szerzo = Console.ReadLine();
                        for (int j = 0; j < osszeszene.Length; j++)
                        {
                            int szamlalo = int.Parse(osszeszene[i].Split('\t')[2]);
                            if (osszeszene[i].Split('\t')[0].Contains("Zene") && osszeszene[i].Split('\t')[1].Contains("Gáspár Laci") && szamlalo == 0)
                            {
                                //Akkor kikéne írni a fájlba a pontszámmal kötőjellel (-)
                                fajlbair.WriteLine(zenecim + "\t" + szerzo + "\t" + szamlalo+1);
                                //fajlbair.WriteLine(zenecim + " - " + szerzo);
                            }
                            else
                            {
                                fajlbair.WriteLine(zenecim + "\t" + szerzo + "\t" + 0);
                            }
                        }
                        Console.Clear();
                    }
                    fajlbair.Close();
                    break;

                case '2':

                default:
                    break;
            }
            Console.ReadLine();
        }
    }
}
