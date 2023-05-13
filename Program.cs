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
            //Fájl beolvasása, lista létrehozása
            string[] beolvasas = File.ReadAllLines("osszeszene.txt");
            List<string> zenek = new List<string>();

            //Lista feltöltése a fájlban lévő adatokkal
            for (int i = 0; i < beolvasas.Length; i++)
            {
                zenek.Add(beolvasas[i]);
            }

            //Menüpontok tömbben
            string[] menupontok = { "Top10 megadása", "Top10 kilistázása" , "Kilépés"};

            while (true)
            {
                //Menüpontok kiíratása
                for (int i = 0; i < menupontok.Length; i++)
                {
                    Console.WriteLine($"[{i + 1}] {menupontok[i]}");
                }

                char valasz = Console.ReadKey().KeyChar;
                Console.Clear();

                //Menüsor működése switchel
                switch (valasz)
                {
                    case '1':
                        //10 zene bekérése
                        for (int i = 0; i < 10; i++)
                        {
                            //A zene és szerző bekérése
                            Console.WriteLine($"Adja meg a(z) {i+1}. zene címét");
                            string zenecim = Console.ReadLine();
                            Console.WriteLine($"Adja meg a(z) {i+1}. zene szerzőjét");
                            string szerzo = Console.ReadLine();

                            //Végigmegy a listán
                            for (int j = 0; j < zenek.Count; j++)
                            {
                                //A jelenlegi zene szavazatának száma
                                int szamlalo = int.Parse(zenek[j].Split('-')[2]);

                                //Ha már létezik a megadott zene a listában akkor törli a listából
                                //és hozzáadja megint +1 szavazattal
                                if (zenek[j].Contains(zenecim + " - " + szerzo))
                                {
                                    zenek.RemoveAt(j);
                                    zenek.Add(zenecim + " - " + szerzo + " - " + (szamlalo + 1));
                                    break;
                                }

                                //Ha nincs még ilyen a listában, hozzáadja 1 szavazattal
                                if (j == zenek.Count - 1)
                                {
                                    szamlalo = 1;
                                    zenek.Add(zenecim + " - " + szerzo + " - " + (szamlalo));
                                }
                            }
                            Console.Clear();
                        }
                        
                        //Beleírja a fájlba a frissített listát
                        StreamWriter fajlbair = new StreamWriter("osszeszene.txt");
                        for (int i = 0; i < zenek.Count; i++)
                        {
                            fajlbair.WriteLine(zenek[i]);
                        }
                        fajlbair.Close();
                        break;

                    case '2':
                        try
                        {
                            int kiirosszamlalo = 0;

                            //Top 10 rendezése
                            var rendezettLista = zenek.OrderByDescending(zene => int.Parse(zene.Split('-')[2]));
                            Console.WriteLine("Zene címe - Szerző - Szavazatok");
                            Console.WriteLine();

                            //Top 10 kiíratása
                            foreach (string zene in rendezettLista)
                            {
                                kiirosszamlalo++;
                                Console.WriteLine($"[{kiirosszamlalo}]\t{zene}");                 

                                //Ha megvan a 10 kiíratott akkor leáll
                                if (kiirosszamlalo == 10)
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            //Ha nincs elég a top10 kiírásához ezt írja ki
                            Console.WriteLine("Nincs elég");
                        }
                        break;

                    case '3':
                        //Kilépés
                        System.Environment.Exit(1);
                        break;

                    default:
                        Console.WriteLine("Nem létezik ilyen menüpont!");
                        break;
                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - 17, Console.WindowHeight / 2);
                Console.WriteLine("Nyomj meg egy gombot a továbblépéshez");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}